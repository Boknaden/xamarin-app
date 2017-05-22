using System;
using Android.Content;
using ApplikasjonBoknaden.JsonHelpers;
using ApplikasjonBoknaden.Droid.AndroidJsonHelpers;

namespace ApplikasjonBoknaden.Droid.SavedValues
{
    public static class UserValues
    {
        /// <summary>
        /// Saves the given user to userprefs
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="sPEditor"></param>
        public static void SaveNewUserValues(UserOld newUser, ISharedPreferencesEditor sPEditor)
        {

            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.userid.ToString(), newUser.UserID, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.firstname.ToString(), newUser.Firstname, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.lastname.ToString(), newUser.Lastname, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.email.ToString(), newUser.Email, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.username.ToString(), newUser.Username, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.verified.ToString(), newUser.verified, sPEditor);
            saveStringPrefs("Password", newUser.Password, sPEditor);
            saveStringPrefs("Token", newUser.Token, sPEditor);
        }

        public static string GetValueFromToken(ISharedPreferences sP, AndroidJsonHelper.UserValuesEnums type)
        {
            string tokenvalue = "";
            if (GetSavedToken(sP) != string.Empty)
            {
                tokenvalue = AndroidJsonHelper.GetValueFromToken(GetSavedToken(sP), type);
            }else
            {
                tokenvalue = string.Empty;
            }

            return tokenvalue;
        }

        public static void SaveToken(ISharedPreferencesEditor sPE, string token)
        {
         
            saveStringPrefs("Token", token, sPE);
        }

        public static string GetSavedToken(ISharedPreferences sP)
        {
            return getStringPrefs("Token", sP);
        }

        public static UserOld GetSavedUserValues(ISharedPreferences sP)
        {
            UserOld U = new UserOld();
            U.Firstname = GetValueFromToken(sP, AndroidJsonHelper.UserValuesEnums.firstname);
            U.Firstname = GetValueFromToken(sP, AndroidJsonHelper.UserValuesEnums.firstname);
            U.Lastname = GetValueFromToken(sP, AndroidJsonHelper.UserValuesEnums.lastname);
            U.Email = GetValueFromToken(sP, AndroidJsonHelper.UserValuesEnums.email);
            U.Username = GetValueFromToken(sP, AndroidJsonHelper.UserValuesEnums.username);
            U.Token = GetSavedToken(sP);
            U.Password = getStringPrefs("Password", sP);
            return U;
        }

        /*Gets and returns Boolean from savedUserPrefs*/
        public static Boolean getBooleanPrefs(String prefName, ISharedPreferences sP)
        {

            return sP.GetBoolean(prefName, false);
        }

        public static String getStringPrefs(String savedPrefName, ISharedPreferences sP)
        {
            return sP.GetString(savedPrefName, "Oslo");
        }

        public static void saveStringPrefs(String prefName, String value, ISharedPreferencesEditor sPEditor)
        {
            sPEditor.PutString(prefName, value);
            sPEditor.Commit();
        }

        public static void saveBooleanPrefs(String prefName, Boolean value, ISharedPreferencesEditor sPEditor)
        {
            sPEditor.PutBoolean(prefName, value);
            sPEditor.Commit();
        }
    }
}