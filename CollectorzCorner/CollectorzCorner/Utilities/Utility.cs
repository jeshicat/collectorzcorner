using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using CryptSharp;
using CollectorzCorner.Membership;
using System.Net.Mail;

namespace CollectorzCorner.Utilities
{
    public static class Utility
    {
        public static string SaltPassword(string password)
        {
            // Turn string into byte array
            byte[] keyBytes = Encoding.ASCII.GetBytes(password);

            return (string)Crypter.Blowfish.Crypt(keyBytes, Crypter.Blowfish.GenerateSalt(10));
        }

        public static bool ValidatePassword(string password, string userPassword)
        {
            byte[] testKeyBytes = Encoding.ASCII.GetBytes(password);
            return (userPassword == Crypter.Blowfish.Crypt(testKeyBytes, userPassword));
        }

        public static void SetUserToken(CCMembershipUser cUser)
        {
            HttpContext.Current.Session["UserToken"] = cUser; 
            HttpContext.Current.Session.Timeout = 30;
        }

        public static CCMembershipUser GetUserToken()
        {
            var cUser = (CCMembershipUser) HttpContext.Current.Session["UserToken"];
            
            if(cUser == null && HttpContext.Current.User != null)
            {
                var user = DBUtility.FindUserByUsername(HttpContext.Current.User.Identity.Name);
                cUser = new CCMembershipUser(user.ID, user.Username, user.Email, user.SecurityQuestion);
                SetUserToken(cUser);
            }
                
            return cUser;
        }

        public static void RemoveUserToken()
        {
            HttpContext.Current.Session.Remove("UserToken");
        }

        public static void SendEmail(string email, string body) {
            SmtpClient smtp = new SmtpClient();
            var msg = new MailMessage();
            msg.Subject = "Contact Us from " + email;
            msg.Body = body;
            msg.To.Add("ContactUs@CollectorzCorner.com");
            msg.To.Add(email);
            msg.From = new MailAddress(email);
            

            smtp.Send(msg);
        }
    }
}