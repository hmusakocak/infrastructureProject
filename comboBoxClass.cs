using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure_inquiry
{
    public class comboboxdata
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }
    }

}
