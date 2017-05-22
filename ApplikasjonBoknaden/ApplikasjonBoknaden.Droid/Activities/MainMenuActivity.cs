using Android.App;
using Android.OS;
using Android.Views;
using ApplikasjonBoknaden.Droid.DialogFragments;
using ApplikasjonBoknaden.Droid.Fragments;
using Android.Content.PM;

namespace ApplikasjonBoknaden.Droid
{
    //[Activity(Label = "LoginActivity")]
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainMenuActivity : CustomFragmentActivity
    {
        protected Android.Support.V4.App.Fragment UserPageFragment = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.Hide();
            SetContentView(Resource.Layout.ActivityMainMenuLayout);
            SetButtonValues();
            FT = SupportFragmentManager.BeginTransaction();
            ChangeFragment(new ItemStoreFragment(), "ItemStoreFragment");
        }
        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            Android.Widget.ImageView itemStoreImageButton = FindViewById<Android.Widget.ImageView>(Resource.Id.ItemStoreImage);
            itemStoreImageButton.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    itemStoreImageButton.Alpha = 1.0f;
                    ChangeFragment(new ItemStoreFragment(), "ItemStoreFragment");
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    itemStoreImageButton.Alpha = 0.4f;
                }
            };

            Android.Widget.ImageView chatPageImageButton = FindViewById<Android.Widget.ImageView>(Resource.Id.ChatPageImage);
            chatPageImageButton.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    chatPageImageButton.Alpha = 1.0f;
                    ChangeFragment(new ChatPageFragment(),"ChatPageFragment");
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    chatPageImageButton.Alpha = 0.4f;
                }
            };

            Android.Widget.ImageView UserPageImageButton = FindViewById<Android.Widget.ImageView>(Resource.Id.UserPageImage);
            UserPageImageButton.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    UserPageImageButton.Alpha = 1.0f;
                    ChangeFragment(new UserPageActivity(), "UserPageActivity");
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    UserPageImageButton.Alpha = 0.4f;
                }
            };
        }
    }
}