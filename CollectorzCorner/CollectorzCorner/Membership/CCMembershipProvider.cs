using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CryptSharp;
using System.Text;
using CollectorzCorner.Utilities;

namespace CollectorzCorner.Membership
{
    public class CCMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (CCEntities db = new CCEntities())
            {
                User user = (from u in db.Users
                             where u.Username == username
                             select u).SingleOrDefault();
                if (user != null)
                {
                    if (Utility.ValidatePassword(oldPassword, user.Password))
                    {
                        user.Password = Utility.SaltPassword(newPassword);
                        db.SaveChanges();
                        return true;
                    }
                    return true;
                }
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string securityQuestion, string securityAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = 
                new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser mUser = GetUser(username, true);

            if (mUser == null)
            { 
                User user = new User();
                user.Username = username;
                user.Password = Utilities.Utility.SaltPassword(password);
                user.SecurityQuestion = securityQuestion;
                user.SecurityAnswer = securityAnswer;
                user.Email = email;
                user.CreationDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                user.IsApproved = false;
                user.IsLockedOut = false;

                using (CCEntities db = new CCEntities())
                {
                    db.Users.AddObject(user);
                    db.SaveChanges();
                }

                status = MembershipCreateStatus.Success;

                return GetUser(username, true);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }
        
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (CCEntities db = new CCEntities())
            {
                User user = (from u in db.Users
                             where u.Username == username
                             select u).SingleOrDefault();

                if (user != null) return new CCMembershipUser(user.ID, user.Username, user.Email, user.SecurityQuestion);
                return null;
            }
        }
        
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            using (CCEntities db = new CCEntities())
            {
                User user = (from u in db.Users
                             where u.Email == email
                             select u).SingleOrDefault();

                if (user != null) return user.Username;

                return string.Empty;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            using (CCEntities db = new CCEntities())
            {
                
                User user = DBUtility.FindUserByUsername(username);

                if (user != null)
                {
                    // Tests to see if entered password matches the database password.
                    //byte[] testKeyBytes = Encoding.ASCII.GetBytes(password);
                    //bool matches = (user.Password == Crypter.Blowfish.Crypt(testKeyBytes, user.Password));

                    if (Utility.ValidatePassword(password,user.Password))
                    {
                        // Sets a session variable with user info we will need later.
                        Utility.SetUserToken(new CCMembershipUser(user.ID, user.Username, user.Email, user.SecurityQuestion));

                        // Sets last login date to current date.
                        user.LastLoginDate = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }
    }
}