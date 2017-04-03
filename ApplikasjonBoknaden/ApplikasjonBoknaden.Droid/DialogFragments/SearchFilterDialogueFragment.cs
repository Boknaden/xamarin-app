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
using ApplikasjonBoknaden.Droid.DialogFragments;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;

namespace ApplikasjonBoknaden.Droid
{
    [Activity(Label = "SearchFilterActivity")]
    public class SearchFilterActivity : CustomDialogActivity
    {
        private ISharedPreferencesEditor sPEditor;
        private ISharedPreferences sP;
        private RadioGroup SortAfterGroup = null;

        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentSearchFiltersLayout;
        }

        protected override void InitiateFragment()
        {
        
    
            sPEditor = CallerActivity.GetSharedPreferences("SearchFilter", FileCreationMode.Private).Edit();
            sP = CallerActivity.GetSharedPreferences("SearchFilter", FileCreationMode.Private);
            SetButtonValues();


        }


     //   protected override void OnCreate(Bundle savedInstanceState)
     //   {
    //        base.OnCreate(savedInstanceState);
     //       ActionBar.Hide();
    //        SetContentView(Resource.Layout.SearchFilters);
    //        sPEditor = FragmentActivityCaller.GetSharedPreferences("SearchFilter", FileCreationMode.Private).Edit();
     //       sP = FragmentActivityCaller.GetSharedPreferences("SearchFilter", FileCreationMode.Private);
    //        SetButtonValues();

    //    }

        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            Android.Widget.Button backToMainMenuButton = Dialogueview.FindViewById<Android.Widget.Button>(Resource.Id.BackToMainMenuButton);
            backToMainMenuButton.Click += delegate {
                BackToMainMenu();
            };

            Android.Widget.Button resetFiltersButton = Dialogueview.FindViewById<Android.Widget.Button>(Resource.Id.ResetFiltersButton);
            resetFiltersButton.Click += delegate {
                ResetFilters();
            };

            SortAfterGroup = Dialogueview.FindViewById<Android.Widget.RadioGroup>(Resource.Id.SortaAfterGroup);
            InitialiseSortByRadioButtons();

        }

        private void InitialiseSortByRadioButtons()
        {
            for (int i = 0; i < SortAfterGroup.ChildCount; i++)
            {
                RadioButton rdb = (RadioButton)SortAfterGroup.GetChildAt(i);
                rdb.Click += RadioButtonClick;
                bool b = SavedValues.UserValues.getBooleanPrefs(rdb.Text, sP);

                if (b)
                {
                    rdb.Checked = true;
                  //  Toast.MakeText(this, rdb.Text, ToastLength.Short).Show();
                }
                else
                {
                    rdb.Checked = false;
                }
            }
        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
           // Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
            SaveSortByRadioButons();
        }



        private void BackToMainMenu()
        {
            CloseFragment();
         //   StartActivity(typeof(MainMenuActivity));
        }

        private void SaveSortByRadioButons()
        {
            for (int i = 0; i < SortAfterGroup.ChildCount; i++)
            {
                RadioButton rdb = (RadioButton)SortAfterGroup.GetChildAt(i);
                SavedValues.UserValues.saveBooleanPrefs(rdb.Text, rdb.Checked, sPEditor);
            }
        }

        private void ResetSortByRadioButtons()
        {
            for (int i = 0; i < SortAfterGroup.ChildCount; i++)
            {
                RadioButton rdb = (RadioButton)SortAfterGroup.GetChildAt(i);
                if (i == 0)
                {
                    rdb.Checked = true;
                }
                else
                {
                    rdb.Checked = false;
                }
                SavedValues.UserValues.saveBooleanPrefs(rdb.Text, rdb.Checked, sPEditor);
            }
        }

        private void ResetFilters()
        {
            ResetSortByRadioButtons();
        }
    }
}