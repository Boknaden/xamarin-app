using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace ApplikasjonBoknaden.Droid.Helpers
{
    public class RestSharpHelper
    {
       

        public void AdNewAdd(string header)
        {
            var client = new RestClient("http://146.185.164.20:57483/");

            Json.Ad newad = new Json.Ad();
            // newad.userid = "1";
            newad.universityid = 1;
            newad.courseid = 1;
            newad.adname = "Nye boker!";
            newad.text = "Text";
            Json.Aditem newbook = new Json.Aditem();
            // newbook.userid = "1";
            newbook.price = 250;
            newbook.text = "Funker fra app!";

            newad.aditems.Add(newbook);

            var request = new RestRequest("ads", Method.POST);
            //request.AddObject(newbook);

            //request.AddObject("'");
            request.AddJsonBody(newad);
            // request.AddBody("'");
            //  request.AddJsonBody(newbook);
            //  request.AddBody("'");
            // request.AddBody(newad);
            //  request.AddObject(newad);

            request.AddHeader("boknaden-verify", header);
            System.Diagnostics.Debug.WriteLine(request.Parameters);

            //  RestResponse response = client.Execute(request);
            // var content = response.Content;

            client.ExecuteAsync(request, response => {
                Console.WriteLine(response.Content);
            });
        }
    }
}