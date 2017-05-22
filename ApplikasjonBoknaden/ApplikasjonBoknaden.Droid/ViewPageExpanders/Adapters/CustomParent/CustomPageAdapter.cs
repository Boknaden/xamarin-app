using Android.Runtime;
using Android.Content;
using Android.Views;
using Android.Support.V4.View;

namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    /// <summary>
    /// Instantiates pages based on a given Catalog
    /// </summary>
    class CustomPageAdapter : PagerAdapter
    {
        protected CustomCatalog _CustomCatalog;
        protected Context context;

        public CustomPageAdapter(Context context)
        {
            this.context = context;
        }

        public CustomPageAdapter(Context context, CustomCatalog catalog)
        {
            this.context = context;
            this._CustomCatalog = catalog;

        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_CustomCatalog[position].Header);
        }

        public override int Count
        {
            get { return _CustomCatalog.NumOfPages; }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View view = _CustomCatalog[position].GetView(container);
            container.AddView(view);
            return view;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }
    }
}