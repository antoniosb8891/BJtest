using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BJtest.Commons.Helpers
{
    public static class Settings
    {
        private const string TokenKey = "token";
        public static readonly string TokenDefault = "";

        // ---

        public static string Token
        {
            get
            {
                return Preferences.Get(TokenKey, TokenDefault);
            }
            set
            {
                Preferences.Set(TokenKey, value);
            }
        }

    }
}
