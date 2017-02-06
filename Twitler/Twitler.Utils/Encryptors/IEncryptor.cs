using System;

namespace Twitler.Utils.Encryptors
{
    public interface IEncryptor
    {
        Guid Encrypt(string value);
    }
}
