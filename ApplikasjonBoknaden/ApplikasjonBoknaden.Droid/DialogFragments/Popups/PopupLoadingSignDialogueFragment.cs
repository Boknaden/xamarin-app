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
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;

namespace ApplikasjonBoknaden.Droid.DialogFragments.Popups
{
    class PopupLoadingSignDialogueFragment : CustomDialogFragment
    {
        private TextView MessageText = null;
        private Button CloseButton = null;
        private Button TransparentButton = null;
        private string Message = "";
        private RelativeLayout ClickableBackground = null;
        private bool ShowCheckMark = false;
        private bool ShowTransparentButton = false;
        protected ProgressBar _ProgressBar;
        private RelativeLayout ProgressBarLayoutPosition = null;

        public PopupLoadingSignDialogueFragment()
        {
           
        }

        public PopupLoadingSignDialogueFragment(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, String s, string buttonText)
        {
            ShowTransparentButton = true;
            Show(manager, tag, CallerActivity, s, false);

        }

        private void AddLoadingSign()
        {
            _ProgressBar = new ProgressBar(Context);
            _ProgressBar.LayoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            ProgressBarLayoutPosition.AddView(_ProgressBar);
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
            return Resource.Layout.DialogueFragment_PopupLoadingSign;
        }



        protected override void InitiateFragment()
        {
            ProgressBarLayoutPosition = Dialogueview.FindViewById<RelativeLayout>(Resource.Id.LoadingSignPosition);

            if (ShowCheckMark)
            {
            }
            else
            {
            }

            MessageText = Dialogueview.FindViewById<TextView>(Resource.Id.TextViewPopupMessage);
            MessageText.Text = Message;
           // ClickableBackground = Dialogueview.FindViewById<RelativeLayout>(Resource.Id.ClickableBackgroundRelativeLayout);
           // ClickableBackground.Clickable = true;
           // CloseButton = Dialogueview.FindViewById<Button>(Resource.Id.ClosePoppupMessageButton);
           // TransparentButton = Dialogueview.FindViewById<Button>(Resource.Id.TransparentButton_Popup);
            if (ShowTransparentButton)
            {
           //     TransparentButton.Visibility = ViewStates.Visible;
            }
            else
            {
            //    TransparentButton.Visibility = ViewStates.Gone;
            }

           // ClickableBackground.Click += delegate {
            //    CloseFragment();
           // };

            AddLoadingSign();
        }

        public Button GetTransparentButton()
        {
            return TransparentButton;
        }
    }
}