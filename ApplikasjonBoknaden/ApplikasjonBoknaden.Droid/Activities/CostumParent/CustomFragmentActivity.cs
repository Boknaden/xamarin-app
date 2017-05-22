using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Graphics;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    [Activity(Label = "CustomFragmentActivity")]
    public class CustomFragmentActivity : Android.Support.V4.App.FragmentActivity
    {
        public ISharedPreferences sP;
        public ISharedPreferencesEditor sPEditor;
        public AddNewAdPackDialogueFragment takePictureFragment = null;

        protected string NewestFragmentTag = "";
        protected Android.Support.V4.App.FragmentTransaction FT = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            sP = GetSharedPreferences("SearchFilter", FileCreationMode.Private);
            sPEditor = sP.Edit();
            base.OnCreate(savedInstanceState);
        }
        /// <summary>
        /// Used when user is taking a picture
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.
            takePictureFragment.setProductImage();
            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        protected void ChangeFragment(CostumFragment NewFragment, string newFragmentTag)
        {
            if (NewestFragmentTag == null || (NewestFragmentTag != null && newFragmentTag != NewestFragmentTag))
            {
                NewFragment.SetFragmentActivityCaller(this);
                FT = SupportFragmentManager.BeginTransaction();
                FT.SetCustomAnimations(Resource.Animation.design_bottom_sheet_slide_in, Resource.Animation.design_bottom_sheet_slide_out);
                FT.Replace(Resource.Id.FragmentHolderMainMenu, NewFragment, newFragmentTag);
                FT.Commit();
                NewestFragmentTag = newFragmentTag;
            }
        }
    }

    public static class App
    {
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;
    }
}