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
    public class GetCoinService
    {


        public void GetCoinsByDateAndPrice()
        {
            string APIKey = "";
            var client = new RestClient("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?sort=date_added&price_max=0.001");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CMC_PRO_API_KEY", APIKey);
            request.AddHeader("sort", "date_added");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {

               CoinResults.CoinResultsMain myDeserializedCoins = JsonConvert.DeserializeObject<CoinResults.CoinResultsMain>(response.Content);



                List<string> mailingList = new List<string>();
                mailingList.Add("person@email.com");
               


                string SendFrom = "test@test.com";
                    
                StringBuilder body = new StringBuilder();

                foreach (var ii in mailingList)
                {
                    string Sendto = ii.ToString();
                    int count = 0;
                    string notVerified = "";
                    string notVerifiedCS = "";

                    body.Append("<h4> Coin data as reported on Coin Market Cap. These coins are all below $0.001. NOTE: Do not invest unless you trust the project! Do not connect your wallet to anything you have not verified! <h4><br>");
                    foreach (var i in myDeserializedCoins.data)
                    {
                        try {
                            if (i.date_added >= DateTime.Now.AddDays(-1))
                            {
                                if (i.quote.USD.market_cap == 0 && i.circulating_supply == 0)
                                {
                                    notVerified = "Not yet CMC verified";
                                    notVerifiedCS = "Not yet CMC verified";
                                }
                                count++;


                                body.Append("<div class='container' style='clear: both; Margin - top: 10px; text - align: center; width: 100 %; '>");

                                body.Append("<div class='row'>");
                                body.Append("<div class='col-sm-3'>");
                                body.Append("<h2 style='color:#61CE70;'>New coin added: " + i.name + "(" + i.symbol + ")" + "</h2> <br>" +
                                    "<h2>Date added - " + i.date_added + " </h2>" +
                                    "<h4> Price - " + i.quote.USD.price + " , Volume 24H " + i.quote.USD.volume_24h + "<h4> " +
                                    "<h4>Market cap - " + i.quote.USD.market_cap + " " + notVerified + " </h4> " +
                                    "<h4>Max supply - " + i.max_supply + " </h4> " +
                                "<h4>Circulating supply - " + i.circulating_supply + " " + notVerifiedCS + " </h4> " +
                                    "<h4>Built on - " + i.platform.name + " </h4> " +
                                    "<br>");
                                body.Append("<a href='https://coinmarketcap.com/currencies/" + i.slug + "'" + "style= 'display: inline-block; color: #ffffff; background-color: #3498db; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;'> Check out</a>");
                                body.Append("</div></div>");
                                body.Append("</div>" + "<hr>");
                            }
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    body.Append("<br> <h5>Reply with the word STOP to unsubscribe.</h5>");
                    string subject = "CMC " + count + " recent coins added within the last 24";
                    string strBody = string.Format(body.ToString());

                    Console.WriteLine("Sending messages to " + ii.ToString());
                    var Send = SendEmail(subject, strBody, SendFrom, Sendto);
                }
                Console.WriteLine("Completed");
                Console.ReadLine();

                
            }

            



        }



        public bool SendEmail(string subject, string body, string SendFrom, string Sendto)
            {

            //modify with credentials or map to app secrets or keyvault
            string smtpAccount = "";
            int port = 25;
            string userName="";
            string password="";

             SmtpClient smtpClient = new SmtpClient(smtpAccount, port);

            smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
            
            
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage(SendFrom, Sendto);
                mailMessage.Subject = subject;
                mailMessage.Body = body;


                mailMessage.IsBodyHtml = true;

                try
                {
                
                    smtpClient.Send(mailMessage);

                }
                catch (Exception e)
                {
                Console.WriteLine("An error has occurred " + e.Message, " " + e.InnerException);
                    return false;
                }

                return true;


            }


        
    }
}
