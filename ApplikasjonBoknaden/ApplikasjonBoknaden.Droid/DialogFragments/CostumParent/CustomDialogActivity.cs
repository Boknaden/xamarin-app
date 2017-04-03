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
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using Android.Graphics;
/// <summary>
/// Inspired by project https://github.com/t9mike/CustomDialogFragmentSample
/// </summary>
namespace ApplikasjonBoknaden.Droid.DialogFragments.CostumParent
{
    [Activity(MainLauncher = true)]
    public class CustomDialogActivity : Android.Support.V4.App.DialogFragment
    {
        Button Button_Dismiss;
        protected CustomFragmentActivity CallerActivity = null;
        protected View Dialogueview = null;


        /// <summary>
        /// New show, used to also set the CallerActivity.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="tag"></param>
        /// <param name="caller"></param>
        public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller)
        {
            this.CallerActivity = caller;
            base.Show(manager, tag);
        }

        protected virtual void ShowToast(string message)
        {
            Toast.MakeText(CallerActivity, message, ToastLength.Long).Show();
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Android 3.x+ still wants to show title: disable
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

         //  View view = InitiateFragment(inflater, container);

            // Create our view
            var view = inflater.Inflate(LayoutSetter(), container, true);
            Dialogueview = view;

            // Handle dismiss button click
            //  //Button_Dismiss = view.FindViewById<Button>(Resource.Id.Button_Dismiss);
            //  Button_Dismiss.Click += Button_Dismiss_Click;
            InitiateFragment();
            return view;
        }
        /// <summary>
        /// Override this to initiate your fragment when it is ready.
        /// </summary>
        protected virtual void InitiateFragment()
        {

        }
        /// <summary>
        /// Override this to set the current layout
        /// </summary>
        /// <returns></returns>
        protected virtual int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentRegisterUserLayout;
        }

        public override void OnResume()
        {
            // Auto size the dialog based on it's contents
            Dialog.Window.SetLayout(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);

            // Make sure there is no background behind our view
            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));

            // Disable standard dialog styling/frame/theme: our custom view should create full UI
            SetStyle(Android.Support.V4.App.DialogFragment.StyleNoFrame, Android.Resource.Style.Theme);

            base.OnResume();
        }

        protected virtual void CloseFragment()
        {
            Dismiss();
        }

     //   private void Button_Dismiss_Click(object sender, EventArgs e)
     //   {
     //       Dismiss();
    //    }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // Unwire event
          //  if (disposing)
            //    Button_Dismiss.Click -= Button_Dismiss_Click;
        }
    }
}