using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models;
using PasswordHash;

namespace Hospital.Identity
{
    public class BMUserManager : UserManager<User>
    {
        public BMUserManager() : base(new UserStore())
        {
            this.PasswordHasher = new SQLPasswordHasher();
        }
    }

    public class SQLPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            String providedPasswordHash = PasswordHash.PasswordHash.CreateHash(providedPassword);

            if(PasswordHash.PasswordHash.ValidatePassword(hashedPassword, providedPasswordHash))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }

        private string EncryptPassword(string pass, int passwordFormat, string salt)
        {
            if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                return pass;

            if (passwordFormat == 1)
            {
                String hash = PasswordHash.PasswordHash.CreateHash(pass);
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(hash));
            }
            return String.Empty;
        }


    }
}