using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments;
using Android.Support.V4.View;
using ApplikasjonBoknaden.Droid.ViewPageExpanders.ApplikasjonBoknaden.Droid.ViewPageExpanders;
using ApplikasjonBoknaden.Droid.ViewPageExpanders;

namespace ApplikasjonBoknaden.Droid.Fragments
{
    public class ChatPageFragment : CostumFragment
    {
        protected override int Layout()
        {
            return Resource.Layout.FragmentChatPage;
        }

        protected override void InitiateFragment()
        {

            ViewPager viewPager = Fragmentview.FindViewById<ViewPager>(Resource.Id.viewpager);
            TreeCatalog treeCatalog = new TreeCatalog();
            viewPager.Adapter = new TreePagerAdapter(this.Context, treeCatalog);

        }
    }
}