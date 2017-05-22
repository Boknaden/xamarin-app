using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class RegisterNewUserDialogueFragment : CustomDialogFragment
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

        protected ImageView BackButton = null;


        protected ValidationResponse ValidationResponder = new ValidationResponse();

        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentRegisterUserLayout;
        }

        protected override void InitiateFragment()
        {
            SetButtonValues();
            foreach (NeededValues value in Enum.GetValues(typeof(NeededValues)))
            {
                string neededvalue = value.ToString();
                ListOfNeededValues.Add(neededvalue);
            }
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

            BackButton = Dialogueview.FindViewById<ImageView>(Resource.Id.ImageViewCardClose);
            BackButton.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    BackButton.Alpha = 1.0f;
                    CloseFragment();
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    BackButton.Alpha = 0.4f;
                }
            };

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
            ShowToast("Velkommen" +" " +  WritenUsername +"!", true);
            await Json.JsonUploader.RegisterNewUser(WritenUsername, WritenFirstname, WritenLastname, WritenPassword, WrittenEmail, WritenPhoneNR, WritenCourseID);
             CloseFragment();
        }
    }
}