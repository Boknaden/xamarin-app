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
using Android.Graphics;
using ApplikasjonBoknaden.Droid.Helpers;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    [Activity(Label = "CustomFragmentActivity")]
    public class CustomFragmentActivity : Android.Support.V4.App.FragmentActivity
    {
        public ISharedPreferences sP;
        public ISharedPreferencesEditor sPEditor;
        public RestSharpHelper _RestSharpHelper;
        public TakePictureDialogueFragment takePictureFragment = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _RestSharpHelper = new RestSharpHelper();
            sP = GetSharedPreferences("SearchFilter", FileCreationMode.Private);
            sPEditor = sP.Edit();
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

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

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = takePictureFragment._imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                takePictureFragment._imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }
    }

    public static class App
    {
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;
    }
}