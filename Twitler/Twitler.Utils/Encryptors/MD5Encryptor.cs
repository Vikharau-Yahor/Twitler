using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Twitler.Utils.Encryptors
{
    public class MD5Encryptor : IEncryptor
    {
        public Guid Encrypt(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);

            MD5CryptoServiceProvider md5Csp =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5Csp.ComputeHash(bytes);

            string hash = byteHash.Aggregate(string.Empty, (current, b) => current + $"{b:x2}");

            return new Guid(hash);
        }
    }
}
