using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden
{
    public static class JsonDownloader
    {
        private static string chats = "http://146.185.164.20:57483/chats";
        private static string messages = "http://146.185.164.20:57483/messages";

        public static async Task <Json.RootObject>  GetItemsFromDatabase()
        {
            try
            {
                Json.RootObject publicFeed = new Json.RootObject();

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://146.185.164.20:57483/ads");
                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                publicFeed = JsonConvert.DeserializeObject<Json.RootObject>(responseBody);
                return publicFeed;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                return null;
            }
        }

        public static async Task<Json.Chat.RootObject> GetChatsFromDB(string header)
        {
            try
            {
                Json.Chat.RootObject publicFeed = new Json.Chat.RootObject();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("boknaden-verify", header);
                    client.DefaultRequestHeaders.Add("boknaden-verify", header);
                    HttpResponseMessage response = await client.GetAsync(chats);
                    string result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result);
                    publicFeed = JsonConvert.DeserializeObject<Json.Chat.RootObject>(result);
                  
                    return publicFeed;
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Hm");
                System.Diagnostics.Debug.WriteLine("Nay");
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                return null;
            }
        }

        public static async Task<Json.Messages.RootObject> GetChatBubblesFromDB(string header, int chatID)
        {
            try
            {
                Json.Messages.RootObject publicFeed = new Json.Messages.RootObject();

                string query;
                using (var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                  new KeyValuePair<string, string>("chatid", chatID.ToString()),
                     }))
                {
                    query = content.ReadAsStringAsync().Result;
                }

                var builder = new UriBuilder(messages);
                builder.Query = query.ToString();
                string url = builder.ToString();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("boknaden-verify", header);
                    client.DefaultRequestHeaders.Add("boknaden-verify", header);

                   HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        publicFeed = JsonConvert.DeserializeObject<Json.Messages.RootObject>(result);
                        System.Diagnostics.Debug.WriteLine(result + "Denne her");
                        return publicFeed;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Nay");
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                return null;
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
}
