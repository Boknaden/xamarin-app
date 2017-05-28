using Android.Content;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments;
using System.Threading.Tasks;
using static ApplikasjonBoknaden.Droid.Controllers.Chat.ChatBubleController;

namespace ApplikasjonBoknaden.Droid.Controllers.Chat
{
    class ChatPickerController : RelativeLayout
    {
        private Json.Chat.Chat _Chat;
        private View Controller_View;
        private ViewGroup _Parent;
        private ViewGroup AdPack_Parent;
        private TextView ChatText;
        private TextView LastMessageText;
        private LinearLayout _LinearLayoutButton;
        private CustomFragmentActivity _CustomFragmentActivity;
        private ChatBubleType _ChatBubleType;

        public ChatPickerController(Context context) : base (context)
        {
            //Initialize();
        }

        public ChatPickerController(Context context, ViewGroup parent, Json.Chat.Chat ch, CustomFragmentActivity customFragmentActivity, ChatBubleType type) : base(context)
        {
            _ChatBubleType = type;
            _CustomFragmentActivity = customFragmentActivity;
            _Chat = ch;
            AdPack_Parent = parent;
            Initiate(parent);
        }

        public ChatPickerController(Context context, Android.Util.IAttributeSet attrs) : base (context,attrs)    
        {
            //  Initialize();
        }

        public ChatPickerController(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
        {
            // Initialize();
        }
        private void Initiate(ViewGroup parent)
        {
            _Parent = parent;
            Controller_View = LayoutInflater.From(Context).Inflate(Resource.Layout.ChatPickerControllLayout, parent, false);
            ChatText = Controller_View.FindViewById<TextView>(Resource.Id.ChatBuble_Textview);
            LastMessageText = Controller_View.FindViewById<TextView>(Resource.Id.textView_lastmessage);
            _LinearLayoutButton = Controller_View.FindViewById<LinearLayout>(Resource.Id.linearLayout_ChatPickerControllButton);
            _LinearLayoutButton.Click += delegate {
                ShowChat(_Chat);
            };

            if (_ChatBubleType == ChatBubleType.Byer)
            {
                     ChatText.Text = _Chat.Recipient.firstname + " " + _Chat.Recipient.lastname;
            }else
            {
                ChatText.Text = _Chat.Initiator.firstname + " " + _Chat.Initiator.lastname;
            }

            GetAndShowMessages(_Chat.chatid);



            parent.AddView(Controller_View);
            SetButtonValues();
        }
        private void SetButtonValues()
        {
          
        }

        private void ShowChat(Json.Chat.Chat chat)
        {
            System.Diagnostics.Debug.WriteLine("Vis melding");
            ChatDialogueFragment APDF = new ChatDialogueFragment();
            APDF.Show(_CustomFragmentActivity.SupportFragmentManager, "dialog", _CustomFragmentActivity, chat);
        }

        private async void GetAndShowMessages(int chatid)
        {
            //Gets messages based on chat id
           // Json.Messages.RootObject m = await Task.Run(() => Messages(chatid));
            Json.Messages.RootObject messages = await JsonDownloader.GetChatBubblesFromDB(SavedValues.UserValues.GetSavedToken(_CustomFragmentActivity.sP), chatid);

            int i = (messages.chatMessages.Count -1);
       
            LastMessageText.Text = BoknadenHelpers.CutAndDotString(messages.chatMessages[i].message, 40);
        }

        private async Task<Json.Messages.RootObject> Messages(int chatid)
        {
            Json.Messages.RootObject messages1 = await JsonDownloader.GetChatBubblesFromDB(SavedValues.UserValues.GetSavedToken(_CustomFragmentActivity.sP), chatid);
            //Json.Messages.ChatMessages m = messages1.chatMessages;
            return messages1;
        }
    }
}