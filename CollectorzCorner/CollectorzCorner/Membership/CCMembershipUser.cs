using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CollectorzCorner.Membership
{
    public class CCMembershipUser : MembershipUser
    {
        // Might be used later
        DateTime BirthDate { get; set; }
        public int UserId { get; set; }

        public CCMembershipUser(int userId,
                                string username,
                                string email,
                                string passwordQuestion) :
                                base("CCMembershipProvider",
                                    username,
                                    null,
                                    email,
                                    passwordQuestion,
                                    null,
                                    true,
                                    false,
                                    DateTime.Now,
                                    DateTime.Now,
                                    DateTime.Now,
                                    DateTime.Now,
                                    DateTime.Now)
        {
            this.UserId = userId;
        }

        public CCMembershipUser()
        {
        }
    }
}