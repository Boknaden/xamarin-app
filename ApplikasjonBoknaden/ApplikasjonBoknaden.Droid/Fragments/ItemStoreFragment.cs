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
using RestSharp;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class ItemStoreFragment : CostumFragment
    {
        protected GridLayout AdDisplayer;

        protected override int Layout()
        {
            return Resource.Layout.FragmentItemStoreLayout;
        }

        protected override void SetButtonValues()
        {
            AdDisplayer = Fragmentview.FindViewById<GridLayout>(Resource.Id.tableLayout1);
           // AdDisplayer.ColumnCount = 2;
            ImageView iv = Fragmentview.FindViewById<ImageView>(Resource.Id.SettingsImage);
            iv.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    iv.Alpha = 1.0f;
                    SearchFilterActivity s = new SearchFilterActivity();
                    s.Show(FragmentActivityCaller.SupportFragmentManager, "dialog", FragmentActivityCaller);
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    iv.Alpha = 0.4f;
                }
            };

            ImageView iv2 = Fragmentview.FindViewById<ImageView>(Resource.Id.AddNewItemImage);
            iv2.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    iv2.Alpha = 1.0f;

                    FragmentActivityCaller.takePictureFragment = new DialogFragments.TakePictureDialogueFragment();
                    FragmentActivityCaller.takePictureFragment.Show(FragmentActivityCaller.SupportFragmentManager, "dialog", FragmentActivityCaller);

                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    iv2.Alpha = 0.4f;
                }
            };
        }

        private async void GetNewestAdsFromDatabase()
        {
            Json.RootObject root = await JsonDownloader.GetItemsFromDatabase();
            if (root != null)
            {
                AddItems(root);
            }
        }

        private void AddItems(Json.RootObject root)
        {
            foreach (Json.Ad a in root.ads)
            {
                AdItemClasses.AdMiniature AdPackDisplay1 = new AdItemClasses.AdMiniature(Context, AdDisplayer, a);
            }
        }

        protected override void InitiateFragment()
        {
            base.InitiateFragment();
            GetNewestAdsFromDatabase();
        }
    }
}