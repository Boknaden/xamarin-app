using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
namespace ApplikasjonBoknaden.Droid.Helpers
{
    public class RestSharpHelper
    {
        private static string AdsURL = "http://146.185.164.20:57483/ads"; //http://146.185.164.20:57483/authenticate
        public static async Task<HttpResponseMessage> AdNewAdd(string header, Json.Ad sentad)
        {
            //header = header;
            RestSharp.IRestResponse resp = null;
            try
            {

               // var client = new RestClient("http://146.185.164.20:57483/");

  

                Json.NewAdTest newad = new Json.NewAdTest();
                newad.courseid = 1;
                newad.adname = sentad.adname;
                newad.text = sentad.text;
                newad.aditems = new List<Json.Aditem>();

                 foreach (Json.Aditem item in sentad.aditems)
                  {
                      newad.aditems.Add(item);
                 }

              //  Json.Aditem newAdItem = new Json.Aditem();
               // newAdItem.isbn = "5555555555";
               // newAdItem.price = sentad.p
                //newAdItem.text = "Funker fra app!";
               // newAdItem.description = "Hm";

                //newad.aditems.Add(newAdItem);
                string jsonData = JsonConvert.SerializeObject(newad);
                System.Diagnostics.Debug.WriteLine(jsonData);

                var request = new RestRequest("ads", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("boknaden-verify", header);
                request.AddParameter("Application/Json", jsonData, ParameterType.RequestBody);


                //request.AddJsonBody(newad);

                //  System.Diagnostics.Debug.WriteLine(request.Parameters);

                //  RestResponse response = client.Execute(request);
                // var content = response.Content;

                //   var asyncHandle = client.ExecuteAsync<Json.Ad>(request, response => {
                //      Console.WriteLine(response.Data.adname);
                // });
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    content.Headers.Add("boknaden-verify", header);
                    HttpResponseMessage response = await client.PostAsync(AdsURL, content);
                    var result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result);

                    return response;
                }


              //  using (var client1 = new HttpClient())
              //  {
                //    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
               //     content.Headers.Add("boknaden-verify", header);

               //     HttpResponseMessage response1 = await client1.PostAsync(AdsURL, content);
               //     System.Diagnostics.Debug.WriteLine(response1);
            //        return response1;
             //   }

                // var restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
              //  resp = await client.ExecuteTaskAsync(request);
              //  System.Diagnostics.Debug.WriteLine(resp.Content);
              //  System.Diagnostics.Debug.WriteLine(resp.StatusCode + "Kode");

                //Console.WriteLine(resp.d);
               // return resp;

             //  = null;
              //  if (restResponse.)
              //  {

               // }


              //  client.ExecuteAsync(request, response =>
                //{
                  //  resp = response;
               

                    //HttpResponseMessage resp = response;
                  //  if (response.ErrorMessage == "User is not verified.")
                  //  {
                  //      System.Diagnostics.Debug.WriteLine("Ikke verifisert");
                 //  }
                 //  System.Diagnostics.Debug.WriteLine(response.Content);
              //  });

              //  return resp;


            }
            catch (Exception exception)
            {
              //  resp.ResponseStatus = ResponseStatus.Error;
                //resp.ErrorMessage = exception.Message;
               // resp.ErrorException = exception;

                System.Diagnostics.Debug.WriteLine("Nay");
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                return null;
            }
        }
    }
}