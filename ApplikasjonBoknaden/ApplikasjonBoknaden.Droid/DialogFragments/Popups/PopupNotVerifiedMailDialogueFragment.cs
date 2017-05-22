using System;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Droid.DialogFragments.Popups
{
    class PopupNotVerifiedMailDialogueFragment : CustomDialogFragment
    {
        private TextView MessageText = null;
        private Button TransparentButton = null;
        private string Message = "";
        private string TransparentButtonMessage = "";
        private RelativeLayout ClickableBackground = null;
        private string Token = "";
        /// <summary>
        /// Constructor for the popup
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="tag"></param>
        /// <param name="caller"></param>
        /// <param name="s"></param>
        /// <param name="buttonText"></param>
        public PopupNotVerifiedMailDialogueFragment(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, string buttonText, string token)
        {
            TransparentButtonMessage = buttonText;
            Token = token;
            Message = s;
            this.CallerActivity = caller;
            Show(manager, tag);
        }
        /// <summary>
        /// Returns the layout for the popup
        /// </summary>
        /// <returns></returns>
        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentPoppupMessageLayout;
        }
        /// <summary>
        /// Initiates the popup
        /// </summary>
        protected override void InitiateFragment()
        {
            MessageText = Dialogueview.FindViewById<TextView>(Resource.Id.TextViewPopupMessage);
            MessageText.Text = Message;

            ClickableBackground = Dialogueview.FindViewById<RelativeLayout>(Resource.Id.ClickableBackgroundRelativeLayout);
            ClickableBackground.Clickable = true;

            TransparentButton = Dialogueview.FindViewById<Button>(Resource.Id.TransparentButton_Popup);
            TransparentButton.Visibility = ViewStates.Visible;
            TransparentButton.Click += delegate {
                SendNewVerificationMail();
            };

            ClickableBackground.Click += delegate {
                CloseFragment();
            };
        }
        /// <summary>
        /// Sends a new verification mail to user and shows a new popup with "Email sendt" text
        /// </summary>
        private async void SendNewVerificationMail()
        {
            System.Diagnostics.Debug.WriteLine("SendNewVerificationMail");
            ShowToast("Email sendt!", false);
            System.Net.Http.HttpResponseMessage Response = await Task.Run(() => Json.JsonUploader.SendNewVerificationMail(Token));
            CloseFragment();
        }
    }
}