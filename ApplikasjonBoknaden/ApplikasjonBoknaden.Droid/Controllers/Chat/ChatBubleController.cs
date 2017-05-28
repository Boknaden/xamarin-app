using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace ApplikasjonBoknaden.Droid.Controllers.Chat
{
    class ChatBubleController : RelativeLayout
    {
        //public Json.Chat.Chat _Chat;
        private View Controller_View;
        private ViewGroup _Parent;
        private ViewGroup AdPack_Parent;
        private TextView ChatBubleText;
        private LinearLayout _LinearLayoutButton;
        private ChatBubleType _ChatBubleType;
        private string ChatText;

        public enum ChatBubleType
        {
            Seller,
            Byer
        }

        public ChatBubleController(Context context) : base (context)
        {
            //Initialize();
        }

        public ChatBubleController(Context context, ViewGroup parent, ChatBubleType chatBubleType, string chatbubletext) : base(context)
        {
            _ChatBubleType = chatBubleType;
            ChatText = chatbubletext;
            AdPack_Parent = parent;
            Initiate(parent);
        }

        public LinearLayout GetLinearLayoutButton()
        {
            return _LinearLayoutButton;
        }

        public ChatBubleController(Context context, Android.Util.IAttributeSet attrs) : base (context,attrs)    
        {
            //  Initialize();
        }

        public ChatBubleController(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
        {
            // Initialize();
        }

        private void Initiate(ViewGroup parent)
        {
            _Parent = parent;
            if (_ChatBubleType == ChatBubleType.Byer)
            {
                Controller_View = LayoutInflater.From(Context).Inflate(Resource.Layout.ChatBuble_Buyer, parent, false);
            }else
            {
                Controller_View = LayoutInflater.From(Context).Inflate(Resource.Layout.ChatBuble_Seller, parent, false);
            }
            ChatBubleText = Controller_View.FindViewById<TextView>(Resource.Id.ChatBuble_Textview);
            ChatBubleText.Text = ChatText;
            parent.AddView(Controller_View);
        }
        private void SetButtonValues()
        {

        }
    }
}