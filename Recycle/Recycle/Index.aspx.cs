using System;
using System.Linq;
using System.IO;
using System.Xml;
using System.Net;

using System.Data;
using System.Data.SqlClient;

namespace Recycle
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SubmitAddress();
            }
            else
            {
                // Connect to the database, get the data
                string connString = "Data Source=(local);Initial Catalog=dbRecycle;Persist Security Info=True;User ID=daystar;Password=daystar";
                string query = "select * from tblLocationData";

                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                var dataTable = new DataTable();

                da.Fill(dataTable);
                conn.Close();
                da.Dispose();
                BuildScript(dataTable);

            }

        }

        protected void SubmitAddress()
        {
            Address adrs = new Address();
            adrs.FullAddress = txtAddress.Value.ToString();
            try
            {
                adrs.GeoCode();
                //Response.Write(adrs.Latitude + "; " + adrs.Longitude);

                // clean the boxes
                txtAddress.Value = "";
                txtComment.Value = "";
                lblMsg.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured" + ex.Message);
            }

        }
    

    private void BuildScript(DataTable tbl)
        {
            String Locations = "";
            foreach (DataRow r in tbl.Rows)
            {
                // bypass empty rows	 	
                if (r["locLatitude"].ToString().Trim().Length == 0)
                    continue;

                string Latitude = r["locLatitude"].ToString();
                string Longitude = r["locLongitude"].ToString();

                // create a line of JavaScript for marker on map for this record	
                Locations += Environment.NewLine + " map.addOverlay(new GMarker(new GLatLng(" + Latitude + "," + Longitude + ")));";
            }

            // construct the final script
            js.Text = @"<script type='text/javascript'>
                            function initialize() {
                                if (GBrowserIsCompatible()) {
                                    var map = new GMap2(document.getElementById('map_canvas'));
                                    map.setCenter(new GLatLng(39.5254004, -119.8135266), 12); 
                                    " + Locations + @"
                                    map.setUIToDefault();
                                    map.panTo(new GLatLng(39.5254004, -119.8135266));
                                }
                            }
                            </script> ";
        }       
}
    public class Address
    {
        public Address()
        {
        }
        //Properties
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FullAddress { get; set; }

        //The Geocoding here i.e getting the latt/long of address
        public void GeoCode()
        {
            //to Read the Stream
            StreamReader sr = null;

            //The Google Maps API Either return JSON or XML. We are using XML Here
            //Saving the url of the Google API 
            string url = String.Format("http://maps.googleapis.com/maps/api/geocode/xml?address=" + this.FullAddress + "&sensor=false");

            //to Send the request to Web Client 
            WebClient wc = new WebClient();
            try
            {
                sr = new StreamReader(wc.OpenRead(url));
            }
            catch (Exception ex)
            {
                throw new Exception("The Error Occured" + ex.Message);
            }

            try
            {
                XmlTextReader xmlReader = new XmlTextReader(sr);
                bool latread = false;
                bool longread = false;

                while (xmlReader.Read())
                {
                    xmlReader.MoveToElement();
                    switch (xmlReader.Name)
                    {
                        case "lat":

                            if (!latread)
                            {
                                xmlReader.Read();
                                this.Latitude = xmlReader.Value.ToString();
                                latread = true;

                            }
                            break;
                        case "lng":
                            if (!longread)
                            {
                                xmlReader.Read();
                                this.Longitude = xmlReader.Value.ToString();
                                longread = true;
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured" + ex.Message);
            }
        }
    }
}