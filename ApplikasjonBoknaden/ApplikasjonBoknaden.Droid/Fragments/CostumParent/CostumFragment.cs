using Android.OS;
using Android.Views;
using Android.Widget;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class CostumFragment : Android.Support.V4.App.Fragment
    {
        protected View Fragmentview = null;
        protected CustomFragmentActivity CallerActivity = null;
        /// <summary>
        /// Returns this fragments caller (A CustomFragmentActivity)
        /// </summary>
        /// <returns></returns>
        public CustomFragmentActivity GetCallerActivity()
        {
            return CallerActivity;
        }
        /// <summary>
        /// Sets this fragments caller
        /// </summary>
        /// <param name="caller"></param>
        public void SetFragmentActivityCaller(CustomFragmentActivity caller)
        {
            CallerActivity = caller;
        }
        /// <summary>
        /// Show a Toastmessage
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowToast(string message)
        {
            Toast.MakeText(this.Context, message, ToastLength.Long).Show();
        }
        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected virtual void SetButtonValues()
        {

        }
        /// <summary>
        /// Override this to initiate your fragment when it is ready.
        /// </summary>
        protected virtual void InitiateFragment()
        {
            SetButtonValues();
        }
        /// <summary>
        /// Use this to close the fragment
        /// </summary>
        protected virtual void CloseFragment()
        {
            //Not used yet
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        /// <summary>
        /// returns the layout for this fragment
        /// </summary>
        /// <returns></returns>
        protected virtual int Layout()
        {
            return Resource.Layout.DialogueFragmentRegisterUserLayout;
        }
        /// <summary>
        /// Use this to return your custom view for this Fragment
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            Fragmentview = inflater.Inflate(Layout(), container, false);
            InitiateFragment();
            return Fragmentview;
        }
    }
}