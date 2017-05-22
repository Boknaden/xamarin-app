using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Support.V4.View;
using ApplikasjonBoknaden.Droid.ViewPageExpanders;
using ApplikasjonBoknaden.Droid.DialogFragments;
using ApplikasjonBoknaden.Droid.SavedValues;

namespace ApplikasjonBoknaden.Droid
{
    //https://developer.xamarin.com/guides/android/user_interface/viewpager/part-1-viewpager-and-views/
    [Activity(Label = "UserPageActivity")]
    public class UserPageActivity : CostumFragment
    {

        private ISharedPreferences sP;
        private ISharedPreferencesEditor sPEditor;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        protected override int Layout()
        {
            return Resource.Layout.FragmentUserPageLayout;
        }

        protected override void InitiateFragment()
        {
            Button logOutButton = Fragmentview.FindViewById<Button>(Resource.Id.LogOutUserbutton);
            logOutButton.Click += delegate {
                LogOut();
            };



            sP = CallerActivity.GetSharedPreferences("SearchFilter", FileCreationMode.Private);
            sPEditor = sP.Edit();
            TextView usernameTextview = Fragmentview.FindViewById<TextView>(Resource.Id.UserNametextView);
            usernameTextview.Text = SavedValues.UserValues.GetValueFromToken(sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.firstname) + " " + SavedValues.UserValues.GetValueFromToken(sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.lastname);
            ViewPager viewPager = Fragmentview.FindViewById<ViewPager>(Resource.Id.viewpager);
            CustomCatalog customCatalog = new CustomCatalog();
            viewPager.Adapter = new CustomPageAdapter(this.Context, customCatalog);

        }
        /// <summary>
        /// Saves a new empty user, and returns to LoginActivity.
        /// </summary>
        private void LogOut()
        {
            UserValues.SaveToken(sPEditor, "");
            CallerActivity.StartActivity(typeof(LoginActivity));
        }
    }
}