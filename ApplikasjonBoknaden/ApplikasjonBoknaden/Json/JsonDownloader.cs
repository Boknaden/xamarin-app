using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden
{
    public static class JsonDownloader
    {

        public static async Task <Json.RootObject>  GetItemsFromDatabase()
        {
            try
            {
                Json.RootObject publicFeed = new Json.RootObject();

                //List<Ads> ad = new List<Ads>();
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://146.185.164.20:57483/ads");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                publicFeed = JsonConvert.DeserializeObject<Json.RootObject>(responseBody);
                //Console.WriteLine(publicFeed.ads[0].adname + "Yay");
                return publicFeed;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                return null;
            }
        }


        public static async Task RegisterNewUser(string username, string firstName, string lastName, string password, string email)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://146.185.164.20:57483/users");


            string jsonData = 
            @"{
            ""username"" : ""Staurheim55"", 
            ""passphrase"" : ""Staurheimspylsa55"",
            ""email"" : ""cam-bridge@hotmail55.com"",
            ""firstname"" : ""Gunnar55"",
            ""lastname"" : ""Burka55"" 
            }";


            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://146.185.164.20:57483/users", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
        }

        public static async Task TestUploadNewUser()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://146.185.164.20:57483/users");

            string jsonData = @"{""username"" : ""Staurheim"", 
            ""passphrase"" : ""Staurheimspylsa"",
            ""email"" : ""cam-bridge@hotmail.com"",
            ""firstname"" : ""Gunnar"",
            ""lastname"" : ""Burka"" }";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://146.185.164.20:57483/users", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
        }

        public static async Task UploadNewUser(string username, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string jsonData = @"{""username"" : ""Staurheim"", 
            ""passphrase"" : ""Staurheimspylsa"",
            ""email"" : ""cam-bridge@hotmail.com"",
            ""firstname"" : ""Gunnar"",
            ""lastname"" : ""Burka"" }";
                    var content = new StringContent(
                    JsonConvert.SerializeObject(new { jsonData }));
                    var result = await client.PostAsync("http://146.185.164.20:57483/users", content).ConfigureAwait(false);
                    if (result.IsSuccessStatusCode)
                    {
                        var tokenJson = await result.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("Yay");
                    }
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }

        public static async Task GetValue(string name)
        {
            var uri2 = new Uri("http://www.pizzaboy.de/app/pizzaboy.json");
            var uri = new Uri("http://146.185.164.20:57483/ping");
            //  HttpClient myClient = new HttpClient();

            try
            {
                using (var myClient = new HttpClient())
                {
                    var response = await myClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<RootObject>(content);
                        // var Items = JsonConvert.DeserializeObject<List<RootObject>>(content);
                        System.Diagnostics.Debug.WriteLine(Items.payload + "Her ja wtf");
                        // Console.WriteLine("");
                    }

                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
            }


        }

        public static async Task RefreshDataAsyncLogin()
        {
            var uri2 = new Uri("http://www.pizzaboy.de/app/pizzaboy.json");
            var uri = new Uri("http://146.185.164.20:57483/users");
            //  HttpClient myClient = new HttpClient();

            try
            {
                using (var myClient = new HttpClient())
                {
                    var response = await myClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<RootObject2>(content);
                        // var Items = JsonConvert.DeserializeObject<List<RootObject>>(content);
                        System.Diagnostics.Debug.WriteLine(Items.username + "Her ja wtf");
                        // Console.WriteLine("");
                    }

                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                System.Diagnostics.Debug.WriteLine("Funka ikkje gitt");
            }


        }

        public static async Task RefreshDataAsync()
        {
            var uri2 = new Uri("http://www.pizzaboy.de/app/pizzaboy.json");
            var uri = new Uri("http://146.185.164.20:57483/ping");
            //  HttpClient myClient = new HttpClient();

            try
            {
                using (var myClient = new HttpClient())
                {
                    var response = await myClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Items = JsonConvert.DeserializeObject<RootObject>(content);
                       // var Items = JsonConvert.DeserializeObject<List<RootObject>>(content);
                        System.Diagnostics.Debug.WriteLine(Items.payload + "Her ja wtf");
                        // Console.WriteLine("");
                    }

                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                System.Diagnostics.Debug.WriteLine("Funka ikkje gitt");
            }


        }

        public static async Task<T> DownloadSerializedJSONDataAsync<T>(string url) where T : new()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonData = string.Empty;
                try
                {
                    jsonData = await httpClient.GetStringAsync(url);
                }
                catch (Exception)
                {
                    return default(T);
                }
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : default(T);
            }
        }
    }

    public class RootObject
    {
        public string payload { get; set; }
    }

    public class RootObject2
    {
        public string username { get; set; }
    }

    public class RootObject1
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Link { get; set; }
    }
}
