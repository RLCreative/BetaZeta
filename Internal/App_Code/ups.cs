using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingRates
{
    /// <summary>
    /// Retrieve shipping rates from UPS using XML and HTTP POST.
    /// </summary>
    public class ups
    {
        #region Variables
        private string m_version = "1.5";

        private string RatesWebURL;
        private string TimesWebURL;
        private string AccessLicenseNumber;
        private string UserID;
        private string Password;
        public string ProxyAddress;

        public string ShipperNumber;
        private string m_PickupTypeCode;
        private string m_CustomerClassification;

        public string ShipperCity;
        private string m_ShipperStateProvinceCode;
        private string m_ShipperCountryCode;
        private string m_ShipperPostalCode;

        public string ReceiverCity;
        private string m_ReceiverStateProvinceCode;
        private string m_ReceiverCountryCode;
        private string m_ReceiverPostalCode;
        private string m_ResidentialAddressIndicator;

        private string m_PackageType;
        //public string OversizePackage;
        public string LargePackage;

        public string PkgLength;
        public string PkgWidth;
        public string PkgHeight;
        public string InsuredValue;

        private string m_ShipDate;
        public string ShipmentWeight;
        public string ShipmentCube;

        private string m_SaturdayPickup;
        private string m_SaturdayDelivery;

        private int m_RatesCount;
        private int m_TimesCount;
        private string m_RatesResultCode;
        protected string m_RatesErrorDescription;
        private string m_TimesResultCode;
        protected string m_TimesErrorDescription;
        private string m_RawRatesXMLText;
        private string m_RawRatesXMLRequestText;
        private string m_RawTimesXMLText;
        private string m_RawTimesXMLRequestText;

        // STRINGS RETURNED WITH RATE REQUEST DATA
        private string m_UPS1DAY;
        private string m_UPS2DAY;
        private string m_UPS3DAY;
        private string m_UPSGRND;
        private string m_UPS1Time;
        private string m_UPS2Time;
        private string m_UPS3Time;
        private string m_UPSGRNDTime;
        private decimal m_RLShipCharge;


        protected RateDetail UPS_RateDetail;
        protected ArrayList UPS_Rates = new ArrayList();

        protected PackageDetail UPS_PackageDetail;
        protected ArrayList UPS_Packages = new ArrayList();

        private static Regex _isNumber = new Regex(@"^\d+$");
        private static Regex pkgNumber = new Regex(@"\s\d+.$");

        private const string const_AccessLicenseNumber = "7BE35337D2B747F4";
        private const string const_UserID = "regallager";
        private const string const_Password = "V5RiJhBn";
        private const string const_RatesWebURL = "https://www.ups.com/ups.app/xml/Rate?";
        private const string const_TimesWebURL = "https://www.ups.com/ups.app/xml/TimeInTransit?";
        #endregion

        #region Enumerations
        public enum PickupTypeCode_Options
        {
            _01DailyPickup,
            _03CustomerCounter,
            _06OneTimePickup,
            _07OnCallAir,
            _11SuggestedRetailRates,
            _19LetterCenter,
            _20AirServiceCenter
        }

        public enum CustomerClassification_Options
        {
            _01WholeSale,
            _03Occasional,
            _04Retail
        }

        public enum ResidentialAddressIndicator_Options
        {
            Yes,
            No
        }

        public enum PackageType_Options
        {
            _01UPS_Letter_or_Envelope,
            _02Package,
            _03UPS_Tube,
            _04UPS_Pak,
            _21UPS_Express_Box,
            _24UPS_25Kg_Box,
            _25UPS_10Kg_Box
        }

        public enum SaturdayPckDlvry_Options
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

        public PickupTypeCode_Options PickupTypeCode
        {
            set
            {
                switch (value)
                {
                    case PickupTypeCode_Options._01DailyPickup:
                        m_PickupTypeCode = "01";
                        m_CustomerClassification = "01";
                        break;
                    case PickupTypeCode_Options._03CustomerCounter:
                        m_PickupTypeCode = "03";
                        m_CustomerClassification = "04";
                        break;
                    case PickupTypeCode_Options._06OneTimePickup:
                        m_PickupTypeCode = "06";
                        m_CustomerClassification = "03";
                        break;
                    case PickupTypeCode_Options._07OnCallAir:
                        m_PickupTypeCode = "07";
                        m_CustomerClassification = "03";
                        break;
                    case PickupTypeCode_Options._11SuggestedRetailRates:
                        m_PickupTypeCode = "11";
                        m_CustomerClassification = "03";
                        break;
                    case PickupTypeCode_Options._19LetterCenter:
                        m_PickupTypeCode = "19";
                        m_CustomerClassification = "03";
                        break;
                    case PickupTypeCode_Options._20AirServiceCenter:
                        m_PickupTypeCode = "20";
                        m_CustomerClassification = "03";
                        break;
                    default:
                        m_PickupTypeCode = "01";
                        break;
                }
            }
        }

        public CustomerClassification_Options CustomerClassification
        {
            set
            {
                switch (value)
                {
                    case CustomerClassification_Options._01WholeSale:
                        m_CustomerClassification = "01";
                        break;
                    case CustomerClassification_Options._03Occasional:
                        m_CustomerClassification = "03";
                        break;
                    case CustomerClassification_Options._04Retail:
                        m_CustomerClassification = "04";
                        break;
                    default:
                        m_CustomerClassification = "03";
                        break;
                }
            }
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

        public PackageType_Options PackageType
        {
            set
            {
                switch (value)
                {
                    case PackageType_Options._01UPS_Letter_or_Envelope:
                        m_PackageType = "01";
                        break;
                    case PackageType_Options._02Package:
                        m_PackageType = "02";
                        break;
                    case PackageType_Options._03UPS_Tube:
                        m_PackageType = "03";
                        break;
                    case PackageType_Options._04UPS_Pak:
                        m_PackageType = "04";
                        break;
                    case PackageType_Options._21UPS_Express_Box:
                        m_PackageType = "21";
                        break;
                    case PackageType_Options._24UPS_25Kg_Box:
                        m_PackageType = "24";
                        break;
                    case PackageType_Options._25UPS_10Kg_Box:
                        m_PackageType = "25";
                        break;
                    default:
                        m_PackageType = "02";
                        break;
                }
            }
        }

        public string ShipDate
        {
            set { m_ShipDate = value; }
        }

        public SaturdayPckDlvry_Options SaturdayPickup
        {
            get
            {
                switch (m_SaturdayPickup)
                {
                    case "True":
                        return SaturdayPckDlvry_Options.Yes;
                    default:
                        return SaturdayPckDlvry_Options.No;
                }
            }
            set
            {
                if (value == SaturdayPckDlvry_Options.Yes)
                {
                    m_SaturdayPickup = "True";
                }
                else
                {
                    m_SaturdayPickup = "";
                }
            }
        }

        public SaturdayPckDlvry_Options SaturdayDelivery
        {
            get
            {
                switch (m_SaturdayDelivery)
                {
                    case "True":
                        return SaturdayPckDlvry_Options.Yes;
                    default:
                        return SaturdayPckDlvry_Options.No;
                }
            }
            set
            {
                if (value == SaturdayPckDlvry_Options.Yes)
                {
                    m_SaturdayDelivery = "True";
                }
                else
                {
                    m_SaturdayDelivery = "";
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

        public string RatesResultCode
        {
            get { return m_RatesResultCode; }
        }

        public string RatesErrorDescription
        {
            get { return m_RatesErrorDescription; }
        }

        public string RawRatesXMLText
        {
            get { return m_RawRatesXMLText; }
        }

        public string RawRatesXMLRequestText
        {
            get { return m_RawRatesXMLRequestText; }
        }

        public string TimesResultCode
        {
            get { return m_TimesResultCode; }
        }

        public string TimesErrorDescription
        {
            get { return m_TimesErrorDescription; }
        }

        public string RawTimesXMLText
        {
            get { return m_RawTimesXMLText; }
        }

        public string RawTimesXMLRequestText
        {
            get { return m_RawTimesXMLRequestText; }
        }
        #endregion

        #region Structs
        public struct RateDetail : IComparable
        {
            public string UPSCode;
            public string UPSTransitCode;
            public string Description;
            public decimal ShippingCost;
            public string GuaranteedDaysToDelivery;
            public string DaysInTransit;
            public string ScheduledDeliveryDate;
            public string ScheduledDeliveryTime;
            public string[] Warnings;

            // Defines how to compare two RateDetail structs
            public int CompareTo(object obj)
            {
                if (obj is RateDetail)
                {
                    RateDetail rd = (RateDetail)obj;

                    return ShippingCost.CompareTo(rd.ShippingCost);
                }

                throw new ArgumentException("object is not a RateDetail");
            }

            public override bool Equals(Object obj)
            {
                // Check for null and compare run-time types.
                if (obj == null || GetType() != obj.GetType()) return false;

                // Compare the UPS Codes
                RateDetail rd = (RateDetail)obj;
                return UPSCode == rd.UPSCode;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

        }

        public struct PackageDetail
        {
            public PackageType_Options PackageType;
            public string Description;
            public string Length;
            public string Width;
            public string Height;
            public string Weight;
            //public string Oversize;
            public string LargePackage;
            public string InsuredValue;
        }
        #endregion

        public ups()
        {
            RatesWebURL = const_RatesWebURL;
            TimesWebURL = const_TimesWebURL;
            AccessLicenseNumber = const_AccessLicenseNumber;
            UserID = const_UserID;
            Password = const_Password;

            ProxyAddress = "";
            ShipperNumber = "";
            ShipperCountryCode = "US";
            ResidentialAddressIndicator = ResidentialAddressIndicator_Options.Yes;
            PickupTypeCode = PickupTypeCode_Options._01DailyPickup;
            PackageType = PackageType_Options._02Package;
            PkgLength = "0";
            PkgWidth = "0";
            PkgHeight = "0";
            InsuredValue = "0";
            m_ShipDate = System.DateTime.Today.ToString();
            SaturdayPickup = SaturdayPckDlvry_Options.No;
            SaturdayDelivery = SaturdayPckDlvry_Options.No;
            m_RatesCount = 0;
            m_RatesErrorDescription = "";
            m_RawRatesXMLText = "";
            m_TimesCount = 0;
            m_TimesErrorDescription = "";
            m_RawTimesXMLText = "";
        }

        public string getRates()
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

                // Rates + Service Selection Request XML
                strXML = "<?xml version='1.0'?><RatingServiceSelectionRequest xml:lang='en-US'>";
                strXML = strXML + "<Request><TransactionReference><CustomerContext>Rating and Service</CustomerContext><XpciVersion>1.0001</XpciVersion></TransactionReference>";
                strXML = strXML + "<RequestAction>Rate</RequestAction>";
                strXML = strXML + "<RequestOption>shop</RequestOption></Request>";

                // Shipper information
                strXML = strXML + "<PickupType><Code>" + m_PickupTypeCode + "</Code></PickupType>";
                strXML = strXML + "<CustomerClassification><Code>" + m_CustomerClassification + "</Code></CustomerClassification>";
                strXML = strXML + "<Shipment><Shipper>";

                if (ShipperNumber != "")
                {
                    strXML = strXML + "<ShipperNumber>" + ShipperNumber + "</ShipperNumber>";
                }

                strXML = strXML + "<Address>";
                strXML = strXML + "<City>" + ShipperCity + "</City>";
                strXML = strXML + "<StateProvinceCode>" + m_ShipperStateProvinceCode + "</StateProvinceCode>";
                strXML = strXML + "<PostalCode>" + m_ShipperPostalCode + "</PostalCode>";
                strXML = strXML + "<CountryCode>" + ShipperCountryCode + "</CountryCode>";
                strXML = strXML + "</Address></Shipper>";

                // Receiver information
                strXML = strXML + "<ShipTo><Address>";
                strXML = strXML + "<City>" + ReceiverCity + "</City>";
                strXML = strXML + "<StateProvinceCode>" + m_ReceiverStateProvinceCode + "</StateProvinceCode>";
                strXML = strXML + "<PostalCode>" + m_ReceiverPostalCode + "</PostalCode>";
                strXML = strXML + "<CountryCode>" + ReceiverCountryCode + "</CountryCode>";

                if (m_ResidentialAddressIndicator != "")
                {
                    strXML = strXML + "<ResidentialAddressIndicator></ResidentialAddressIndicator>";
                }

                strXML = strXML + "</Address></ShipTo>";

                // Shipment weight
                strXML = strXML + "<ShipmentWeight>";
                strXML = strXML + "<UnitOfMeasurement><Code>LBS</Code></UnitOfMeasurement>";
                strXML = strXML + "<Weight>" + ShipmentWeight + "</Weight>";
                strXML = strXML + "</ShipmentWeight>";

                // Service
                strXML = strXML + "<Service><Code>" + "11" + "</Code></Service>";

                if (UPS_Packages.Count > 0)
                {
                    // Multiple package information
                    foreach (PackageDetail pd in UPS_Packages)
                    {
                        // Set package type variable m_PackageType
                        PackageType = pd.PackageType;

                        // Build package XML
                        strXML = strXML + "<Package>";
                        strXML = strXML + "<PackagingType><Code>" + m_PackageType + "</Code>";
                        strXML = strXML + "<Description>" + pd.Description + "</Description></PackagingType>";

                        strXML = strXML + "<Dimensions>";
                        strXML = strXML + "<UnitOfMeasurement><Code>IN</Code></UnitOfMeasurement>";
                        strXML = strXML + "<Length>" + pd.Length + "</Length>";
                        strXML = strXML + "<Width>" + pd.Width + "</Width>";
                        strXML = strXML + "<Height>" + pd.Height + "</Height>";
                        strXML = strXML + "</Dimensions>";

                        strXML = strXML + "<PackageWeight>";
                        strXML = strXML + "<UnitOfMeasurement><Code>LBS</Code></UnitOfMeasurement>";
                        strXML = strXML + "<Weight>" + pd.Weight + "</Weight>";
                        strXML = strXML + "</PackageWeight>";

                        //strXML = strXML + "<OversizePackage>" + pd.Oversize + "</OversizePackage>";

                        if (pd.LargePackage != "")
                        {
                            strXML = strXML + "<LargePackageIndicator></LargePackageIndicator>";
                        }

                        strXML = strXML + "<PackageServiceOptions><InsuredValue><CurrencyCode>USD</CurrencyCode>";
                        strXML = strXML + "<MonetaryValue>" + pd.InsuredValue + "</MonetaryValue></InsuredValue></PackageServiceOptions>";
                        strXML = strXML + "</Package>";
                    }
                }
                else
                {
                    // Single package information
                    // Build package XML
                    strXML = strXML + "<Package>";
                    strXML = strXML + "<PackagingType><Code>" + m_PackageType + "</Code>";
                    strXML = strXML + "<Description>Package</Description></PackagingType>";

                    strXML = strXML + "<Dimensions>";
                    strXML = strXML + "<UnitOfMeasurement><Code>IN</Code></UnitOfMeasurement>";
                    strXML = strXML + "<Length>" + PkgLength + "</Length>";
                    strXML = strXML + "<Width>" + PkgWidth + "</Width>";
                    strXML = strXML + "<Height>" + PkgHeight + "</Height>";
                    strXML = strXML + "</Dimensions>";

                    strXML = strXML + "<PackageWeight>";
                    strXML = strXML + "<UnitOfMeasurement><Code>LBS</Code></UnitOfMeasurement>";
                    strXML = strXML + "<Weight>" + ShipmentWeight + "</Weight>";
                    strXML = strXML + "</PackageWeight>";

                    //strXML = strXML + "<OversizePackage>" + OversizePackage + "</OversizePackage>";

                    if (LargePackage != "")
                    {
                        strXML = strXML + "<LargePackageIndicator></LargePackageIndicator>";
                    }

                    strXML = strXML + "<PackageServiceOptions><InsuredValue><CurrencyCode>USD</CurrencyCode>";
                    strXML = strXML + "<MonetaryValue>" + InsuredValue + "</MonetaryValue></InsuredValue></PackageServiceOptions>";
                    strXML = strXML + "</Package>";
                }

                // Shipment service options
                strXML = strXML + "<ShipmentServiceOptions>";

                if (m_SaturdayPickup != "")
                {
                    strXML = strXML + "<SaturdayPickupIndicator></SaturdayPickupIndicator>";
                }

                if (m_SaturdayDelivery != "")
                {
                    strXML = strXML + "<SaturdayDeliveryIndicator></SaturdayDeliveryIndicator>";
                }

                strXML = strXML + "</ShipmentServiceOptions>";
                strXML = strXML + "</Shipment>";
                strXML = strXML + "</RatingServiceSelectionRequest>";

                m_RawRatesXMLRequestText = strXML;

                // ***************************
                // Send request and retrieve response
                // ***************************

                // Create web request
                HttpWebRequest rateRequest = (HttpWebRequest)HttpWebRequest.Create(RatesWebURL);

                // Create the proxy class instance
                if (ProxyAddress != "")
                {
                    IWebProxy prxy = new WebProxy(ProxyAddress);
                    rateRequest.Proxy = prxy;
                }

                rateRequest.AllowAutoRedirect = false;
                rateRequest.Method = "POST";
                rateRequest.ContentType = "application/x-www-form-urlencoded";

                // Create POST stream
                Stream RequestStream = rateRequest.GetRequestStream();
                Byte[] SomeBytes = Encoding.UTF8.GetBytes(strXML_AccessRequest + strXML);
                RequestStream.Write(SomeBytes, 0, SomeBytes.Length);
                RequestStream.Close();

                // Send request and get response
                HttpWebResponse rateResponse = (HttpWebResponse)rateRequest.GetResponse();

                if (rateResponse.StatusCode == HttpStatusCode.OK)
                {
                    // Get the stream
                    Stream ResponseStream = rateResponse.GetResponseStream();
                    Encoding charEncoding = Encoding.GetEncoding("utf-8");

                    // Send the stream to a reader
                    StreamReader readStream = new StreamReader(ResponseStream, charEncoding);

                    // Read the response
                    string Result = readStream.ReadToEnd();
                    m_RawRatesXMLText = Result;
                    rateResponse.Close();
                }

                // ***************************
                // Begin parsing rate response
                // ***************************

                // Load response XML into an XML document
                System.Xml.XmlDocument responseXMLDocument;
                responseXMLDocument = new System.Xml.XmlDocument();
                responseXMLDocument.LoadXml(m_RawRatesXMLText);

                // Check for errors
                System.Xml.XmlNode Success_Node;
                Success_Node = responseXMLDocument.SelectSingleNode("/RatingServiceSelectionResponse/Response/ResponseStatusCode");

                if (Success_Node.InnerText != "1")
                {
                    m_RatesResultCode = "1";
                    m_RatesErrorDescription = responseXMLDocument.SelectSingleNode("/RatingServiceSelectionResponse/Response/Error/ErrorDescription").InnerText;
                }
                else
                {
                    // No errors. Get list of rates
                    System.Xml.XmlNodeList RatedShipment_NodeList;
                    RatedShipment_NodeList = responseXMLDocument.SelectNodes("/RatingServiceSelectionResponse/RatedShipment");
                    m_RatesCount = RatedShipment_NodeList.Count;

                    // Loop through rates and build rate detail
                    for (int i = 0; i < m_RatesCount; i++)
                    {
                        UPS_RateDetail.UPSCode = RatedShipment_NodeList[i].SelectSingleNode("Service/Code").InnerText;
                        UPS_RateDetail.UPSTransitCode = "";
                        m_RLShipCharge = System.Convert.ToDecimal(RatedShipment_NodeList[i].SelectSingleNode("TotalCharges/MonetaryValue").InnerText);
                        UPS_RateDetail.ShippingCost = Decimal.Multiply(m_RLShipCharge,Convert.ToDecimal(1.2));

                        // Now using Time In Transit to obtain information
                        UPS_RateDetail.GuaranteedDaysToDelivery = RatedShipment_NodeList[i].SelectSingleNode("GuaranteedDaysToDelivery").InnerText;
                        UPS_RateDetail.ScheduledDeliveryTime = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;

                        switch (UPS_RateDetail.UPSCode)
                        {
                            case "01":
                                UPS_RateDetail.Description = "UPS Next Day Air";
                                m_UPS1DAY = Convert.ToString(UPS_RateDetail.ShippingCost);
                                m_UPS1Time = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;
                                break;
                            case "02":
                                UPS_RateDetail.Description = "UPS 2nd Day Air";
                                m_UPS2DAY = Convert.ToString(UPS_RateDetail.ShippingCost);
                                m_UPS2Time = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;
                                break;
                            case "03":
                                UPS_RateDetail.Description = "UPS Ground";
                                m_UPSGRND = Convert.ToString(UPS_RateDetail.ShippingCost);
                                m_UPSGRNDTime = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;
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
                                m_UPS3DAY = Convert.ToString(UPS_RateDetail.ShippingCost);
                                m_UPS3Time = RatedShipment_NodeList[i].SelectSingleNode("ScheduledDeliveryTime").InnerText;
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
                        /*if(UPS_RateDetail.UPSCode == "03")
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
                                                PackageDetail[] tempPkgs = UPSPackages;
                                                tempPkgs[Convert.ToInt32(temp)-1].LargePackage = warning.Substring(0,10);
                                                UPS_Packages = ArrayList.Adapter(tempPkgs);
                                            }
                                        }
                                    }
                                }
                            }
                        }*/

                        UPS_Rates.Add(UPS_RateDetail);
                    }

                    // Sort all of the rates in order of shipping cost
                    UPS_Rates.Sort();

                    m_RatesResultCode = "0";
                    m_RatesErrorDescription = "";
                }
            }
            catch (Exception ex)
            {
                m_RatesResultCode = "2";
                m_RatesErrorDescription = ex.Message;
            }
            //getTimes();
            return Math.Round(Convert.ToDecimal(m_UPS1DAY), 2).ToString() + "/" + m_UPS1Time + "/" +
                Math.Round(Convert.ToDecimal(m_UPS2DAY), 2).ToString() + "/" + m_UPS2Time + "/" +
                Math.Round(Convert.ToDecimal(m_UPS3DAY), 2).ToString() + "/" + m_UPS3Time + "/" + 
                Math.Round(Convert.ToDecimal(m_UPSGRND), 2).ToString() + "/" + m_UPSGRNDTime;
           
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

                // Specify total number of packages if UPS_Packages contains any packages
                // Otherwise, total packages defaults to one
                if (UPS_Packages.Count > 0)
                {
                    strXML = strXML + "<TotalPackagesInShipment>" + UPS_Packages.Count.ToString() + "</TotalPackagesInShipment>";
                }

                strXML = strXML + "</TimeInTransitRequest>";

                m_RawTimesXMLRequestText = strXML;

                // ***************************
                // Send request and retrieve response
                // ***************************

                // Create web request
                HttpWebRequest timeRequest = (HttpWebRequest)HttpWebRequest.Create(TimesWebURL);

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
                    m_RawTimesXMLText = Result;
                    timeResponse.Close();
                }

                // ***************************
                // Begin parsing time response
                // ***************************

                // Load response XML into an XML document
                System.Xml.XmlDocument responseXMLDocument;
                responseXMLDocument = new System.Xml.XmlDocument();
                responseXMLDocument.LoadXml(m_RawTimesXMLText);

                // Check for errors
                System.Xml.XmlNode Success_Node;
                Success_Node = responseXMLDocument.SelectSingleNode("/TimeInTransitResponse/Response/ResponseStatusCode");

                if (Success_Node.InnerText != "1")
                {
                    m_TimesResultCode = "1";
                    m_TimesErrorDescription = responseXMLDocument.SelectSingleNode("/TimeInTransitResponse/Response/Error/ErrorDescription").InnerText;
                }
                else
                {
                    // No errors. Get list of times
                    System.Xml.XmlNodeList ServiceSummary_NodeList;
                    ServiceSummary_NodeList = responseXMLDocument.SelectNodes("/TimeInTransitResponse/TransitResponse/ServiceSummary");
                    m_TimesCount = ServiceSummary_NodeList.Count;

                    ArrayList tempRates = new ArrayList();
                    int rdIndex = -1;

                    // Loop through transit times and add information to corresponding rate detail
                    for (int i = 0; i < m_TimesCount; i++)
                    {
                        UPS_RateDetail.UPSCode = "";
                        UPS_RateDetail.UPSTransitCode = "";

                        switch (ServiceSummary_NodeList[i].SelectSingleNode("Service/Code").InnerText)
                        {
                            case "1DA":
                                if (m_SaturdayDelivery == "")
                                {
                                    UPS_RateDetail.UPSCode = "01";
                                    UPS_RateDetail.UPSTransitCode = "1DA";
                                    UPS_RateDetail.Description = "UPS Next Day Air";
                                }
                                break;
                            case "2DA":
                                if (m_SaturdayDelivery == "")
                                {
                                    UPS_RateDetail.UPSCode = "02";
                                    UPS_RateDetail.UPSTransitCode = "2DA";
                                    UPS_RateDetail.Description = "UPS 2nd Day Air";
                                }
                                break;
                            case "GND":
                                UPS_RateDetail.UPSCode = "03";
                                UPS_RateDetail.UPSTransitCode = "GND";
                                UPS_RateDetail.Description = "UPS Ground";
                                break;
                            case "01":
                                UPS_RateDetail.UPSCode = "07";
                                UPS_RateDetail.UPSTransitCode = "01";
                                UPS_RateDetail.Description = "UPS Worldwide ExpressSM";
                                break;
                            case "05":
                                UPS_RateDetail.UPSCode = "08";
                                UPS_RateDetail.UPSTransitCode = "05";
                                UPS_RateDetail.Description = "UPS Worldwide ExpeditedSM";
                                break;
                            case "03":
                                UPS_RateDetail.UPSCode = "11";
                                UPS_RateDetail.UPSTransitCode = "03";
                                UPS_RateDetail.Description = "UPS Standard UPS";
                                break;
                            case "3DS":
                                UPS_RateDetail.UPSCode = "12";
                                UPS_RateDetail.UPSTransitCode = "3DS";
                                UPS_RateDetail.Description = "UPS 3 Day Select";
                                break;
                            case "1DP":
                                UPS_RateDetail.UPSCode = "13";
                                UPS_RateDetail.UPSTransitCode = "1DP";
                                UPS_RateDetail.Description = "UPS Next Day Air Saver";
                                break;
                            case "1DM":
                                if (m_SaturdayDelivery == "")
                                {
                                    UPS_RateDetail.UPSCode = "14";
                                    UPS_RateDetail.UPSTransitCode = "1DM";
                                    UPS_RateDetail.Description = "UPS Next Day Air Early A.M.";
                                }
                                break;
                            case "21":
                                UPS_RateDetail.UPSCode = "54";
                                UPS_RateDetail.UPSTransitCode = "21";
                                UPS_RateDetail.Description = "UPS Worldwide Express PlusSM";
                                break;
                            case "2DM":
                                UPS_RateDetail.UPSCode = "59";
                                UPS_RateDetail.UPSTransitCode = "2DM";
                                UPS_RateDetail.Description = "UPS 2nd Day Air A.M.";
                                break;
                            case "1DAS":
                                if (m_SaturdayDelivery != "")
                                {
                                    UPS_RateDetail.UPSCode = "01";
                                    UPS_RateDetail.UPSTransitCode = "1DAS";
                                    UPS_RateDetail.Description = "UPS Next Day Air (Saturday Delivery)";
                                }
                                break;
                            case "2DAS":
                                if (m_SaturdayDelivery != "")
                                {
                                    UPS_RateDetail.UPSCode = "02";
                                    UPS_RateDetail.UPSTransitCode = "2DAS";
                                    UPS_RateDetail.Description = "UPS 2nd Day Air (Saturday Delivery)";
                                }
                                break;
                            case "1DMS":
                                if (m_SaturdayDelivery != "")
                                {
                                    UPS_RateDetail.UPSCode = "14";
                                    UPS_RateDetail.UPSTransitCode = "1DMS";
                                    UPS_RateDetail.Description = "UPS Next Day Air Early A.M. (Saturday Delivery)";
                                }
                                break;
                            default:
                                UPS_RateDetail.UPSCode = "";
                                UPS_RateDetail.UPSTransitCode = "";
                                UPS_RateDetail.Description = "Unknown Service Code";
                                break;
                        }

                        // Check to see if rate detail exist, if so, update information and add to tempRates
                        rdIndex = UPS_Rates.IndexOf(UPS_RateDetail);
                        if (rdIndex != -1)
                        {
                            RateDetail temp = (RateDetail)UPS_Rates[rdIndex];
                            temp.UPSTransitCode = UPS_RateDetail.UPSTransitCode;
                            temp.DaysInTransit = ServiceSummary_NodeList[i].SelectSingleNode("EstimatedArrival/BusinessTransitDays").InnerText;
                            temp.ScheduledDeliveryDate = Convert.ToDateTime(ServiceSummary_NodeList[i].SelectSingleNode("EstimatedArrival/Date").InnerText).ToLongDateString();
                            temp.ScheduledDeliveryTime = ServiceSummary_NodeList[i].SelectSingleNode("EstimatedArrival/Time").InnerText;
                            temp.Description = UPS_RateDetail.Description;

                            if (temp.ScheduledDeliveryTime == "23:00:00")
                            {
                                temp.ScheduledDeliveryTime = "End of Day";
                            }
                            else
                            {
                                temp.ScheduledDeliveryTime = Convert.ToDateTime(temp.ScheduledDeliveryTime).ToShortTimeString();
                            }

                            tempRates.Add(temp);
                        }
                    }

                    // Update UPS Rates to only rates that returned a transit time
                    // Other rates are not neccesary
                    UPS_Rates = tempRates;
                    UPS_Rates.Sort();

                    if (UPS_Rates.Count == 0 && m_RatesErrorDescription == "")
                    {
                        m_RatesResultCode = "2";
                        m_RatesErrorDescription = "Shipping option is not possible!";                        
                    }

                    m_TimesResultCode = "0";
                    m_TimesErrorDescription = "";
                }
            }
            catch (Exception ex)
            {
                m_TimesResultCode = "2";
                m_TimesErrorDescription = ex.Message;
            }
        }

        public static bool IsInteger(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }
    }
}