using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace infrastructure_inquiry.function
{
    public class request
    {
        public async void POST(System.Windows.Forms.ComboBox cb1, System.Windows.Forms.ComboBox cb2, string param , string emptylabel)
        {
            if (cb1.SelectedIndex != 0 && cb1.SelectedIndex != -1)
            {
                var map = new Dictionary<string, string>()
            {
                {"code", cb1.SelectedValue.ToString()}
            };
                var content = new FormUrlEncodedContent(map);
                HttpClient client = new HttpClient();

                var response = await client.PostAsync("https://www.netspeed.com.tr/InfrastructureInquiry/"+param, content);

                var responseString = await response.Content.ReadAsStringAsync();

                var myDeserializedClass = JsonConvert.DeserializeObject<List<comboboxdata>>(responseString);

                Dictionary<string, int> map1 = new Dictionary<string, int>();

                map1.Add(emptylabel+" seçiniz", 0);

                foreach (var item in myDeserializedClass)
                {
                    if (item.Text == null)
                    { map1.Add("null", item.Value); }
                    else
                    {
                        map1.Add(item.Text, item.Value);
                    }

                }
                cb2.DataSource = new BindingSource(map1, null);
                cb2.DisplayMember = "Key";
                cb2.ValueMember = "Value";

            }
            else
            {
                cb2.DataSource = null;
            }

        }
    }
}
