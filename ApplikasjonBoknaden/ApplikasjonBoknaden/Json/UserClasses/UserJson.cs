using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden.Json.UserClasses
{
    class UserJson
    {
    }

    public class RootObjectTest
    {
        public string username { get; set; }
        public string passphrase { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string courseid { get; set; }
    }
}
