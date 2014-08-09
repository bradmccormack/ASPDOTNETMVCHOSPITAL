using System;
using Microsoft.AspNet.Identity;

namespace Hospital.Models
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }


        public string Id
        {
            get { return UserId.ToString(); }
        }
    }

}