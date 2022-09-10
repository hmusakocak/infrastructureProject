using HtmlAgilityPack;
using infrastructure_inquiry.function;
using infrastructure_inquiry.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace infrastructure_inquiry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringReader sr = new StringReader(Resources.cities_of_turkey);

            string jsondata = sr.ReadToEnd();

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsondata);

            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (var item in myDeserializedClass.List)
            {
                map.Add(item.Name, item.Id);
            }

            comboBox1.DataSource = new BindingSource(map, null);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";

        }

        public HttpClient client = new HttpClient();

        public AddressParams address = new AddressParams();

        public request request = new request();

        private  void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox1, comboBox2 , "GetDistricts" , "İlçe");

        }

        private  void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox2, comboBox3, "GetRegions", "Semt");

        }

        private  void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox3, comboBox4, "GetNeighbourhoods", "Mahalle");
        }

        private  void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox4, comboBox5, "GetStreets", "Cadde/Sokak");

        }

        private  void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox5, comboBox6, "GetBuildings", "Apartman");

        }

        private  void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            request.POST(comboBox6, comboBox7, "GetAparments", "Daire");

        }

        public HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private async void button1_Click(object sender, EventArgs e)
        {
            address.ProvinceId = Convert.ToInt32(comboBox1.SelectedValue);
            address.DistrictId = Convert.ToInt32(comboBox2.SelectedValue);
            address.RegionId = Convert.ToInt32(comboBox3.SelectedValue);
            address.NeighbourhoodId = Convert.ToInt32(comboBox4.SelectedValue);
            address.StreetId = Convert.ToInt32(comboBox5.SelectedValue);
            address.BuildingId = Convert.ToInt32(comboBox6.SelectedValue);
            address.ApartmentId = Convert.ToInt32(comboBox7.SelectedValue);

            var map = new Dictionary<string, string>()
            {
                {"ProvinceId", address.ProvinceId.ToString()},
                {"DistrictId", address.DistrictId.ToString()},
                {"RegionId", address.RegionId.ToString()},
                {"NeighbourhoodId", address.NeighbourhoodId.ToString()},
                {"StreetId", address.StreetId.ToString()},
                {"BuildingId", address.BuildingId.ToString()},
                {"ApartmentId", address.ApartmentId.ToString()}
            };
            var content = new FormUrlEncodedContent(map);

            HttpClient client = new HttpClient(
                new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                }
                );

            var doc = new HtmlAgilityPack.HtmlDocument();

            var response = await client.PostAsync("https://www.netspeed.com.tr/InfrastructureInquiry/InfrastructureInquiryResult", content);

            string htmldata = await response.Content.ReadAsStringAsync();
           
            doc.LoadHtml(htmldata);

            var node = doc.DocumentNode.SelectSingleNode("//div[@class='text-warning font-weight-bolder display-4 my-4 mb-9 animate__animated animate__fadeIn']");
            label11.Text = node.InnerText.Trim();

            var node1 = doc.DocumentNode.SelectSingleNode("//div[@class='text-warning font-weight-bolder font-size-h1 font-size-h2 my-4 mb-9 animate__animated animate__fadeIn animate__delay-1s']");
            label12.Text = node1.InnerText.Trim();

            var node2 = doc.DocumentNode.SelectSingleNode("//span[@class='badge badge-danger animate__animated animate__fadeIn animate__delay-2s']");
            if (node2 == null)
            {
                node2 = doc.DocumentNode.SelectSingleNode("//span[@class='badge badge-success animate__animated animate__fadeIn animate__delay-2s']");
                label13.Text = node2.InnerText.Trim();

            }
            else
            {
                label13.Text = node2.InnerText.Trim();
            }

        }
    }
}
