using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplikasjonBoknaden
{
    public static class BoknadenHelpers
    {
        public static string[] NotAcceptecUsernames = new string[] { "Fuck", "fuck", "Faen", "faen", "Helvete", "helvete", "Fitte", "fitte", "Satan", "satan", "Hitler", "hitler", "Hore", "hore" };


        public static string CleansedToken(string token, int removefront, int removeback)
        {
            //removes the start of the token string
            token = token.Substring(removefront);
            //Removes the end of the token string
            if (removeback != 0)
            {
                token = token.Remove(token.Length - removeback);
            }

            return token;
        }

        //  public static string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        /// <summary>
        /// Compares two strings and returns true or false, based on if they are of the same value
        /// </summary>
        /// <param name="valueOne"></param>
        /// <param name="valueTwo"></param>
        /// <returns></returns>
        public static bool StringsHaveSameValue(string valueOne, string valueTwo)
        {
            if (valueOne == valueTwo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///Checks if given string is a valid Email Source: http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmailIsRightFormat(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if given string contains any bad words. If it does, it returns the bad word. If not, it returns Null.
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static String CheckStringForBadWord(string st)
        {
            foreach (string s in NotAcceptecUsernames)
            {
                if (st.Contains(s))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if string is Empty or null
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static bool StringIsEmpty(string st)
        {
            if (st == "" || st == " " || st == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if given string is long enough, compared to given int.
        /// </summary>
        /// <param name="st"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static bool StringIsLongEnough(string st, int lenght)
        {
            if (st.Length < lenght)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Cheks if given string is to long, compared to given int
        /// </summary>
        /// <param name="st"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static bool StringIsToLong(string st, int givenlenght)
        {
            if (st.Length > givenlenght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
