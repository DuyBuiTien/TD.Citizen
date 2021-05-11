using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Features.Places.Commands
{
    public class FetchResponse
    {
        public Results results { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Links
    {
        public string next { get; set; }
        public string previous { get; set; }
    }

    public class TabKeys
    {
        public string informations { get; set; }
        public string images { get; set; }
        public string rate_detail { get; set; }
    }

    public class Datum
    {
        public int? id { get; set; }
        public string category { get; set; }
        public List<string> tags { get; set; }
        public List<string> targets { get; set; }
        public string cover_url { get; set; }
        public string place_name { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string source { get; set; }
        public string extra_info { get; set; }
        public string phone_contact { get; set; }
        public string website { get; set; }
        public string address_detail { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string date_start { get; set; }
        public string time_start { get; set; }
        public string date_end { get; set; }
        public string time_end { get; set; }
        public string information { get; set; }
        public int? view_quantity { get; set; }
        public string published_at { get; set; }
        public decimal? rate_avg { get; set; }
        public TabKeys tab_keys { get; set; }
    }

    public class Results
    {
        public Links links { get; set; }
        public int? total_items { get; set; }
        public int? item_per_page { get; set; }
        public List<Datum> data { get; set; }
    }

 


}
