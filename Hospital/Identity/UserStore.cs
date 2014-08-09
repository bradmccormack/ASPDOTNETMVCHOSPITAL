using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Hospital.Models;
using System.Data;


namespace Hospital.Identity
{
    public class UserStore : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        private readonly string connectionString;

        public UserStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            this.connectionString = connectionString;
        }

        public UserStore()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Dispose()
        {

        }

        #region IUserStore
      
        public virtual Task<User> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                User user = new User();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand("SELECT Id, UserName, Password, SecurityStamp FROM BookingStaff WHERE Id = @id", connection);
                    command.Parameters.AddWithValue("@id", userId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        Boolean read = reader.Read();
                        if (read)
                        {
                            user.UserId = (int)reader[0];
                            user.UserName = reader[1].ToString();
                            user.PasswordHash = reader[2].ToString();
                            user.SecurityStamp = reader[0].ToString(); // just set it to the ID for now.. TODO figure this out
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return user;
            });
        }

        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                User user = new User();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
            
                    SqlCommand command = new SqlCommand("SELECT Id, UserName, Password, SecurityStamp FROM [dbo].[BookingStaff] WHERE lower(UserName) = lower(@username)" , connection);
                    command.Parameters.AddWithValue("@username", userName);

                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                        Boolean read = reader.Read();
                        if (read)
                        {
                            user.UserId = (int)reader[0];
                            user.UserName = reader[1].ToString();
                            user.PasswordHash = reader[2].ToString();
                            user.SecurityStamp = reader[0].ToString(); // just set it to the ID for now.. TODO figure this out
                        }
                        reader.Close();
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return user;
            });
        }

        #endregion

 
        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion


        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }
}