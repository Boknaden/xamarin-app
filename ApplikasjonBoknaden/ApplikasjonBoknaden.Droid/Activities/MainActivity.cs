using System;
using System.Collections;
using System.Collections.Generic;

using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.IO;
using ApplikasjonBoknaden.JsonHelpers;
using ApplikasjonBoknaden.Droid.SavedValues;

namespace ApplikasjonBoknaden.Droid
{
    // "NoHistory = true" prevents the user to get back to this activity when they press back on their phone
    [Activity (NoHistory = true, Label = "ApplikasjonBoknaden.Droid", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
        private UserOld savedUser = new UserOld();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();

            //Set this as a "Splash screen while autenticating user"
          //  SetContentView();

            savedUser = UserValues.GetSavedUserValues(GetSharedPreferences("SearchFilter", FileCreationMode.Private));
            Toast.MakeText(this, savedUser.Username, ToastLength.Long).Show();
            Toast.MakeText(this, savedUser.Password, ToastLength.Long).Show();
            TryToLogIn(savedUser);

          //  System.Diagnostics.Debug.WriteLine(savedUser.Username);





            //  SavedValues.UserValues.saveStringPrefs("Header", s, sPEditor);
            //System.Diagnostics.Debug.WriteLine(SavedValues.UserValues.getStringPrefs("Header", sP));

            //   if (s != SavedValues.UserValues.getStringPrefs("Header"))
            //   {

            //   }

            //   if (!UserValues.GetUserIsLogedInn())
            //    {
           // StartActivity(typeof(LoginActivity));
            //   } else
            //  {
            //      StartActivity(typeof(MainMenuActivity));
            //  }
        }

        private async void TryToLogIn(UserOld user)
        {
            System.Net.Http.HttpResponseMessage Response = await Json.JsonUploader.CheckLoginCredentials(user);

            if (Response.IsSuccessStatusCode)
            {
                StartActivity(typeof(MainMenuActivity));
                Toast.MakeText(this, "Success!", ToastLength.Long).Show();
            }
            else
            {
                StartActivity(typeof(LoginActivity));
                Toast.MakeText(this, "Feil passord eller brukernavn!", ToastLength.Long).Show();
            }
        }
    }
}


