using Android.App;
using Android.Content;
using Android.OS;
using ApplikasjonBoknaden.JsonHelpers;
using ApplikasjonBoknaden.Droid.SavedValues;
using Android.Content.PM;
using System.Threading.Tasks;
using ApplikasjonBoknaden.Json;
using ApplikasjonBoknaden.Droid.AndroidJsonHelpers;

namespace ApplikasjonBoknaden.Droid
{
    // "NoHistory = true" prevents the user to get back to this activity when they press back on their phone
    [Activity (NoHistory = true, Label = "ApplikasjonBoknaden.Droid", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]

	public class MainActivity : Activity
	{
        private UserOld savedUser = new UserOld();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.Hide();

            string token = UserValues.GetSavedToken(GetSharedPreferences("SearchFilter", FileCreationMode.Private));
            System.Diagnostics.Debug.WriteLine(token);
            //return;
            if (token != string.Empty)
            {

               // if (UserIsVerified(User))
                //{
                //}

                    // savedUser = UserValues.GetSavedUserValues(GetSharedPreferences("SearchFilter", FileCreationMode.Private));
                    TryToLogIn(savedUser);
                    //StartActivity(typeof(LoginActivity));
            }
            else
            {
                StartActivity(typeof(LoginActivity));
            }
        }
        /// <summary>
        /// Tries to log saved user in. (If saved user != null)
        /// </summary>
        /// <param name="user"></param>
        private async void TryToLogIn(UserOld user)
        {
            System.Net.Http.HttpResponseMessage Response = await Task.Run(() => Json.JsonUploader.CheckLoginCredentials(user));
            if (Response.IsSuccessStatusCode)
            {
                if (user.verified == "1")
                {
                    StartActivity(typeof(MainMenuActivity));
                }
                else
                {
                    StartActivity(typeof(LoginActivity));
                }
            }
            else
            {
                StartActivity(typeof(LoginActivity));
            }
        }
    }
}


