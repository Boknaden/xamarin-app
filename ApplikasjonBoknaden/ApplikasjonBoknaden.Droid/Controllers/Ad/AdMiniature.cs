using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ApplikasjonBoknaden.Droid.AdItemClasses
{
   public class AdMiniature : RelativeLayout
    {
        public Json.Ad AdPack_Ad;
        private Button AdPack_Button;
        private Button ShowInterestButton;

        private Json.Aditem _Product;
        private View AdPack_View;
        private TextView AdPack_SellerText;
        private TextView AdPack_PriceText;
        private TextView AdPack_PackNameText;
        private TextView AdPack_AdItemDescriptionText;
        private ImageView AdPack_ImageView;
        private int PackPrice = 0;

        private ViewGroup _Parent;

        private ViewGroup AdPack_Parent;

        private bool isPack = false;

        public AdMiniature(Context context) : base (context)
        {
            //Initialize();
        }

        public AdMiniature(Context context, ViewGroup parent, Json.Aditem product, Json.Ad Ad) : base(context)
        {
            _Product = product;
            AdPack_Ad = Ad;
            AdPack_Parent = parent;
            isPack = false;
            Initiate(parent);
        }

        public AdMiniature(Context context, ViewGroup parent, Json.Ad Ad) : base(context)
        {
            AdPack_Ad = Ad;
            AdPack_Parent = parent;
            isPack = true;
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

        public Button GetButton()
        {
            return AdPack_Button;
        }

        public Button GetShowInterestButton()
        {
            return ShowInterestButton;
        }

        public Json.Aditem GetProduct()
        {
            return _Product;
        }

        private void InitiateAsAdItem()
        {
            AdPack_AdItemDescriptionText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_AdItemDescription_Textview);
            AdPack_AdItemDescriptionText.Text = GetProduct().description;
        }

        private void InitiateAsAdPack()
        {

        }

        private void Initiate(ViewGroup parent)
        {
            _Parent = parent;

            if (isPack)
            {
                AdPack_View = LayoutInflater.From(Context).Inflate(Resource.Layout.AdMiniatureLayout, parent, false);
            }
            else
            {
                AdPack_View = LayoutInflater.From(Context).Inflate(Resource.Layout.AdItemLayout, parent, false);
            }

            parent.AddView(AdPack_View);
            SetButtonValues();
        }

        public void RemoveFromParent()
        {
            _Parent.RemoveView(this.RootView);
        }

        public void setImage(Bitmap b)
        {
            AdPack_ImageView.SetImageBitmap(b);
        }

        public void SetValues(Json.Aditem aditem)
        {
            this.AdPack_PackNameText.Text = aditem.text;
            this.AdPack_PriceText.Text = "Pris:" + " " + aditem.price.ToString() + " " + "Kr";
        }

        public void InitiateShowInterestButton(string buttonText, Boolean isred)
        {
            ShowInterestButton.Visibility = ViewStates.Visible;
            if (isred)
            {
                ShowInterestButton.SetBackgroundResource(Resource.Layout.Red_Button);
            }

            ShowInterestButton.Text = buttonText;
        }

        private void SetButtonValues()
        {
            AdPack_Button = AdPack_View.FindViewById<Button>(Resource.Id.AdPack_button);
            ShowInterestButton = AdPack_View.FindViewById<Button>(Resource.Id.ShowInterestButton);
            ShowInterestButton.Visibility = ViewStates.Gone;
            AdPack_ImageView = AdPack_View.FindViewById<ImageView>(Resource.Id.AdPack_ImageView);

            AdPack_PriceText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_PriceText);
            AdPack_PackNameText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_PackNameText);

        

            if (isPack)
            {
                AdPack_SellerText = AdPack_View.FindViewById<TextView>(Resource.Id.AdPack_SellerText);
                AdPack_SellerText.Text = AdPack_Ad.user.firstname + " " + AdPack_Ad.user.lastname;

                AdPack_PackNameText.Text = AdPack_Ad.adname;
                AdPack_PriceText.Text = PackPrice.ToString() + ",- ";
                InitiateAsAdPack();

            }
            else
            {
                AdPack_PackNameText.Text = _Product.text;
                AdPack_PriceText.Text = _Product.price.ToString() + ",-";
                InitiateAsAdItem();
            }


        }
    }
}