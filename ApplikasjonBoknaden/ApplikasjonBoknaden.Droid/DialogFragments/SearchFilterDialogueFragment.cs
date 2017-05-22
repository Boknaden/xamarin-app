using System;
using Android.App;
using Android.Content;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;

namespace ApplikasjonBoknaden.Droid
{
    [Activity(Label = "SearchFilterActivity")]
    public class SearchFilterActivity : CustomDialogFragment
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
            SaveSortByRadioButons();
        }

        private void BackToMainMenu()
        {
            CloseFragment();
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