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

namespace ApplikasjonBoknaden.Droid.AdItemClasses
{
   public class AdMiniature : RelativeLayout
    {
        public Json.Ad AdPack_Ad;

        private View AdPack_View;
        private TextView AdPack_SellerText;
        private TextView AdPack_PriceText;
        private TextView AdPack_PackNameText;
        private ImageView AdPack_ImageView;
        //private LinearLayout AdPack_HorizontalPri;
        private LinearLayout AdPack_TextFogBackground;
        private int PackPrice = 0;

        private ViewGroup AdPack_Parent;

        public AdMiniature(Context context) : base (context)
        {
            //Initialize();
        }

        public AdMiniature(Context context, ViewGroup parent, Json.Ad Ad) : base(context)
        {
            AdPack_Ad = Ad;
            AdPack_Parent = parent;
            foreach (Json.Aditem ai in Ad.aditems)
            {
                PackPrice = PackPrice + ai.price;
            }
            Initiate(parent);
        }

        public AdMiniature(Context context, Android.Util.IAttributeSet attrs) : base (context,attrs)    
        {
          //  Initialize();
        }

        public AdMiniature(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
        {
           // Initialize();
        }

        private void Initiate(ViewGroup parent)
        {
            AdPack_View = LayoutInflater.From(Context).Inflate(Resource.Layout.AdMiniatureLayout, parent, false);
            parent.AddView(AdPack_View);
            SetButtonValues();
        }

        private void SetButtonValues()
        {
            AdPack_ImageView = AdPack_View.FindViewById<ImageView>(Resource.Id.SettingsImage);
            AdPack_SellerText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_SellerText);
            AdPack_PriceText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_PriceText);
            AdPack_PackNameText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_PackNameText);
            AdPack_TextFogBackground = AdPack_View.FindViewById<LinearLayout>(Resource.Id.AdPack_TextFogBackground);
            AdPack_PackNameText.Text = AdPack_Ad.adname;
            AdPack_SellerText.Text = AdPack_Ad.user.firstname +" " + AdPack_Ad.user.lastname;
            AdPack_PriceText.Text = "Totalt:" + " " + PackPrice.ToString()  + " " + "Kr";
        }
    }
}