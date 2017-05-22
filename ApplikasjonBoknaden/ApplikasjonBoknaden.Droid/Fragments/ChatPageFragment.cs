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
            GetMessages();
            //viewPager.Adapter = new ChatPageAdapter(this.Context, new ChatCatalog());

        }
        private async void GetMessages()
        {
            Json.Chat.RootObject messages = await JsonDownloader.GetChatsFromDB(SavedValues.UserValues.GetSavedToken(CallerActivity.sP));
            if (messages != null)
            {
                AddChats(messages);
            }
     
        }
        private void AddChats(Json.Chat.RootObject root)
        {
            if (root != null)
            {
                foreach (Json.Chat.Chat c in root.chats)
                {
                    Controllers.Chat.ChatPickerController cpc = new Controllers.Chat.ChatPickerController(Context, ChatsDisplayer, c);
                    cpc.GetLinearLayoutButton().Click += delegate {
                        ShowChat(c.chatid);
                    };
                }
            }
        }
        private void ShowChat(int chatID)
        {
            ChatDialogueFragment APDF = new ChatDialogueFragment();
            APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, chatID);
        }
    }
}