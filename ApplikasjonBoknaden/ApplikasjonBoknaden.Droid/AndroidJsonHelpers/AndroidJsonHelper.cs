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
        public enum UserValuesEnums { userid, username, firstname, lastname, email, exp, verified };

        /// <summary>
        /// Cleans the given token and gets user values from it.
        /// </summary>
        /// <param name="token"></param>
        public static string GetValueFromToken(string token, UserValuesEnums uservalue)
        {
            string value = "";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            value = jsonToken.Claims.First(claim => claim.Type == uservalue.ToString()).Value;
            Console.WriteLine(value);
            return value;
        }
    }
}
