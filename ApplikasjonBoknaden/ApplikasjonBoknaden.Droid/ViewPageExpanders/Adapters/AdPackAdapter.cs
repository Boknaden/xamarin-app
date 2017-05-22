using Android.Content;
using Android.Views;
using ApplikasjonBoknaden.Json;

namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    /// <summary>
    /// This class is heavily inspired by thesee two posts/articles: 
    /// Source: https://developer.xamarin.com/guides/android/user_interface/viewpager/part-1-viewpager-and-views/
    /// Source2: http://stackoverflow.com/questions/37480733/xamarin-android-tablayout-adding-fragments-to-viewpager-instead-of-layouts
    /// </summary>
    class AdPackAdapter : CustomPageAdapter
    {
        private Ad _Ad;

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {

            return new Java.Lang.String(_Ad.aditems[position].text);
        }

        public override int Count
        {
            get { return _Ad.aditems.Count; }
        }


        public AdPackAdapter(Context context, Ad ad) : base(context)
        {
            this._Ad = ad;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View view1 = LayoutInflater.From(container.Context).Inflate(Resource.Layout.AdItemLayout, container, false);
            container.AddView(view1);
           // ViewGroup.LayoutParams p = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent,
           // ViewGroup.LayoutParams.WrapContent);
          //  view1.LayoutParameters = p;

            return view1;
        }
    }
}