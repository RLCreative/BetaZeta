using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShippingRates
{
    #region Structs
    public struct FreightClassDetail : IComparable
    {
        public double freightClass;
        public double weight;
        public double cube;

        // Defines how to compare two FreightClassDetail structs
        public int CompareTo(object obj)
        {
            if (obj is FreightClassDetail)
            {
                FreightClassDetail fcd = (FreightClassDetail)obj;

                return freightClass.CompareTo(fcd.freightClass);
            }

            throw new ArgumentException("object is not a FreightClassDetail");
        }

        public override bool Equals(Object obj)
        {
            // Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;

            // Compare the Freight Class codes
            FreightClassDetail fcd = (FreightClassDetail)obj;
            return freightClass == fcd.freightClass;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    #endregion

    /// <summary>
    /// Retrieve estimated UPS shipping rates for an order from Great Plains.
    /// </summary>
    public class ups_GPOrder : ups
    {
        #region Variables
        private string m_OrderID;
        private Guid m_GUID;
        private string m_OrderDate;
        private string m_ReqShipDate;
        private string m_SiteID;
        private string m_PO;
        private string m_CustomerID;
        private string m_ShipToID;
        private string m_Name;
        private string m_Contact;
        private string m_Address1;
        private string m_Address2;
        private string m_Address3;
        private string m_City;
        private string m_State;
        private string m_Zip;
        private string m_Country;
        private string m_Phone;
        private string m_ShipMethod;
        private FreightClassDetail shipment_FCDetail;
        private ArrayList Freight_Classes = new ArrayList();

        private const string const_ShipperNumber = "30X315";
        private const string const_ShipperCity = "KENNESAW";
        private const string const_ShipperStateProvinceCode = "GA";
        private const string const_ShipperCountryCode = "US";
        private const string const_ShipperPostalCode = "30144";
        private const ups.PickupTypeCode_Options const_PickupTypeCode = ups.PickupTypeCode_Options._01DailyPickup;
        private const ups.PackageType_Options const_PackageType = ups.PackageType_Options._02Package;

        private const int miscPackageMaxWeight = 30;
        #endregion

        #region Accessors
        public string OrderID
        {
            get { return m_OrderID; }
            set { m_OrderID = value; }
        }

        public Guid GUID
        {
            get { return m_GUID; }
            set { m_GUID = value; }
        }

        public string OrderDate
        {
            get { return m_OrderDate; }
        }

        public string ReqShipDate
        {
            get { return m_ReqShipDate; }
        }

        public string SiteID
        {
            get { return m_SiteID; }
        }

        public string PO
        {
            get { return m_PO; }
        }

        public string CustomerID
        {
            get { return m_CustomerID; }
        }

        public string ShipToID
        {
            get { return m_ShipToID; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public string Contact
        {
            get { return m_Contact; }
        }

        public string Address1
        {
            get { return m_Address1; }
        }

        public string Address2
        {
            get { return m_Address2; }
        }

        public string Address3
        {
            get { return m_Address3; }
        }

        public string City
        {
            get { return m_City; }
        }

        public string State
        {
            get { return m_State; }
        }

        public string Zip
        {
            get { return m_Zip; }
        }

        public string Country
        {
            get { return m_Country; }
        }

        public string Phone
        {
            get
            {
                if (m_Phone.Length >= 10)
                {
                    return string.Format("({0}) {1}-{2}", m_Phone.Substring(0, 3), m_Phone.Substring(3, 3), m_Phone.Substring(6, 4));
                }
                else
                {
                    return "000-000-0000";
                }
            }
        }

        public string ShipMethod
        {
            get { return m_ShipMethod; }
        }

        public FreightClassDetail[] FreightClasses
        {
            get
            {
                FreightClassDetail[] fcs = new FreightClassDetail[Freight_Classes.Count];
                Freight_Classes.Sort();
                Freight_Classes.CopyTo(fcs);
                return fcs;
            }
        }
        #endregion

        public ups_GPOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void estimateFreight(string orderID, Guid GUID)
        {
            SqlDataReader sdr;
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
            SqlCommand cmd;

            m_OrderID = orderID;
            m_GUID = GUID;

            // Retrieve order info
            cmd = new SqlCommand("ShippingRates_OrderInfoDropShip", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", SqlDbType.VarChar, 31);
            cmd.Parameters["@OrderID"].Value = m_OrderID;

            conn.Open();
            sdr = cmd.ExecuteReader();

            // Make sure the order was retrieved
            if (sdr.HasRows)
            {
                // Populate order info variables
                while (sdr.Read())
                {
                    m_OrderDate = sdr.GetDateTime(1).ToShortDateString();
                    m_ReqShipDate = sdr.GetDateTime(2).ToShortDateString();
                    m_SiteID = sdr.GetString(3).Trim();
                    m_PO = sdr.GetString(4).Trim();
                    m_CustomerID = sdr.GetString(5).Trim();
                    m_ShipToID = sdr.GetString(6).Trim();
                    m_Name = sdr.GetString(7).Trim();
                    m_Contact = sdr.GetString(8).Trim();
                    m_Address1 = sdr.GetString(9).Trim();
                    m_Address2 = sdr.GetString(10).Trim();
                    m_Address3 = sdr.GetString(11).Trim();
                    m_City = sdr.GetString(12).Trim();
                    m_State = sdr.GetString(13).Trim();
                    m_Zip = sdr.GetString(14).Trim();
                    m_Country = sdr.GetString(15).Trim();
                    m_Phone = sdr.GetString(16).Trim();
                    m_ShipMethod = sdr.GetString(17).Trim();

                    // Check the Address Type to see if the shipto is a Residential address
                    if (sdr.GetString(18).Trim().ToUpper().IndexOf('R') != -1)
                    {
                        ResidentialAddressIndicator = ResidentialAddressIndicator_Options.Yes;
                    }
                }

                sdr.Close();
                conn.Close();

                if (m_SiteID.Trim() != "RV STORAGE")
                {
                    cmd = new SqlCommand("ShippingRates_ItemInfoDropShip", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@GUID"].Value = m_GUID;

                    conn.Open();
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        int pkgCount = 0;
                        double shipmentWeight = 0;
                        double shipmentCube = 0;

                        int Boxes = 0;
                        int Qty = 0;
                        int MP = 0;
                        double MPWeight = 0;
                        double MPCube = 0;
                        int IP = 0;
                        double IPWeight = 0;
                        double IPCube = 0;
                        bool MPShippable = false;	// Is the Master Pack shippable
                        bool IPShippable = false;	// Is the Inner Pack shippable
                        double ItemFC = 0;
                        double weightLeftOver = 0;

                        // Calculate number of packages and create package detail
                        while (sdr.Read())
                        {
                            Boxes = 0;
                            Qty = Convert.ToInt32(sdr.GetDecimal(2));
                            MP = Convert.ToInt32(sdr.GetDecimal(3));
                            MPWeight = Convert.ToDouble(sdr.GetDecimal(7));
                            MPCube = Convert.ToDouble(sdr.GetDecimal(8));
                            IP = Convert.ToInt32(sdr.GetDecimal(9));
                            IPWeight = Convert.ToDouble(sdr.GetDecimal(13));
                            IPCube = Convert.ToDouble(sdr.GetDecimal(14));
                            MPShippable = Convert.ToBoolean(sdr.GetBoolean(15));
                            IPShippable = Convert.ToBoolean(sdr.GetBoolean(16));
                            ItemFC = Convert.ToDouble(Math.Round(sdr.GetDecimal(17), 0));

                            if (IPCube == 0)
                            {
                                IPCube = 1.59;  // Default to 14"x14"x14" box
                            }

                            if (Qty >= MP && MP != 0 && MPShippable == true)
                            {
                                Boxes = Qty / MP;

                                // Create a package for each complete master pack box
                                for (int j = 0; j < Boxes; j++)
                                {
                                    pkgCount++;
                                    shipmentWeight = shipmentWeight + MPWeight;
                                    shipmentCube = shipmentCube + MPCube;

                                    UPS_PackageDetail.PackageType = PackageType_Options._02Package;
                                    UPS_PackageDetail.Description = "Package " + pkgCount.ToString();
                                    UPS_PackageDetail.Length = Convert.ToString(Math.Round(sdr.GetDecimal(4), 0));
                                    UPS_PackageDetail.Width = Convert.ToString(Math.Round(sdr.GetDecimal(5), 0));
                                    UPS_PackageDetail.Height = Convert.ToString(Math.Round(sdr.GetDecimal(6), 0));
                                    UPS_PackageDetail.Weight = Convert.ToString(Math.Round(MPWeight, 1));
                                    //UPS_PackageDetail.Oversize = "";

                                    if (Math.Round(sdr.GetDecimal(4), 0) + (2 * Math.Round(sdr.GetDecimal(5), 0)) + (2 * Math.Round(sdr.GetDecimal(6), 0)) >= 130 && Math.Round(sdr.GetDecimal(4), 0) + (2 * Math.Round(sdr.GetDecimal(5), 0)) + (2 * Math.Round(sdr.GetDecimal(6), 0)) <= 165)
                                    {
                                        UPS_PackageDetail.LargePackage = "Large Package";
                                    }
                                    else
                                    {
                                        UPS_PackageDetail.LargePackage = "";
                                    }

                                    UPS_PackageDetail.InsuredValue = "0";
                                    UPS_Packages.Add(UPS_PackageDetail);
                                    this.calcFreightClass(ItemFC, MPWeight, MPCube);
                                }

                                // Take the remaining quantity and multiply it by the weight for 1 piece
                                // To get the weight per each, divide the IPWeight by the IP quantity
                                // Same for the cube
                                if (IP != 0)
                                {
                                    weightLeftOver = weightLeftOver + ((Qty % MP) * (IPWeight / IP));
                                    shipmentCube = shipmentCube + ((Qty % MP) * (IPCube / IP));
                                    this.calcFreightClass(ItemFC, (Qty % MP) * (IPWeight / IP), (Qty % MP) * (IPCube / IP));
                                }
                            }
                            else if (Qty >= IP && IP != 0 && IPShippable == true)
                            {
                                Boxes = Qty / IP;

                                // Create a package for each complete inner pack box
                                for (int j = 0; j < Boxes; j++)
                                {
                                    pkgCount++;
                                    shipmentWeight = shipmentWeight + IPWeight;
                                    shipmentCube = shipmentCube + IPCube;

                                    UPS_PackageDetail.PackageType = PackageType_Options._02Package;
                                    UPS_PackageDetail.Description = "Package " + pkgCount.ToString();
                                    UPS_PackageDetail.Length = Convert.ToString(Math.Round(sdr.GetDecimal(10), 0));
                                    UPS_PackageDetail.Width = Convert.ToString(Math.Round(sdr.GetDecimal(11), 0));
                                    UPS_PackageDetail.Height = Convert.ToString(Math.Round(sdr.GetDecimal(12), 0));
                                    UPS_PackageDetail.Weight = Convert.ToString(Math.Round(IPWeight, 1));
                                    //UPS_PackageDetail.Oversize = "";

                                    if (Math.Round(sdr.GetDecimal(10), 0) + (2 * Math.Round(sdr.GetDecimal(11), 0)) + (2 * Math.Round(sdr.GetDecimal(12), 0)) >= 130 && Math.Round(sdr.GetDecimal(10), 0) + (2 * Math.Round(sdr.GetDecimal(11), 0)) + (2 * Math.Round(sdr.GetDecimal(12), 0)) <= 165)
                                    {
                                        UPS_PackageDetail.LargePackage = "Large Package";
                                    }
                                    else
                                    {
                                        UPS_PackageDetail.LargePackage = "";
                                    }

                                    UPS_PackageDetail.InsuredValue = "0";
                                    UPS_Packages.Add(UPS_PackageDetail);
                                    this.calcFreightClass(ItemFC, IPWeight, IPCube);
                                }

                                // Take the remaining quantity and multiply it by the weight for 1 piece
                                // To get the weight per each, divide the IPWeight by the IP quantity
                                // Same for the cube
                                weightLeftOver = weightLeftOver + ((Qty % IP) * (IPWeight / IP));
                                shipmentCube = shipmentCube + ((Qty % IP) * (IPCube / IP));
                                this.calcFreightClass(ItemFC, (Qty % IP) * (IPWeight / IP), (Qty % IP) * (IPCube / IP));
                            }
                            else if (IP != 0)
                            {
                                // Take the quantity and multiply it by the weight for 1 piece
                                // To get the weight per each, divide the IPWeight by the IP quantity
                                // Same for the cube
                                weightLeftOver = weightLeftOver + (Qty * (IPWeight / IP));
                                shipmentCube = shipmentCube + (Qty * (IPCube / IP));
                                this.calcFreightClass(ItemFC, Qty * (IPWeight / IP), Qty * (IPCube / IP));
                            }
                        }

                        sdr.Close();
                        conn.Close();

                        // Put left over weight into packages
                        Boxes = Convert.ToInt32(Convert.ToInt32(Math.Ceiling(weightLeftOver)) / miscPackageMaxWeight);

                        // Create a package for each misc box equal to the miscPackageMaxWeight
                        for (int j = 0; j < Boxes; j++)
                        {
                            pkgCount++;
                            shipmentWeight = shipmentWeight + miscPackageMaxWeight;

                            UPS_PackageDetail.PackageType = PackageType_Options._02Package;
                            UPS_PackageDetail.Description = "Package " + pkgCount.ToString();

                            //***Default to 18" cubed box.***
                            UPS_PackageDetail.Length = "18";
                            UPS_PackageDetail.Width = "18";
                            UPS_PackageDetail.Height = "18";

                            UPS_PackageDetail.Weight = miscPackageMaxWeight.ToString();
                            //UPS_PackageDetail.Oversize = "";
                            UPS_PackageDetail.LargePackage = "";
                            UPS_PackageDetail.InsuredValue = "0";
                            UPS_Packages.Add(UPS_PackageDetail);
                        }

                        // Create final package
                        if (Math.Ceiling(weightLeftOver) % miscPackageMaxWeight > 0)
                        {
                            pkgCount++;
                            shipmentWeight = shipmentWeight + Math.Ceiling(weightLeftOver) % miscPackageMaxWeight;

                            UPS_PackageDetail.PackageType = PackageType_Options._02Package;
                            UPS_PackageDetail.Description = "Package " + pkgCount.ToString();

                            //***Default to 14" cubed box.***
                            UPS_PackageDetail.Length = "14";
                            UPS_PackageDetail.Width = "14";
                            UPS_PackageDetail.Height = "14";

                            UPS_PackageDetail.Weight = Convert.ToString(Math.Ceiling(weightLeftOver) % miscPackageMaxWeight);
                            //UPS_PackageDetail.Oversize = "";
                            UPS_PackageDetail.LargePackage = "";
                            UPS_PackageDetail.InsuredValue = "0";
                            UPS_Packages.Add(UPS_PackageDetail);
                        }

                        // Get the rates
                        PickupTypeCode = const_PickupTypeCode;
                        PackageType = const_PackageType;
                        ShipperNumber = const_ShipperNumber;
                        ShipperCity = const_ShipperCity;
                        ShipperStateProvinceCode = const_ShipperStateProvinceCode;
                        ShipperCountryCode = const_ShipperCountryCode;
                        ShipperPostalCode = const_ShipperPostalCode;
                        ReceiverCity = m_City;
                        ReceiverStateProvinceCode = m_State;
                        ReceiverCountryCode = m_Country;
                        ReceiverPostalCode = m_Zip;
                        ShipDate = m_ReqShipDate;
                        ShipmentWeight = Convert.ToString(Math.Round(shipmentWeight, 2));

                        shipmentCube = Math.Round(shipmentCube, 2);
                        ShipmentCube = shipmentCube.ToString();

                        if (this.SaturdayDelivery == ups.SaturdayPckDlvry_Options.Yes)
                        {
                            this.SaturdayDelivery = SaturdayPckDlvry_Options.Yes;
                        }

                        getRates();
                    }
                    else
                    {
                        // No items found
                        m_RatesErrorDescription = "No items found!";
                        sdr.Close();
                        conn.Close();
                    }
                }
                else
                {
                    // Can not estimate UPS charges when shipping from RV Storage
                    m_RatesErrorDescription = "Rates can not be calculated for orders shipping from RV Storage!";
                }
            }
            else
            {
                // Order not found
                m_RatesErrorDescription = "Order not found!";
                sdr.Close();
                conn.Close();
            }
        }

        private void calcFreightClass(double strFreightClass, double dblWeight, double dblCube)
        {
            int index = -1;
            FreightClassDetail tempFC;

            shipment_FCDetail.freightClass = strFreightClass;
            index = Freight_Classes.IndexOf(shipment_FCDetail);

            if (index >= 0)
            {
                tempFC = (FreightClassDetail)Freight_Classes[index];
                tempFC.weight += dblWeight;
                tempFC.cube += dblCube;
                Freight_Classes[index] = tempFC;
            }
            else
            {
                shipment_FCDetail.weight = dblWeight;
                shipment_FCDetail.cube = dblCube;
                Freight_Classes.Add(shipment_FCDetail);
            }
        }
    }
}