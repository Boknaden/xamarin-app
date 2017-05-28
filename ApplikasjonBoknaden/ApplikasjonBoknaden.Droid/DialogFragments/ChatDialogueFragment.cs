using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using ApplikasjonBoknaden.Json;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    class ChatDialogueFragment : CustomDialogFragment
    {
        private LinearLayout _ChatBublesLayout;
        private EditText _EditText;
        private Button SendMessageButton;
        private Json.Chat.Chat TheChat;

        public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, Json.Chat.Chat chat)
        {
            this.TheChat = chat;
            this.CallerActivity = caller;
            base.Show(manager, tag);
            GetAndShowMessages(TheChat);
        }

        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentChat;
        }

        protected override void InitiateFragment()
        {
            _ChatBublesLayout = Dialogueview.FindViewById<LinearLayout>(Resource.Id.linearLayout_ChatBubles);
            _EditText = Dialogueview.FindViewById<EditText>(Resource.Id.editTextEmailLogin);
            SendMessageButton = Dialogueview.FindViewById<Button>(Resource.Id.Button_SendMessage);
            SendMessageButton.Click += delegate {
                SendNewMessage();
            };
          

        }

        private async void SendNewMessage()
        {
            NewMessageTest nmt = new NewMessageTest();
            nmt.message = _EditText.Text;
            nmt.chatid = this.TheChat.chatid;
           // nmt._Recipient = TheChat.Recipient;
           // nmt._Chat = this.TheChat;
            //nmt.ChatId = this.TheChat.chatid;
            nmt.recipientid = this.TheChat.recipientid;
            await Json.JsonUploader.AddNewMessage(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), "Ny melding", nmt);
            _EditText.Text = "";
        }



        private async void GetAndShowMessages(Json.Chat.Chat chat)
        {
            //Gets messages based on chat id
            Json.Messages.RootObject m = await JsonDownloader.GetChatBubblesFromDB(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), chat.chatid);
            //Shows the chat
            ShowChat(m);
        }

       // private async Task<Json.Messages.RootObject> Messages(int chatid)
       // {
           // MessagesRoot = await JsonDownloader.GetChatBubblesFromDB(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), chatid);
            //Json.Messages.ChatMessages m = messages1.chatMessages;
            //return MessagesRoot;
       // }

        private void ShowChat(Json.Messages.RootObject cm)
        {
            //System.Diagnostics.Debug.WriteLine(cm.chatMessages[1].ToString() + "Dette er meldingen ny igjen!");

            System.Diagnostics.Debug.WriteLine("viser chatt ny");
            foreach (Json.Messages.ChatMessage c in cm.chatMessages)
            {
                if (c.userid.ToString() == SavedValues.UserValues.GetValueFromToken(CallerActivity.sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.userid))
                {
                    System.Diagnostics.Debug.WriteLine(SavedValues.UserValues.GetValueFromToken(CallerActivity.sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.userid) + "Dette er user ID");
                    Controllers.Chat.ChatBubleController cpc = new Controllers.Chat.ChatBubleController(Context, _ChatBublesLayout, Controllers.Chat.ChatBubleController.ChatBubleType.Seller, c.message);

                }
                else
                {
                    Controllers.Chat.ChatBubleController cpc = new Controllers.Chat.ChatBubleController(Context, _ChatBublesLayout, Controllers.Chat.ChatBubleController.ChatBubleType.Byer, c.message);
                }

                // c.message
                System.Diagnostics.Debug.WriteLine("Melding");
                // cpc.GetLinearLayoutButton().Click += delegate {
                //    ShowChat(c.chatid);
                // };
            }
        }
    }
}