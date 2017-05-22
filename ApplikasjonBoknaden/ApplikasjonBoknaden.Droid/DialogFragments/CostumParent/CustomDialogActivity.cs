using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;
/// <summary>
/// Inspired by project https://github.com/t9mike/CustomDialogFragmentSample
/// </summary>
namespace ApplikasjonBoknaden.Droid.DialogFragments.CostumParent
{
    [Activity(MainLauncher = true)]
    public class CustomDialogFragment : Android.Support.V4.App.DialogFragment
    {
        protected CustomFragmentActivity CallerActivity = null;
        protected View Dialogueview = null;
        Button Button_Dismiss;

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
        /// <summary>
        /// How a normal PopupdialogueFragment
        /// </summary>
        /// <param name="message"></param>
        /// <param name="showCheckMark"></param>
        protected virtual void ShowToast(string message, Boolean showCheckMark = false)
        {
            PoppupDialogueFragment APDF = new PoppupDialogueFragment();
            APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, message, showCheckMark);
            //Toast.MakeText(CallerActivity, message, ToastLength.Long).Show();
        }
        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Android 3.x+ still wants to show title: disable
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            var view = inflater.Inflate(LayoutSetter(), container, true);
            Dialogueview = view;
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
            Dialog.Window.SetLayout(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            SetStyle(Android.Support.V4.App.DialogFragment.StyleNoFrame, Android.Resource.Style.Theme);
            base.OnResume();
        }

        /// <summary>
        /// Close this DialogueFragment
        /// </summary>
        protected virtual void CloseFragment()
        {
            Dismiss();
        }
        /// <summary>
        /// Disposes this fragment
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}