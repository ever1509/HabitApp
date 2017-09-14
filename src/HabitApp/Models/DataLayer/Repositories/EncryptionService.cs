using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class EncryptionService : IEncryptionService
    {
        public EncryptionService()
        {

        }
        public string CrearSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public string EncryptPassword(string pass, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPass = string.Format("{0}{1}", salt, pass);
                byte[] saltedPassAsBytes = Encoding.UTF8.GetBytes(saltedPass);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPassAsBytes));
            }
        }
    }
}
