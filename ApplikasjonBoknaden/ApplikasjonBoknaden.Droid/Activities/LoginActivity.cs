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
using System.Threading.Tasks;
using Android.Support.V4.App;
using ApplikasjonBoknaden.Droid.DialogFragments;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Dynamic;
using ApplikasjonBoknaden.JsonHelpers;
using ApplikasjonBoknaden.Droid.AndroidJsonHelpers;
using ApplikasjonBoknaden.Droid.SavedValues;
using System.Net.Http;

namespace ApplikasjonBoknaden.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : CustomFragmentActivity
    {
        private EditText LoginEditText;
        private EditText PasswordEditText;

        private UserOld User = new UserOld();
        private string password = "";
        private string Username = "";
        private string Email = "";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActionBar.Hide();
           // sP = GetSharedPreferences("SearchFilter", FileCreationMode.Private);
           // sPEditor = sP.Edit();

            

            SetContentView(Resource.Layout.ActivityLoginLayout);



            SetButtonValues();

            //   CheckIfUserShouldGetNewToken();
        }

        //  private async void G()
        //  {
        //       await JsonUploader.RegisterNewUser(WritenUsername, WrittenEmail, WritenFirstname, WritenLastname, WritenPassword);
        //  await JsonDownloader.RegisterNewUser(WritenUsername, WrittenEmail, WritenFirstname, WritenLastname, WritenPassword);
        // }


        private async void TryToLogIn(UserOld user)
        {
            HttpResponseMessage Response = await Json.JsonUploader.CheckLoginCredentials(user);

            if (Response.IsSuccessStatusCode)
            {
                CheckIfUserShouldGetNewToken();
                StartActivity(typeof(MainMenuActivity));
                Toast.MakeText(this, "Success!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Feil passord eller brukernavn!", ToastLength.Long).Show();
            }
        }

        private async void CheckIfUserShouldGetNewToken()
        {
            // if (SavedValues.UserValues.getStringPrefs("Header", sP))
            // {
            JToken s = await Json.JsonUploader.AutenticateUser(User);
            SavedValues.UserValues.saveStringPrefs("Header", s.ToString(), sPEditor);
            System.Diagnostics.Debug.WriteLine(SavedValues.UserValues.getStringPrefs("Header", sP));

            SaveNewLogin(s.ToString());
            //TestJwtSecurityTokenHandler(s.ToString());
            // }
        }

        private void SaveNewLogin(string token)
        {
            User.Firstname = AndroidJsonHelper.GetValueFromToken(token, AndroidJsonHelper.UserValuesEnums.firstname);
            User.Lastname = AndroidJsonHelper.GetValueFromToken(token, AndroidJsonHelper.UserValuesEnums.lastname);
            User.Email = AndroidJsonHelper.GetValueFromToken(token, AndroidJsonHelper.UserValuesEnums.email);
            User.Username = AndroidJsonHelper.GetValueFromToken(token, AndroidJsonHelper.UserValuesEnums.username);
            User.Password = password;
            User.Token = token;

            UserValues.SaveNewUserValues(User, sPEditor);
        }

        private void PopupRegisterPage()
        {

            RegisterNewUserDialogueFragment s = new DialogFragments.RegisterNewUserDialogueFragment();
            s.Show(SupportFragmentManager, "dialog", this);

          //  Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
         //   ft.SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_top);
         //   ft.Replace(Resource.Id.FragmentHolderRelativeLayout, new RegisterNewUserFragment(), "details_fragment");
           // ft.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
        //    ft.Commit();


        }








        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            Android.Widget.Button loginButton = FindViewById<Android.Widget.Button>(Resource.Id.Button_Login);
            Android.Widget.Button RegisterButton = FindViewById<Android.Widget.Button>(Resource.Id.Button_NewUser);
            LoginEditText = FindViewById<EditText>(Resource.Id.editTextEmailLogin);
            PasswordEditText = FindViewById<EditText>(Resource.Id.editTextPassordLogin);

            loginButton.Click += delegate {
                Login();

            };

            RegisterButton.Click += delegate {
             //   var dialog = new RegisterNewUserFragment();
               // dialog.Show(SupportFragmentManager, "dialog", this);
                 PopupRegisterPage();

            };
        }

        private void Login()
        {
            password = PasswordEditText.Text;
            Username = LoginEditText.Text;
            Email = LoginEditText.Text;
            User.Username = Username;
            User.Password = password;
            User.Email = Email;

    
            TryToLogIn(User);


           // CheckIfUserShouldGetNewToken();
           // StartActivity(typeof(MainMenuActivity));
        }


     //   protected async void Login()
   //     {
    //        await JsonUploader.TryToLogIn(LoginEditText.Text, PasswordEditText.Text);
            //StartActivity(typeof(MainMenuActivity));
    //    }
    }
}