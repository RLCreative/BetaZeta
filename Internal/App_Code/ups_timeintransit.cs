using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace ShippingRates.RateShop
{
    /// <summary>
    /// Retrieve shipping rates from UPS using XML and HTTP POST.
    /// </summary>
    public class ups_TimeInTransit
    {
        #region Variables
        private string m_version = "1.5";

        public string WebURL;
        public string ProxyAddress;
        public string AccessLicenseNumber;
        public string UserID;
        public string Password;

        public string ShipperCity;
        private string m_ShipperStateProvinceCode;
        private string m_ShipperCountryCode;
        private string m_ShipperPostalCode;

        public string ReceiverCity;
        private string m_ReceiverStateProvinceCode;
        private string m_ReceiverCountryCode;
        private string m_ReceiverPostalCode;
        private string m_ResidentialAddressIndicator;

        private string m_ShipDate;
        private string m_TotalPackages;
        public string ShipmentWeight;

        private int m_RatesCount;
        private string m_ResultCode;
        protected string m_ErrorDescription;
        private string m_RawXMLText;
        private string m_RawXMLRequestText;

        protected RateDetail UPS_RateDetail;
        protected ArrayList UPS_Rates = new ArrayList();

        protected PackageDetail UPS_PackageDetail;
        protected ArrayList UPS_Packages = new ArrayList();

        private static Regex _isNumber = new Regex(@"^\d+$");
        private static Regex pkgNumber = new Regex(@"\s\d+.$");
        #endregion

        #region Enumerations
        public enum ResidentialAddressIndicator_Options
        {
            Yes,
            No
        }
        #endregion

        #region Accessors
        public string Version
        {
            get { return m_version; }
        }

        public string ShipperStateProvinceCode
        {
            get { return m_ShipperStateProvinceCode; }
            set { m_ShipperStateProvinceCode = value.ToUpper(); }
        }

        public string ReceiverStateProvinceCode
        {
            get { return m_ReceiverStateProvinceCode; }
            set { m_ReceiverStateProvinceCode = value.ToUpper(); }
        }

        public string ShipperCountryCode
        {
            get { return m_ShipperCountryCode; }
            set { m_ShipperCountryCode = value.ToUpper(); }
        }

        public string ReceiverCountryCode
        {
            get { return m_ReceiverCountryCode; }
            set { m_ReceiverCountryCode = value.ToUpper(); }
        }

        public string ShipperPostalCode
        {
            set
            {
                if (m_ShipperCountryCode.ToUpper() == "US")
                {
                    m_ShipperPostalCode = value.Substring(0, 5);
                }
                else
                {
                    m_ShipperPostalCode = value;
                }
            }
        }

        public string ReceiverPostalCode
        {
            set
            {
                if (m_ReceiverCountryCode.ToUpper() == "US")
                {
                    m_ReceiverPostalCode = value.Substring(0, 5);
                }
                else
                {
                    m_ReceiverPostalCode = value;
                }
            }
        }

        public ResidentialAddressIndicator_Options ResidentialAddressIndicator
        {
            set
            {
                if (value == ResidentialAddressIndicator_Options.Yes)
                {
                    m_ResidentialAddressIndicator = "True";
                }
                else
                {
                    m_ResidentialAddressIndicator = "";
                }
            }
        }

        public PackageDetail[] UPSPackages
        {
            get
            {
                PackageDetail[] pkgs = new PackageDetail[UPS_Packages.Count];
                UPS_Packages.CopyTo(pkgs);
                return pkgs;
            }
        }

        public int RatesCount
        {
            get { return m_RatesCount; }
        }

        public RateDetail[] UPSRates
        {
            get
            {
                RateDetail[] rates = new RateDetail[UPS_Rates.Count];
                UPS_Rates.CopyTo(rates);
                return rates;
            }
        }

        public string ResultCode
        {
            get { return m_ResultCode; }
        }

        public string ErrorDescription
        {
            get { return m_ErrorDescription; }
        }

        public string RawXMLText
        {
            get { return m_RawXMLText; }
        }

        public string RawXMLRequestText
        {
            get { return m_RawXMLRequestText; }
        }
        #endregion

        #region Structs
        public struct RateDetail : IComparable
        {
            public string UPSCode;
            public string Description;
            public decimal ShippingCost;
            public string GuaranteedDaysToDelivery;
            public string ScheduledDeliveryTime;
            public string[] Warnings;

            // Defines how to compare two RateDetail structs
            public int CompareTo(object obj)
            {
                if (obj is RateDetail)
                {
                    RateDetail cost = (RateDetail)obj;

                    return ShippingCost.CompareTo(cost.ShippingCost);
                }

                throw new ArgumentException("object is not a RateDetail");
            }
        }

        public struct PackageDetail
        {
            //public PackageType_Options PackageType;
            public string Description;
            public string Length;
            public string Width;
            public string Height;
            public string Weight;
            public string Oversize;
            public string InsuredValue;
        }
        #endregion

        public ups_TimeInTransit()
        {
            ProxyAddress = "";
            ShipperCountryCode = "US";
            ResidentialAddressIndicator = ResidentialAddressIndicator_Options.No;
            m_ShipDate = System.DateTime.Today.ToString();
            m_TotalPackages = "1";
            m_RatesCount = 0;
            m_ErrorDescription = "";
            m_RawXMLText = "";
        }

        public void getTimes()
        {
            try
            {
                // ***************************
                // Create XML rate request string
                // ***************************

                string strXML_AccessRequest;
                string strXML;

                // Access Request XML
                strXML_AccessRequest = "<?xml version='1.0'?><AccessRequest xml:lang='en-US'><AccessLicenseNumber>" + AccessLicenseNumber + "</AccessLicenseNumber>";
                strXML_AccessRequest = strXML_AccessRequest + "<UserId>" + UserID + "</UserId><Password>" + Password + "</Password></AccessRequest>";

                // Time In Transit Request XML
                strXML = "<?xml version='1.0'?><TimeInTransitRequest xml:lang='en-US'>";
                strXML = strXML + "<Request><TransactionReference><CustomerContext>Time In Transit</CustomerContext><XpciVersion>1.0002</XpciVersion></TransactionReference>";
                strXML = strXML + "<RequestAction>TimeInTransit</RequestAction>";
                strXML = strXML + "</Request>";

                // Shipper information
                strXML = strXML + "<TransitFrom><AddressArtifactFormat>";
                strXML = strXML + "<PoliticalDivision2>" + ShipperCity + "</PoliticalDivision2>";
                strXML = strXML + "<PoliticalDivision1>" + m_ShipperStateProvinceCode + "</PoliticalDivision1>";
                strXML = strXML + "<CountryCode>" + ShipperCountryCode + "</CountryCode>";
                strXML = strXML + "<PostcodePrimaryLow>" + m_ShipperPostalCode + "</PostcodePrimaryLow>";
                strXML = strXML + "</AddressArtifactFormat></TransitFrom>";

                // Receiver information
                strXML = strXML + "<TransitTo><AddressArtifactFormat>";
                strXML = strXML + "<PoliticalDivision2>" + ReceiverCity + "</PoliticalDivision2>";
                strXML = strXML + "<PoliticalDivision1>" + m_ReceiverStateProvinceCode + "</PoliticalDivision1>";
                strXML = strXML + "<CountryCode>" + ReceiverCountryCode + "</CountryCode>";
                strXML = strXML + "<PostcodePrimaryLow>" + m_ReceiverPostalCode + "</PostcodePrimaryLow>";

                if (m_ResidentialAddressIndicator != "")
                {
                    strXML = strXML + "<ResidentialAddressIndicator></ResidentialAddressIndicator>";
                }

                strXML = strXML + "</AddressArtifactFormat></TransitTo>";

                // Shipment Date
                try
                {
                    strXML = strXML + "<PickupDate>" + Convert.ToDateTime(m_ShipDate).ToString("yyyyMMdd") + "</PickupDate>";
                }
                catch
                {
                    strXML = strXML + "<PickupDate>" + System.DateTime.Today.ToString("yyyyMMdd") + "</PickupDate>";
                }

                // Shipment weight
                strXML = strXML + "<ShipmentWeight>";
                strXML = strXML + "<UnitOfMeasurement><Code>LBS</Code></UnitOfMeasurement>";
                strXML = strXML + "<Weight>" + ShipmentWeight + "</Weight>";
                strXML = strXML + "</ShipmentWeight>";

                // Total number of packages
                strXML = strXML + "<TotalPackagesInShipment>" + m_TotalPackages + "</TotalPackagesInShipment>";

                strXML = strXML + "</TimeInTransitRequest>";

                m_RawXMLRequestText = strXML;

                // ***************************
                // Send request and retrieve response
                // ***************************

                // Create web request
                HttpWebRequest timeRequest = (HttpWebRequest)HttpWebRequest.Create(WebURL);

                // Create the proxy class instance
                if (ProxyAddress != "")
                {
                    IWebProxy prxy = new WebProxy(ProxyAddress);
                    timeRequest.Proxy = prxy;
                }

                timeRequest.AllowAutoRedirect = false;
                timeRequest.Method = "POST";
                timeRequest.ContentType = "application/x-www-form-urlencoded";

                // Create POST stream
                Stream RequestStream = timeRequest.GetRequestStream();
                Byte[] SomeBytes = Encoding.UTF8.GetBytes(strXML_AccessRequest + strXML);
                RequestStream.Write(SomeBytes, 0, SomeBytes.Length);
                RequestStream.Close();

                // Send request and get response
                HttpWebResponse timeResponse = (HttpWebResponse)timeRequest.GetResponse();

                if (timeResponse.StatusCode == HttpStatusCode.OK)
                {
                    // Get the stream
                    Stream ResponseStream = timeResponse.GetResponseStream();
                    Encoding charEncoding = Encoding.GetEncoding("utf-8");

                    // Send the stream to a reader
                    StreamReader readStream = new StreamReader(ResponseStream, charEncoding);

                    // Read the response
                    string Result = readStream.ReadLine();
                    m_RawXMLText = Result;
                    timeResponse.Close();
                }

                // ***************************
                // Begin parsing rate response
                // ***************************
                /*
                                // Load response XML into an XML document
                                System.Xml.XmlDocument responseXMLDocument;
                                responseXMLDocument = new System.Xml.XmlDocument();
                                responseXMLDocument.LoadXml(m_RawXMLText);

                                // Check for errors
                                System.Xml.XmlNode Success_Node;
                                Success_Node = responseXMLDocument.SelectSingleNode("/RatingServiceSelectionResponse/Response/ResponseStatusCode");

                                if(Success_Node.InnerText != "1")
                                {
                                    m_ResultCode = "1";
                                    m_ErrorDescription = responseXMLDocument.SelectSingleNode("/RatingServiceSelectionResponse/Response/Error/ErrorDescription").InnerText;
                                }
                                else
                                {
                                    // No errors. Get list of rates
                                    System.Xml.XmlNodeList RatedShipment_NodeList;
                                    RatedShipment_NodeList = responseXMLDocument.SelectNodes("/RatingServiceSelectionResponse/RatedShipment");
                                    m_RatesCount = RatedShipment_NodeList.Count;
					
                                    // Loop through rates and build rate detail
                                    for(int i=0; i<m_RatesCount; i++)
                                    {
                                        UPS_RateDetail.UPSCode = RatedShipment_NodeList[i].SelectSingleNode("Service/Code").InnerText;
                                        UPS_RateDetail.ShippingCost = System.Convert.ToDecimal(RatedShipment_NodeList[i].SelectSingleNode("TotalCharges/MonetaryValue").InnerText);
                                        UPS_RateDetail.GuaranteedDaysToDelivery = RatedShipment_NodeList[i].SelectSingleNode("GuaranteedDaysToDelivery").InnerText;
                                        UPS_RateDetail.ScheduledDeliveryTime = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;

                                        switch(UPS_RateDetail.UPSCode)
                                        {
                                            case "01":
                                                UPS_RateDetail.Description = "UPS Next Day Air";
                                                break;
                                            case "02":
                                                UPS_RateDetail.Description = "UPS 2nd Day Air";
                                                break;
                                            case "03":
                                                UPS_RateDetail.Description = "UPS Ground";
                                                break;
                                            case "07":
                                                UPS_RateDetail.Description = "UPS Worldwide ExpressSM";
                                                break;
                                            case "08":
                                                UPS_RateDetail.Description = "UPS Worldwide ExpeditedSM";
                                                break;
                                            case "11":
                                                UPS_RateDetail.Description = "UPS Standard UPS";
                                                break;
                                            case "12":
                                                UPS_RateDetail.Description = "UPS 3 Day Select";
                                                break;
                                            case "13":
                                                UPS_RateDetail.Description = "UPS Next Day Air Saver";
                                                break;
                                            case "14":
                                                UPS_RateDetail.Description = "UPS Next Day Air Early A.M.";
                                                break;
                                            case "54":
                                                UPS_RateDetail.Description = "UPS Worldwide Express PlusSM";
                                                break;
                                            case "59":
                                                UPS_RateDetail.Description = "UPS 2nd Day Air A.M.";
                                                break;
                                            default:
                                                UPS_RateDetail.Description = "Unknown Service Code";
                                                break;
                                        }

                                        // Record the rated shipment warnings for UPS Ground rates
                                        if(UPS_RateDetail.UPSCode == "03")
                                        {
                                            System.Xml.XmlNodeList RatedShipment_Warnings;
                                            RatedShipment_Warnings = RatedShipment_NodeList[i].SelectNodes("RatedShipmentWarning");

                                            if(RatedShipment_Warnings.Count > 0)
                                            {
                                                string warning = "";
                                                string temp = "";

                                                for(int j=0; j<RatedShipment_Warnings.Count; j++)
                                                {
                                                    warning = RatedShipment_Warnings[j].InnerText;

                                                    if(warning.Substring(0,8) == "Oversize")
                                                    {
                                                        temp = pkgNumber.Match(warning).Value;

                                                        if(temp != "")
                                                        {
                                                            temp = temp.Substring(1,temp.Length-2);
                                                            if(IsInteger(temp))
                                                            {
                                                                UPSPackages[Convert.ToInt32(temp)-1].Oversize = warning.Substring(0,10);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        UPS_Rates.Add(UPS_RateDetail);
                                    }

                                    // Sort all of the rates in order of shipping cost
                                    UPS_Rates.Sort();

                                    m_ResultCode = "0";
                                    m_ErrorDescription = "";
                                }*/
            }
            catch (Exception ex)
            {
                m_ResultCode = "2";
                m_ErrorDescription = ex.Message;
            }
        }

        public static bool IsInteger(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }
    }
}

