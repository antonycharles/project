using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Application.Providers
{
    public class PasswordProvider : IPasswordProvider
    {
        private const string SALT = "$2a$11$0bCvXIYk0H2NQpgoSxfg0.";

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, SALT);
        }
    }
}