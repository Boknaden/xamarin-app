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
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.firstname.ToString(), newUser.Firstname, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.lastname.ToString(), newUser.Lastname, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.email.ToString(), newUser.Email, sPEditor);
            saveStringPrefs(AndroidJsonHelper.UserValuesEnums.username.ToString(), newUser.Username, sPEditor);
            saveStringPrefs("Password", newUser.Password, sPEditor);
            saveStringPrefs("Token", newUser.Token, sPEditor);
        }

        public static string GetSavedUsername(ISharedPreferences sP)
        {
            return getStringPrefs(AndroidJsonHelper.UserValuesEnums.username.ToString(), sP);
        }

        public static string GetSavedFirstName(ISharedPreferences sP)
        {
            return getStringPrefs(AndroidJsonHelper.UserValuesEnums.firstname.ToString(), sP);
        }

        public static string GetSavedLastName(ISharedPreferences sP)
        {
            return getStringPrefs(AndroidJsonHelper.UserValuesEnums.lastname.ToString(), sP);
        }
        public static string GetSavedToken(ISharedPreferences sP)
        {
            return getStringPrefs("Token", sP);
        }

        public static UserOld GetSavedUserValues(ISharedPreferences sP)
        {
            UserOld U = new UserOld();
            U.Firstname = getStringPrefs(AndroidJsonHelper.UserValuesEnums.firstname.ToString(), sP);
            U.Lastname = getStringPrefs(AndroidJsonHelper.UserValuesEnums.lastname.ToString(), sP);
            U.Email = getStringPrefs(AndroidJsonHelper.UserValuesEnums.email.ToString(), sP);
            U.Username = getStringPrefs(AndroidJsonHelper.UserValuesEnums.username.ToString(), sP);
            U.Password = getStringPrefs("Password", sP);
            U.Token = getStringPrefs("Token", sP);
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