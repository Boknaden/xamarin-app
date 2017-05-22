using Android.Content;
using Android.Views;
using Android.Widget;

namespace ApplikasjonBoknaden.Droid.Controllers.Chat
{
    class ChatPickerController : RelativeLayout
    {
        public Json.Chat.Chat _Chat;
        private View Controller_View;
        private ViewGroup _Parent;
        private ViewGroup AdPack_Parent;
        private TextView ChatText;
        private LinearLayout _LinearLayoutButton;


        public ChatPickerController(Context context) : base (context)
        {
            //Initialize();
        }

        public ChatPickerController(Context context, ViewGroup parent, Json.Chat.Chat ch) : base(context)
        {
            _Chat = ch;
            AdPack_Parent = parent;
            Initiate(parent);
        }

        public LinearLayout GetLinearLayoutButton()
        {
            return _LinearLayoutButton;
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
            _LinearLayoutButton = Controller_View.FindViewById<LinearLayout>(Resource.Id.linearLayout_ChatPickerControllButton);

            ChatText.Text = _Chat.Recipient.firstname + " " + _Chat.Recipient.lastname;
            parent.AddView(Controller_View);
            SetButtonValues();
        }
        private void SetButtonValues()
        {
          
        }
    }
}