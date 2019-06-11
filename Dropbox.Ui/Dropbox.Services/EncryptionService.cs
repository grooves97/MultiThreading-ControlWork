using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;

namespace Dropbox.Services
{
    public class EncryptionService
    {
        public static Task<string> HashPassword(string password)
        {
            return Task.FromResult(BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()));
        }

        public static Task<bool> VerifyPassword(string candidatePassword, string hashedPassword)
        {
            return Task.FromResult(BCryptHelper.CheckPassword(candidatePassword, hashedPassword));
        }
    }
}
