using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Json.Messages
{
    class MessagesJson
    {
    }

    public class User
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }

    public class Row
    {
        public int chatmessageid { get; set; }
        public int userid { get; set; }
        public int chatid { get; set; }
        public string message { get; set; }
        public object imageid { get; set; }
        public int deleted { get; set; }
        public string createddate { get; set; }
        public string updateddate { get; set; }
        public User user { get; set; }
    }

    public class ChatMessages
    {
        public int count { get; set; }
        public List<Row> rows { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public ChatMessages chatMessages { get; set; }
    }
}
