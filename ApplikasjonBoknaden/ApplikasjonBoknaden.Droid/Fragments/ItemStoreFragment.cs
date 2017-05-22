using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Json;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    public class ItemStoreFragment : CostumFragment
    {
        protected GridLayout AdDisplayer;
        protected ProgressBar _ProgressBar;
        

        protected override int Layout()
        {
            return Resource.Layout.FragmentItemStoreLayout;
        }

        protected override void SetButtonValues()
        {
            AdDisplayer = Fragmentview.FindViewById<GridLayout>(Resource.Id.tableLayout1);
           // AdDisplayer.ColumnCount = 2;
            ImageView iv = Fragmentview.FindViewById<ImageView>(Resource.Id.SettingsImage);
            iv.Alpha = 0.4f;
            iv.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                   // iv.Alpha = 1.0f;
                  //  SearchFilterActivity s = new SearchFilterActivity();
                   // s.Show(FragmentActivityCaller.SupportFragmentManager, "dialog", FragmentActivityCaller);
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                  //  iv.Alpha = 0.4f;
                }
            };

            ImageView iv2 = Fragmentview.FindViewById<ImageView>(Resource.Id.AddNewItemImage);
            iv2.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    iv2.Alpha = 1.0f;

                    CallerActivity.takePictureFragment = new DialogFragments.AddNewAdPackDialogueFragment();
                    CallerActivity.takePictureFragment.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity);

                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    iv2.Alpha = 0.4f;
                }
            };
        }

        private void AddLoadingSign()
        {
            _ProgressBar = new ProgressBar(Context);
            _ProgressBar.LayoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent,RelativeLayout.LayoutParams.MatchParent);
            AdDisplayer.AddView(_ProgressBar);
        }

        private void ShowAdPack(Ad ad)
        {
            AdPackDialogueFragment APDF = new AdPackDialogueFragment();
            APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, ad);

        }
        private async void GetNewestAdsFromDatabase()
        {
            AddLoadingSign();
            Json.RootObject root = await JsonDownloader.GetItemsFromDatabase();
            if (root != null)
            {
                AddItems(root);
            }
            _ProgressBar.Visibility = ViewStates.Gone;

        }
        private void AddItems(Json.RootObject root)
        {
            foreach (Json.Ad a in root.ads)
            {
                AdItemClasses.AdMiniature AdPackDisplay1 = new AdItemClasses.AdMiniature(Context, AdDisplayer, a);
                AdPackDisplay1.GetButton().Click += delegate {
                    ShowAdPack(a);
                };
            }
        }
        protected override void InitiateFragment()
        {
            base.InitiateFragment();
            GetNewestAdsFromDatabase();
        }
    }
}