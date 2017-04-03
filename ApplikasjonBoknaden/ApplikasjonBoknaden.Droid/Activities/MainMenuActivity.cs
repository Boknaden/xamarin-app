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

using System.Net;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Timers;
using Android.Support.V4.App;
using ApplikasjonBoknaden.Droid.DialogFragments;
using ApplikasjonBoknaden.Droid.Fragments;
using RestSharp;
using Newtonsoft.Json;

namespace ApplikasjonBoknaden.Droid
{
    [Activity(Label = "LoginActivity")]
    public class MainMenuActivity : CustomFragmentActivity
    {

        protected Android.Support.V4.App.Fragment UserPageFragment = null;

        protected Android.Support.V4.App.Fragment NewestFragment = null;
        protected Android.Support.V4.App.FragmentTransaction FT = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityMainMenuLayout);
            // ge();

           // AddItems();
            SetButtonValues();

            FT = SupportFragmentManager.BeginTransaction();

            ChangeFragment(new ItemStoreFragment());
            //Call
            //AdNewAdd();
        }


     

        private void ChangeFragment(CostumFragment fragment)
        {
            if (fragment != NewestFragment)
            {
                fragment.SetFragmentActivityCaller(this);
                FT = SupportFragmentManager.BeginTransaction();

                FT.SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom);
                // UserPageFragment = new ItemStoreFragment();
                FT.Replace(Resource.Id.FragmentHolderMainMenu, fragment, "details_fragment");
                //  ft.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
                FT.Commit();
                NewestFragment = fragment;
            }

        }

        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            Android.Widget.ImageButton itemStoreImageButton = FindViewById<Android.Widget.ImageButton>(Resource.Id.ItemStoreImageButton);
            itemStoreImageButton.Click += delegate {
                ChangeFragment(new ItemStoreFragment());
                Toast.MakeText(this, "Item", ToastLength.Long).Show();
            };

            Android.Widget.ImageButton chatPageImageButton = FindViewById<Android.Widget.ImageButton>(Resource.Id.ChatPageImageButton);
            chatPageImageButton.Click += delegate {
                ChangeFragment(new ChatPageFragment());
                Toast.MakeText(this, "Chat", ToastLength.Long).Show();
            };

            Android.Widget.ImageButton UserPageImageButton = FindViewById<Android.Widget.ImageButton>(Resource.Id.UserPageImageButton);
            UserPageImageButton.Click += delegate {
                ChangeFragment(new UserPageActivity());
                Toast.MakeText(this, "User", ToastLength.Long).Show();
            };


            }


 
    }
}