using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    class ChatDialogueFragment : CustomDialogFragment
    {
        private int ChatID = 0;
        private Json.Messages.ChatMessages _ChatMessages;
        private LinearLayout _ChatBublesLayout;

        public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, int chatID)
        {
            this.CallerActivity = caller;
            ChatID = chatID;
            base.Show(manager, tag);
        }

        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentChat;
        }

        protected override void InitiateFragment()
        {
            _ChatBublesLayout = Dialogueview.FindViewById<LinearLayout>(Resource.Id.linearLayout_ChatBubles);
            GetAndShowMessages(ChatID);

        }

        private async void GetAndShowMessages(int chatid)
        {
            //Gets messages based on chat id
            Json.Messages.ChatMessages m = await Task.Run(() => Messages(chatid));
            //Shows the chat
            ShowChat(m);
        }

        private async Task<Json.Messages.ChatMessages> Messages(int chatid)
        {
            Json.Messages.RootObject messages1 = await JsonDownloader.GetChatBubblesFromDB(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), chatid);
            Json.Messages.ChatMessages m = messages1.chatMessages;
            return m;
        }

        private void ShowChat(Json.Messages.ChatMessages cm)
        {
            System.Diagnostics.Debug.WriteLine(cm.rows[1].message.ToString() + "Dette er meldingen ny igjen!");

            System.Diagnostics.Debug.WriteLine("viser chatt ny");
            foreach (Json.Messages.Row c in cm.rows)
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