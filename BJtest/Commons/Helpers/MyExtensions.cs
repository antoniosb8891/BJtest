using System;
using System.Net.Mail;

namespace BJtest.Commons.Helpers
{
    public static class MyExtensions
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                if (String.IsNullOrEmpty(emailaddress))
                    return false;
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }

    }
}
