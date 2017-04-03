using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class RegisterNewUserDialogueFragment : CustomDialogActivity
    {
        protected enum NeededValues
        {
            Username,
            Emai,
            Firstname,
            Lastname,
            Password,
            RepeatedPassword,

        };

        protected List<string> ListOfNeededValues = new List<string>();

        protected string WritenUsername = "";
        protected string WrittenEmail = "";
        protected string WritenFirstname = "";
        protected string WritenLastname = "";
        protected string WritenPassword = "";
        protected string WritenRepeatedPassword = "";
        protected string WritenPhoneNR = "";
        protected string WritenCourseID = "";

        protected EditText InputFieldUsername = null;
        protected EditText InputFieldEmail = null;
        protected EditText InputFieldFirstname = null;
        protected EditText InputFieldLastname = null;
        protected EditText InputFieldPassword = null;
        protected EditText InputFieldRepeatedPassword = null;


        protected ValidationResponse ValidationResponder = new ValidationResponse();


     //   public override void OnCreate(Bundle savedInstanceState)
      //  {
      //      base.OnCreate(savedInstanceState);

            // Create your fragment here
     //   }

       // public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
       // {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
         //   return inflater.Inflate(Resource.Layout.RegisterNewUserFragmentLayout, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);
     //   }

        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentRegisterUserLayout;
        }

        protected override void InitiateFragment()
        {
           // base.OnCreate(savedInstanceState);

            // Create your application here
            //   SetContentView(Resource.Layout.RegisterNewUser);
            SetButtonValues();

            foreach (NeededValues value in Enum.GetValues(typeof(NeededValues)))
            {
                string neededvalue = value.ToString();
                ListOfNeededValues.Add(neededvalue);
            }
       //     Toast.MakeText(this, "Registrering fullført", ToastLength.Long).Show();
         //   UploadNewUser();
        }
        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            InputFieldUsername = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextUsername);
            InputFieldEmail = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextEmail);
            InputFieldFirstname = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextFirstname);
            InputFieldLastname = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextLastname);
            InputFieldPassword = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextPassword);
            InputFieldRepeatedPassword = Dialogueview.FindViewById<Android.Widget.EditText>(Resource.Id.editTextRepeatedPassword);


            Android.Widget.Button RegisterButton = Dialogueview.FindViewById<Android.Widget.Button>(Resource.Id.RegisterUserButton);
            RegisterButton.Click += delegate {

                ValidateInput();
            };

        }

        /// <summary>
        /// Starts the validation prosess of the values writen in the inputfields
        /// </summary>
        protected void ValidateInput()
        {
            WritenUsername = InputFieldUsername.Text;
            WrittenEmail = InputFieldEmail.Text;
            WritenFirstname = InputFieldFirstname.Text;
            WritenLastname = InputFieldLastname.Text;
            WritenPassword = InputFieldPassword.Text;
            WritenRepeatedPassword = InputFieldRepeatedPassword.Text;

            //Validates username
            ValidationResponder = InputValidator.validUsername(WritenUsername);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            //Validates email
            ValidationResponder = InputValidator.validEmail(WrittenEmail);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            //validates firstname
            ValidationResponder = InputValidator.validFirstname(WritenFirstname);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            //Validates lastname
            ValidationResponder = InputValidator.validLastname(WritenLastname);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            //Validates password
            ValidationResponder = InputValidator.validPassword(WritenPassword);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            //Validates repeated password
            ValidationResponder = InputValidator.validRepeatedPassword(WritenPassword, WritenRepeatedPassword);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            UploadNewUser();
        }

        private async void UploadNewUser()
        {
            ShowToast("Registrerer");
            await Json.JsonUploader.RegisterNewUser(WritenUsername, WritenFirstname, WritenLastname, WritenPassword, WrittenEmail, WritenPhoneNR, WritenCourseID);
           
             CloseFragment();
            //  await JsonDownloader.RegisterNewUser(WritenUsername, WrittenEmail, WritenFirstname, WritenLastname, WritenPassword);
        }
    }
}