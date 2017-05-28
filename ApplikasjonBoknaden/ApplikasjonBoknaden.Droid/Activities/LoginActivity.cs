using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using ApplikasjonBoknaden.Droid.DialogFragments;
using Newtonsoft.Json.Linq;
using ApplikasjonBoknaden.JsonHelpers;
using ApplikasjonBoknaden.Droid.AndroidJsonHelpers;
using ApplikasjonBoknaden.Droid.SavedValues;
using Android.Content.PM;
using ApplikasjonBoknaden.Droid.DialogFragments.Popups;

namespace ApplikasjonBoknaden.Droid
{
    [Activity(Label = "LoginActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : CustomFragmentActivity
    {
        private EditText LoginEditText;
        private EditText PasswordEditText;
        private UserOld User = new UserOld();
    

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.ActivityLoginLayout);
            SetButtonValues();
            ShowBetaInfo();
        }

        private void ShowBetaInfo()
        {
            PoppupDialogueFragment APDF = new PoppupDialogueFragment();
            APDF.Show(SupportFragmentManager, "dialog", this, "Denne appen er fortsatt under utvikling! Registrer deg og utforsk.", false);
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
                User.Username = LoginEditText.Text;
                User.Password = PasswordEditText.Text;
                User.Email = LoginEditText.Text;
                NewLogin();
                //Login(User);

            };
            RegisterButton.Click += delegate {
                PopupRegisterPage();
            };
        }
        /// <summary>
        /// Tries to log user in.
        /// </summary>
        private async void NewLogin()
        {
            ShowLoadingPopup("Logger inn");
            Json.LoginInfo lf = await Task.Run(() => Json.JsonUploader.AutenticateUser(User));

            if (!lf.WrongLoginInfo)
            {
                string token = lf.Token;
                User.verified = AndroidJsonHelper.GetValueFromToken(token, AndroidJsonHelper.UserValuesEnums.verified);
                if (UserIsVerified(User))
                {
                    UserValues.SaveToken(sPEditor, token);
                    CloseLoadingPopup();
                    StartActivity(typeof(MainMenuActivity));
                }
                else
                {
                    CloseLoadingPopup();
                    PopupNotVerifiedMailDialogueFragment APDF = new PopupNotVerifiedMailDialogueFragment(SupportFragmentManager, "dialog", this, Resources.GetString(Resource.String.UserNotVerified), Resources.GetString(Resource.String.SendNewVerification), token);
                }
            }
            else
            {
                PoppupDialogueFragment APDF = new PoppupDialogueFragment();
                CloseLoadingPopup();
                APDF.Show(SupportFragmentManager, "dialog", this, Resources.GetString(Resource.String.WrongloginInfo), false);
            }
        }
        /// <summary>
        /// Checks if given user has its verified value set to 1 (true) or 0 (false).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UserIsVerified(UserOld user)
        {
            if (User.verified == "1")
            {
                return true;
            }else
            {
                return false;
            }
        }
        /// <summary>
        /// Shows the register new user fragment
        /// </summary>
        private void PopupRegisterPage()
        {
            RegisterNewUserDialogueFragment s = new DialogFragments.RegisterNewUserDialogueFragment();
            s.Show(SupportFragmentManager, "dialog", this);
        }
    }
}