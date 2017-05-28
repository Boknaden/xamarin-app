using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments;
using Android.Support.V4.View;
using ApplikasjonBoknaden.Droid.ViewPageExpanders;

namespace ApplikasjonBoknaden.Droid.Fragments
{
    public class ChatPageFragment : CostumFragment
    {
        protected LinearLayout ChatsDisplayer;

        protected override int Layout()
        {
            return Resource.Layout.FragmentChatPage;
        }

        protected override void InitiateFragment()
        {

            ViewPager viewPager = Fragmentview.FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new ChatPageAdapter(this.Context, this, new ChatCatalog());
            //viewPager.Adapter = new CustomPageAdapter(this.Context, new ChatCatalog());
            ChatsDisplayer = Fragmentview.FindViewById<LinearLayout>(Resource.Id.linearLayout_Chats);
           // PagerTabStrip pts = Fragmentview.FindViewById<PagerTabStrip>(Resource.Id.linearLayout_Chats);
           // pts.
            //viewPager.Adapter = new ChatPageAdapter(this.Context, new ChatCatalog());

        }
    }
}