using System;
using System.Collections.Generic;

namespace CoinLatestData
{
    public class CoinResults
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class USD
        {
            public decimal price { get; set; }
            public object volume_24h { get; set; }
            public double volume_change_24h { get; set; }
            public double percent_change_1h { get; set; }
            public double percent_change_24h { get; set; }
            public double percent_change_7d { get; set; }
            public double market_cap { get; set; }
            public long market_cap_dominance { get; set; }
            public double fully_diluted_market_cap { get; set; }
            public DateTime last_updated { get; set; }
        }

              

        public class Quote
        {
            public USD USD { get; set; }
            
        }

        public class Data
        {
            public int id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string slug { get; set; }
            public int? cmc_rank { get; set; }
            public int? num_market_pairs { get; set; }
            public Int64? circulating_supply { get; set; }
            public Int64? total_supply { get; set; }
            public Int64? max_supply { get; set; }
            public DateTime last_updated { get; set; }
            public DateTime date_added { get; set; }
            public System.Collections.Generic.List<string> tags { get; set; }
            public Platform platform { get; set; }
            public Quote quote { get; set; }
        }

        public class Status
        {
            public DateTime timestamp { get; set; }
            public int? error_code { get; set; }
            public string error_message { get; set; }
            public int? elapsed { get; set; }
            public int? credit_count { get; set; }
        }


        public class Platform
        {
            public int? id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string slug { get; set; }
            public string token_address { get; set; }
        }


        public class CoinResultsMain
        {
            public List<Data> data { get; set; }
            public Status status { get; set; }
        }


    }
}

