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
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    class PoppupDialogueFragment : CustomDialogFragment
    {
        private TextView MessageText = null;
        private Button CloseButton = null;
        private Button TransparentButton = null;
        private string Message = "";
        private RelativeLayout ClickableBackground = null;
        private ImageView GreenCheckMarkImage = null;
        private bool ShowCheckMark = false;
        private bool ShowTransparentButton = false;

        public PoppupDialogueFragment()
        {

        }

        public PoppupDialogueFragment(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, string buttonText)
        {
            ShowTransparentButton = true;
            Show(manager, tag, CallerActivity, s, false);

        }

        public async Task Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, string buttonText)
        {
            ShowTransparentButton = true;
            Show(manager, tag, CallerActivity, s, false);
        }

       // public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, string buttonText)
       // {
         //   ShowTransparentButton = true;
         //   Show(manager, tag, CallerActivity, s, false);
          
      //  }

        public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, bool showCheckMark)
        {
            this.CallerActivity = caller;
            base.Show(manager, tag);
            Message = s;

            if (showCheckMark)
            {
                ShowCheckMark = true;
            }
            else
            {
                ShowCheckMark = false;
            }


        }



        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentPoppupMessageLayout;
        }

   

        protected override void InitiateFragment()
        {
            GreenCheckMarkImage = Dialogueview.FindViewById<ImageView>(Resource.Id.imageViewGreenCheckMark);
        
            if (ShowCheckMark)
            {
                GreenCheckMarkImage.Visibility = ViewStates.Visible;
            }else
            {
                GreenCheckMarkImage.Visibility = ViewStates.Gone;
            }
            MessageText = Dialogueview.FindViewById<TextView>(Resource.Id.TextViewPopupMessage);
            MessageText.Text = Message;
            ClickableBackground = Dialogueview.FindViewById<RelativeLayout>(Resource.Id.ClickableBackgroundRelativeLayout);
            ClickableBackground.Clickable = true;
            CloseButton = Dialogueview.FindViewById<Button>(Resource.Id.ClosePoppupMessageButton);
            TransparentButton = Dialogueview.FindViewById<Button>(Resource.Id.TransparentButton_Popup);
            if (ShowTransparentButton)
            {
                TransparentButton.Visibility = ViewStates.Visible;
            }else
            {
                TransparentButton.Visibility = ViewStates.Gone;
            }

            ClickableBackground.Click += delegate {
                CloseFragment();
            };
        }

        public Button GetTransparentButton()
        {
            return TransparentButton;
        }
    }
}