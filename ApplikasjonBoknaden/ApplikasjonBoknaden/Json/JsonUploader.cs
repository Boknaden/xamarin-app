using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApplikasjonBoknaden.JsonHelpers;
using System.Net.Http.Headers;
using ApplikasjonBoknaden.Json.Chat;
using System.IO;
using Android.Graphics;
using System.Globalization;
using System.Text.RegularExpressions;
using Android.OS;
using static Android.Provider.SyncStateContract;

namespace ApplikasjonBoknaden.Json
{
    /// <summary>
    /// http://www.newtonsoft.com/json
    /// https://github.com/dvsekhvalnov/jose-pcl
    /// </summary>
    public static class JsonUploader
    {

        private static string AutenticationURL = "http://146.185.164.20:57483/authenticate"; //http://146.185.164.20:57483/authenticate
        private static string UsersURL = "http://146.185.164.20:57483/users"; //http://146.185.164.20:57483/users //http://10.0.0.58:57483/users
        private static string AdURL = "http://146.185.164.20:57483/ads";
        private static string VerifyURL = "http://146.185.164.20:57483/verifyuser";
        private static string messagesURL = "http://146.185.164.20:57483/messages";
        private static string ImagesURL = "http://146.185.164.20:57484/image";

        public static async void UploadImage(string header, AdItemImage file)
        {
            //variable
          //  var url = "http://hallpassapi.jamsocialapps.com/api/profile/UpdateProfilePicture/";
            //var file = "path/to/file.ext";

            try
            {
                byte[] bitmapData;
                var stream = new MemoryStream();
                file.file.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                bitmapData = stream.ToArray();
                var filecontent = new ByteArrayContent(bitmapData);

                //read file into upfilebytes array
                //var upfilebytes = System.IO.File.ReadAllBytes(file);

                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(bitmapData);
                StringContent studentIdContent = new StringContent("2123");
                
                content.Add(baContent, "file");
                content.Add(studentIdContent, "StudentId");
                content.Headers.Add("boknaden-verify", header);
                //content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                //upload MultipartFormDataContent content async and store response in response var
                var response =
                    await client.PostAsync(ImagesURL, content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                //debug
                System.Diagnostics.Debug.WriteLine(responsestr);

            }
            catch (Exception e)
            {
                //debug
                System.Diagnostics.Debug.WriteLine("Exception Caught: " + e.ToString());

                return;
            }
        }

        public static async void upload(string header, AdItemImage ai)
 {
            try
            {
                byte[] bitmapData;
                var stream = new MemoryStream();
                ai.file.Compress(Bitmap.CompressFormat.Jpeg, 85, stream);
                bitmapData = stream.ToArray();
                var filecontent = new ByteArrayContent(bitmapData);

                if (filecontent == null)
                {
                    System.Diagnostics.Debug.WriteLine("Den er tom!");
                }

                // StreamContent scontent = new StreamContent(stream);
                filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = "image.jpg",
                    Name = "file"
                };
                filecontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                var client = new HttpClient();
                var multi = new MultipartFormDataContent();
                multi.Add(filecontent);
                multi.Headers.Add("boknaden-verify", header);
                //client.BaseAddress = new Uri(Constants.API_ROOT_URL);
                var result = client.PostAsync(ImagesURL, multi).Result;
                System.Diagnostics.Debug.WriteLine(result.ReasonPhrase);
                var result1 = await result.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(result1 + "Den ligger her");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
  }

        public static async Task<String> UploadBitmapAsync(string header, AdItemImage file)
        {

            byte[] bitmapData;
            var stream = new MemoryStream();
            file.file.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
            bitmapData = stream.ToArray();
            var filecontent = new ByteArrayContent(bitmapData);


            filecontent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
               Name = "file",
                FileName = "my_uploaded_image.jpg"
           };

            string boundary = "---8d0f01e6b3b5dafaaadaad";
            MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
            multipartContent.Add(filecontent);
            multipartContent.Headers.Add("boknaden-verify", header);
            multipartContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(ImagesURL, multipartContent);
            System.Diagnostics.Debug.WriteLine(response + "Dette er responsen");
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result + "Dette er resultatet nyny");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(content + "Det funka");
                return content;
            }
            return null;
        }

        public static async Task<HttpResponseMessage> AddNewImage(string header, AdItemImage image)
        {
            try
            {
                // NewMessageTest me = new NewMessageTest();

              //  byte[] bytes = image.imagefile.
               // dados.put("File", DatatypeConverter.printBase64Binary(bytes))

                string jsonData = JsonConvert.SerializeObject(image.imagefile);
               // Java.IO.File jsonData = JsonConvert.SerializeObject(image.imagefile);
              //  System.Diagnostics.Debug.WriteLine(jsonData);

                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    //var content = new ByteArrayContent(image.imagefile);
                    content.Headers.Add("boknaden-verify", header);
                    HttpResponseMessage response = await client.PostAsync(ImagesURL, content);
                    var result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result + "Dette er resultatet");

                    return response;
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

        public static async Task<HttpResponseMessage> AddNewMessage(string header, string message, NewMessageTest nmt)
        {
            try
            {
               // NewMessageTest me = new NewMessageTest();


                string jsonData = JsonConvert.SerializeObject(nmt);
                System.Diagnostics.Debug.WriteLine(jsonData);

                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    content.Headers.Add("boknaden-verify", header);
                    HttpResponseMessage response = await client.PostAsync(messagesURL, content);
                    var result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result);

                    return response;
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

        /// <summary>
        /// Asks the API to send a new verificationmail to given user (Gets userinfo from given token)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendNewVerificationMail(string token)
        {
            System.Diagnostics.Debug.WriteLine("sender");
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    content.Headers.Add("boknaden-verify", token);
                    HttpResponseMessage response = await client.PutAsync(VerifyURL, content);
                    var result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result);
                    System.Diagnostics.Debug.WriteLine("har sendt");
                    return response;
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
        /// <summary>
        /// Ads given addpack to the database
        /// </summary>
        /// <param name="header"></param>
        /// <param name="sentad"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> AddNewAdPack(string header, Json.Ad sentad)
        {
            try
            {
                Json.NewAdTest newad = new Json.NewAdTest();
                newad.courseid = 1;
                newad.adname = sentad.adname;
                newad.text = sentad.text;
                newad.aditems = new List<Json.Aditem>();

                foreach (Json.Aditem item in sentad.aditems)
                {
                    //item.imageid = 1;

                    item.image.imageid = 1;
                    newad.aditems.Add(item);

                }

                string jsonData = JsonConvert.SerializeObject(newad);
                System.Diagnostics.Debug.WriteLine(jsonData);

                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    content.Headers.Add("boknaden-verify", header);
                    HttpResponseMessage response = await client.PostAsync(AdURL, content);
                    var result = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(result +"Kommer hit");

                    return response;
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

        /// <summary>
        /// Checks given User and returns respons from database (Sucessfull or unsucessfull)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CheckLoginCredentials(UserOld user)
        {

            UserCredentialsUsername ucu = new UserCredentialsUsername();
            ucu.passphrase = user.Password;
            ucu.username = user.Username;

            string jsonData1 = JsonConvert.SerializeObject(ucu);
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData1, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(AutenticationURL, content);
                    return response;
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
        /// <summary>
        /// Autenticates given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<LoginInfo> AutenticateUser(UserOld user)
        {
            LoginInfo loginInfo = new LoginInfo();
            UserCredentialsUsername ucu = new UserCredentialsUsername();
            ucu.passphrase = user.Password;
            ucu.username = user.Username;

            string jsonData = JsonConvert.SerializeObject(ucu);

            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await Task.Run(() => client.PostAsync(AutenticationURL, content));
                    JToken jt = await Task.Run(() => response.Content.ReadAsStringAsync());
                    string result = jt.ToString();
                    if (response.IsSuccessStatusCode)
                    {
                        loginInfo.Token = result;
                        loginInfo.Token = BoknadenHelpers.CleansedToken(result);
                        return loginInfo;
                    }
                    else
                    {
                        result = "Feil brukernavn eller passord";
                        loginInfo.WrongLoginInfo = true;
                        return loginInfo;
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
        /// <summary>
        /// http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonConvert.htm
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static async Task RegisterNewUser(string username, string firstName, string lastName, string password, string email, string phone, string courseID)
        {

            Json.UserClasses.RootObjectTest newuser = new Json.UserClasses.RootObjectTest();
            newuser.username = username;
            newuser.passphrase = password;
            newuser.email = email;
            newuser.firstname = firstName;
            newuser.lastname = lastName;
            newuser.phone = "47706787";
            newuser.courseid = "1";

            string jsonData = JsonConvert.SerializeObject(newuser);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UsersURL);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(UsersURL, content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Nay");
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }
    }

    public class NewMessageTest
    {
        //public Recipient _Recipient;
        public string message = "";
       // public Json.Chat.Chat _Chat;
      //  public int ChatId;
        public int recipientid;
        public int chatid;
    }

    public class NewAdTest
    {
        public int userid { get; set; }
        public int courseid { get; set; }
        public string adname { get; set; }
        public string text { get; set; }
        public object pinned { get; set; }
        public int deleted { get; set; }

        public List<Aditem> aditems { get; set; }
    }

    public class User
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class Aditem
    {
        public int aditemid { get; set; }
        public int userid { get; set; }
        public int adid { get; set; }
       // public Image image { get; set; }
        public long price { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public object isbn { get; set; }
        public int deleted { get; set; }
        public int active { get; set; }
        public object buyerid { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public AdItemImage image { get; set; }
    }

    public class AdItemImage
    {
        public string imageurl { get; set; }
        public string title { get; set; }
        public int imageid { get; set; }
        public Java.IO.File imagefile { get; set; }
        public Bitmap file { get; set; }
    }


    public class University
    {
        public int universityid { get; set; }
        public string universityname { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int deleted { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
    }

    public class Campus
    {
        public int campusid { get; set; }
        public string campusname { get; set; }
        public int universityid { get; set; }
        public int deleted { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public University university { get; set; }
    }

    public class Course
    {
        public int courseid { get; set; }
        public string coursename { get; set; }
        public int campusid { get; set; }
        public int deleted { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public Campus campus { get; set; }
    }

    /// <summary>
    ///  values with ? behind them can be null.
    /// </summary>
    public class Ad
    {
        public int adid { get; set; }
        public int userid { get; set; }
        public int courseid { get; set; }
        public string adname { get; set; }
        public string text { get; set; }
        public object pinned { get; set; }
        public int deleted { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public object universityid { get; set; }
        public object campusid { get; set; }
        public User user { get; set; }
        public List<Aditem> aditems { get; set; }
        public Course course { get; set; }
    }

    public class RootObject
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public List<Ad> ads { get; set; }
    }

    public class LoginInfo
    {
        public string Token = "";
        public bool WrongLoginInfo = false;
    }
}


