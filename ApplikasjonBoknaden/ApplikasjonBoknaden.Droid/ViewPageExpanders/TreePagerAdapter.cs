using System;
using Android.App;
using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Java.Lang;

using ApplikasjonBoknaden.Droid;
using ApplikasjonBoknaden.Droid.ViewPageExpanders.ApplikasjonBoknaden.Droid.ViewPageExpanders;

namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    /// <summary>
    /// This class is heavily inspired by thesee two posts/articles: 
    /// Source: https://developer.xamarin.com/guides/android/user_interface/viewpager/part-1-viewpager-and-views/
    /// Source2: http://stackoverflow.com/questions/37480733/xamarin-android-tablayout-adding-fragments-to-viewpager-instead-of-layouts
    /// </summary>
    class TreePagerAdapter : PagerAdapter
    {
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(treeCatalog[position].caption);
        }

        //  public override int Count
        //  {
        //     get { throw new NotImplementedException(); }
        //  }

        public override int Count
        {
            get { return treeCatalog.NumTrees; }
        }

        Context context;
        TreeCatalog treeCatalog;

        public TreePagerAdapter(Context context, TreeCatalog treeCatalog)
        {
            this.context = context;
            this.treeCatalog = treeCatalog;

        }

       // public override bool IsViewFromObject(View view, Java.Lang.Object obj)
      //  {
       //     throw new NotImplementedException();
      //  }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }

        //  public override Java.Lang.Object InstantiateItem(View container, int position)
        // {
        //      throw new NotImplementedException();
        //   }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            if (position == 2)
            {
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.DialogueFragmentRegisterUserLayout, container, false);
                container.AddView(view);
                ImageView userPageImage = view.FindViewById<ImageView>(Resource.Id.UserPageImageButton);

                return view;
            }

            if (position == 0)
            {
                ScrollView soldItemsScrollView = new ScrollView(context);
                GridLayout gl = new GridLayout(context);
                gl.ColumnCount = 1;
                soldItemsScrollView.AddView(gl);

                LinearLayout.LayoutParams scrollParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 2.0f);
                soldItemsScrollView.LayoutParameters = scrollParams;
                PopulateScrollTest(gl);
                container.AddView(soldItemsScrollView);

                return soldItemsScrollView;
            }

            if (position == 1)
            {
                ScrollView soldItemsScrollView = new ScrollView(context);
                GridLayout gl = new GridLayout(context);
                gl.ColumnCount = 1;
                soldItemsScrollView.AddView(gl);

                LinearLayout.LayoutParams scrollParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 2.0f);
                soldItemsScrollView.LayoutParameters = scrollParams;
                PopulateScrollTest(gl);
                container.AddView(soldItemsScrollView);

                return soldItemsScrollView;
            }



            var imageView = new ImageView(context);
            imageView.SetImageResource(treeCatalog[position].imageId);
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.AddView(imageView);
            return imageView;
        }

        private void PopulateScrollTest(GridLayout gl)
        {
            ImageButton[] btnGreen = new ImageButton[6];

            Android.Widget.RelativeLayout[] lLL = new Android.Widget.RelativeLayout[6];

            for (int i = 0; i < 6; i++)
            {

                lLL[i] = new Android.Widget.RelativeLayout(context);
                //  lLL[i].Orientation = Orientation.Vertical;
                Android.Widget.RelativeLayout.LayoutParams width = new Android.Widget.RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                Android.Widget.RelativeLayout.LayoutParams height = new Android.Widget.RelativeLayout.LayoutParams(width.Width, 400);
                lLL[i].LayoutParameters = height;
                //textbox3.setLayoutParams(width);


                // l.LayoutParameters = new TableLayout.LayoutParams(, LayoutParams.WRAP_CONTENT, 1f);


                TextView t = new TextView(context);

                ImageView iv = new ImageView(context);
                iv.SetImageResource(Resource.Drawable.design_fab_background);
                t.Text = "Databaser og krig: Pris: 250kr";
                t.SetTextColor(Android.Graphics.Color.White);
                btnGreen[i] = new ImageButton(context);
                lLL[i].AddView(btnGreen[i]);
                lLL[i].AddView(t);
                lLL[i].AddView(iv);
                // btnGreen[i].SetImageResource(R.drawable.bola_verde);
                Android.Widget.RelativeLayout.LayoutParams heightb = new Android.Widget.RelativeLayout.LayoutParams(width.Width, ViewGroup.LayoutParams.MatchParent);
                btnGreen[i].LayoutParameters = heightb;
                //  btnGreen[i].SetOnClickListener(ClickListener);
                //  btnGreen[i].SetBackgroundColor(Android.Graphics.Color.Transparent);
                //  btnGreen[i].SetTag(i);
                // btnGreen[i].SetId(i);
                //  gridView.AddView(lLL[i]);
                gl.AddView(lLL[i]);


            }
        }

     //   public override Java.Lang.Object InstantiateItem(View container, int position)
    //    {
     //       var imageView = new ImageView(context);
      //      imageView.SetImageResource(treeCatalog[position].imageId);
      //      var viewPager = container.JavaCast<ViewPager>();
       //     viewPager.AddView(imageView);
     //       return imageView;
       // }

        //  public override void DestroyItem(View container, int position, Java.Lang.Object view)
        //  {
        //     throw new NotImplementedException();
        //  }


        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }
    }
}