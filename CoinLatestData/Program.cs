using System;
using System.Collections.Generic;
using System.Net.Http;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Net.Http;
using RestSharp;

using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace CoinLatestData
{
    class Program
    {
        static void Main(string[] args)
        {


            GetCoinService gcs = new GetCoinService();
            gcs.GetCoinsByDateAndPrice();


        }



    }
}



