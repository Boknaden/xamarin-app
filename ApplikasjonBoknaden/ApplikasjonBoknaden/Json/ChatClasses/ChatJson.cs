using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Json.Chat
{
    class ChatJson
    {
    }

    public class Initiator
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }

    public class Recipient
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }

    public class Chat
    {
        public int chatid { get; set; }
        public int initiatorid { get; set; }
        public int recipientid { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public Initiator Initiator { get; set; }
        public Recipient Recipient { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public int count { get; set; }
        public List<Chat> chats { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }

}
