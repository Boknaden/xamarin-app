using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using static Android.Provider.ContactsContract.CommonDataKinds;
using System.Text.RegularExpressions;

namespace ApplikasjonBoknaden
{
    public static class InputValidator
    {
        /// <summary>
        /// Returns true if all digits are numbers. Returns false if thats not the case. 
        /// (Source: http://stackoverflow.com/questions/7461080/fastest-way-to-check-if-string-contains-only-digits)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if given string is a valid Username
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public static ValidationResponse validISBN(string isbn)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!IsDigitsOnly(isbn))
            {
                vR.Successful = false;
                vR.Information = "ISBN skal bare bestå av nummer";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(isbn))
            {
                vR.Successful = false;
                vR.Information = "ISBN kan ikke være tomt";
                return vR;
            }

            if (!BoknadenHelpers.StringIsLongEnough(isbn, 10))
            {
                vR.Successful = false;
                vR.Information = "ISBN er for kort";
                return vR;
            }

            if (BoknadenHelpers.StringIsToLong(isbn, 13))
            {
                vR.Successful = false;
                vR.Information = "ISBN er for lang";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }

        /// <summary>
        /// Checks if given string is a valid Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static ValidationResponse validUsername(string username)
        {
            ValidationResponse vR = new ValidationResponse();

            string badWord = BoknadenHelpers.CheckStringForBadWord(username);
            if (badWord != null)
            {
                vR.Successful = false;
                vR.Information = "Brukernavn består av skitne ord. Venligst bytt ut" + " " + "'" + badWord + "'";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(username))
            {
                vR.Successful = false;
                vR.Information = "Brukernavn kan ikke være tomt";
                return vR;
            }

            if (!BoknadenHelpers.StringIsLongEnough(username, 5))
            {
                vR.Successful = false;
                vR.Information = "Brukernavnet er for kort";
                return vR;
            }

            if (BoknadenHelpers.StringIsToLong(username, 10))
            {
                vR.Successful = false;
                vR.Information = "Brukernavnet er for langt";
                return vR;
            }


            vR.Successful = true;
            vR.Information = "";
            return vR;
        }

        /// <summary>
        ///Checks if given string is a valid Email Source: http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static ValidationResponse validEmail(string email)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!BoknadenHelpers.EmailIsRightFormat(email))
            {
                vR.Successful = false;
                vR.Information = "Epost er i feil format";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(email))
            {
                vR.Successful = false;
                vR.Information = "E-post kan ikke være tom";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }


        public static ValidationResponse validFirstname(string firstname)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!BoknadenHelpers.StringIsLongEnough(firstname, 2))
            {
                vR.Successful = false;
                vR.Information = "Navn er for kort";
                return vR;
            }

            if (BoknadenHelpers.StringIsToLong(firstname, 15))
            {
                vR.Successful = false;
                vR.Information = "Navn er for langt";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(firstname))
            {
                vR.Successful = false;
                vR.Information = "Navn kan ikke være tomt";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }

        public static ValidationResponse validLastname(string lastname)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!BoknadenHelpers.StringIsLongEnough(lastname, 2))
            {
                vR.Successful = false;
                vR.Information = "Etternavn er for kort";
                return vR;
            }

            if (BoknadenHelpers.StringIsToLong(lastname, 15))
            {
                vR.Successful = false;
                vR.Information = "Etternavn er for langt";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(lastname))
            {
                vR.Successful = false;
                vR.Information = "Etternavn kan ikke være tomt";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }

        public static ValidationResponse validPassword(string password)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!BoknadenHelpers.StringIsLongEnough(password, 5))
            {
                vR.Successful = false;
                vR.Information = "Passord er for kort. Minimum 5 siffer/tall";
                return vR;
            }

            if (BoknadenHelpers.StringIsToLong(password, 15))
            {
                vR.Successful = false;
                vR.Information = "Passord er for langt. Max 15 siffer/tall";
                return vR;
            }

            if (BoknadenHelpers.StringIsEmpty(password))
            {
                vR.Successful = false;
                vR.Information = "Passord kan ikke være tomt";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }


        public static ValidationResponse validRepeatedPassword(string password, string repeatedPassword)
        {
            ValidationResponse vR = new ValidationResponse();

            if (!BoknadenHelpers.StringsHaveSameValue(password, repeatedPassword))
            {
                vR.Successful = false;
                vR.Information = "Passordene er ikke like";
                return vR;
            }

            vR.Successful = true;
            vR.Information = "";
            return vR;
        }
    }

    public class ValidationResponse
    {
        public bool Successful { get; set; }
        public string Information { get; set; }
    }

}




