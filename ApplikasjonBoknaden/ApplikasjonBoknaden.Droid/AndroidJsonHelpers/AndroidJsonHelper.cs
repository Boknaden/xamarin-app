using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace ApplikasjonBoknaden.Droid.AndroidJsonHelpers
{
    public static class AndroidJsonHelper
    {

        public enum UserValuesEnums { userid, username, firstname, lastname, email, exp };


        public static string CleansedToken(string token)
        {
            //removes the start of the token string
            token = token.Substring(25);
            //Removes the end of the token string
            token = token.Remove(token.Length - 2);
            return token;
        }

        /// <summary>
        /// Cleans the given token and gets user values from it.
        /// </summary>
        /// <param name="token"></param>
        public static string GetValueFromToken(string token, UserValuesEnums uservalue)
        {
            string value = "";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(CleansedToken(token)) as JwtSecurityToken;
            value = jsonToken.Claims.First(claim => claim.Type == uservalue.ToString()).Value;
            Console.WriteLine(value);
            return value;
        }
    }
}
