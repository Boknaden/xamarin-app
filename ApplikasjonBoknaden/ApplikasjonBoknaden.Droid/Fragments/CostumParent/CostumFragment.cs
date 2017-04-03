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

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class CostumFragment : Android.Support.V4.App.Fragment
    {
        protected View Fragmentview = null;
        protected CustomFragmentActivity FragmentActivityCaller = null;

        public void SetFragmentActivityCaller(CustomFragmentActivity caller)
        {
            FragmentActivityCaller = caller;
        }


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

        protected virtual void CloseFragment()
        {
            //
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        protected virtual int Layout()
        {
            return Resource.Layout.DialogueFragmentRegisterUserLayout;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            Fragmentview = inflater.Inflate(Layout(), container, false);
            InitiateFragment();
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            return Fragmentview;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}