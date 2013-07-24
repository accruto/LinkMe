using System;
using System.Security.Cryptography;
using System.Text;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Security
{
    public class LoginCredentials
    {
        [Required, LoginId]
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool MustChangePassword { get; set; }

        public static byte[] HashToBytes(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;
            var data = Encoding.UTF8.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }

        public static string HashToString(string password)
        {
            return string.IsNullOrEmpty(password) ? password : Convert.ToBase64String(HashToBytes(password));
        }
    }
}