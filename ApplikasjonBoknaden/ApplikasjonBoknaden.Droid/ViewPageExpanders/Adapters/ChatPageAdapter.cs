using Android.Content;
using Android.Views;
using ApplikasjonBoknaden.Json;
using Android.Widget;
using ApplikasjonBoknaden.Droid.Fragments;

namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    /// <summary>
    /// This class is heavily inspired by thesee two posts/articles: 
    /// Source: https://developer.xamarin.com/guides/android/user_interface/viewpager/part-1-viewpager-and-views/
    /// Source2: http://stackoverflow.com/questions/37480733/xamarin-android-tablayout-adding-fragments-to-viewpager-instead-of-layouts
    /// </summary>
    class ChatPageAdapter : CustomPageAdapter
    {
        private Ad _Ad;
        private Json.Chat.Chat _Chats;
        private Json.Chat.RootObject _Root;

        private LinearLayout SellingChatsDisplayer;
        private LinearLayout BuyingChatsDisplayer;
        private ChatPageFragment _ChatPageFragment;
        private string UserID = "";

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {

            return new Java.Lang.String(_CustomCatalog[position].Header);
        }

        public override int Count
        {
            get { return _CustomCatalog.NumOfPages; }
        }


        public ChatPageAdapter (Context context, Json.Chat.Chat chats, ChatPageFragment cpf) : base(context)
        {
            this._Chats = chats;
            _ChatPageFragment = cpf;
        }

        public ChatPageAdapter(Context context, ChatPageFragment cpf, CustomCatalog catalog) : base(context)
        {
            _ChatPageFragment = cpf;
            this._CustomCatalog = catalog;
            UserID = SavedValues.UserValues.GetValueFromToken(_ChatPageFragment.GetCallerActivity().sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.userid);
            GetMessages();
        }



        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View view1 = LayoutInflater.From(container.Context).Inflate(Resource.Layout.ChatPageSellingLayout, container, false);
            container.AddView(view1);


            switch (position)
            {
                case 0: SellingChatsDisplayer = view1.FindViewById<LinearLayout>(Resource.Id.linearLayout_Chats); break;
                case 1: BuyingChatsDisplayer = view1.FindViewById<LinearLayout>(Resource.Id.linearLayout_Chats);  break;
                default: break;
            }
            // ViewGroup.LayoutParams p = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent,
            // ViewGroup.LayoutParams.WrapContent);
            //  view1.LayoutParameters = p;

            return view1;
        }

        private async void GetMessages()
        {
            if (_Root == null)
            {
                _Root = await JsonDownloader.GetChatsFromDB(SavedValues.UserValues.GetSavedToken(_ChatPageFragment.GetCallerActivity().sP));
            }
            AddBuyingChats(_Root);
            AddSellingChats(_Root);
        }

        private void AddBuyingChats(Json.Chat.RootObject root)
        {
            if (root != null)
            {
                foreach (Json.Chat.Chat c in root.chats)
                {
                    if (c.Initiator.userid.ToString() == UserID)
                    {
                        Controllers.Chat.ChatPickerController cpc = new Controllers.Chat.ChatPickerController(context, BuyingChatsDisplayer, c);
                        cpc.GetLinearLayoutButton().Click += delegate {
                            ShowChat(c.chatid);
                        };
                    }
                  
                }
            }
        }

        private void AddSellingChats(Json.Chat.RootObject root)
        {
            if (root != null)
            {
                foreach (Json.Chat.Chat c in root.chats)
                {
                    if (c.Initiator.userid.ToString() != UserID)
                    {
                        Controllers.Chat.ChatPickerController cpc = new Controllers.Chat.ChatPickerController(context, BuyingChatsDisplayer, c);
                        cpc.GetLinearLayoutButton().Click += delegate {
                            ShowChat(c.chatid);
                        };
                    }
                }
            }
        }

        private void ShowChat(int chatID)
        {
           // ChatDialogueFragment APDF = new ChatDialogueFragment();
            //APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, chatID);
        }
    }
}