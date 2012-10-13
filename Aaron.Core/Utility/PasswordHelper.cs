using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aaron.Core.Security.EDO;

namespace Aaron.Core.Utility
{
    public class PasswordHash
    {
        public byte[] Password { get; set; }
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
        public int Length { get; set; }
    }

    public static class PasswordHelper
    {
        public static PasswordHash HashPassword(this string origin)
        {
            ISerializable edo = EDObjectContext.CreateInstance();
            PasswordHash hash = new PasswordHash();
            hash.IV = edo.IV;
            hash.Key = edo.Key;
            hash.Password = edo.EncryptoObject(origin);
            hash.Length = edo.TextBytesLength;

            return hash;
        }

        public static string Convert2OriginalPassword
        (
            byte[] password, 
            byte[] iv, 
            byte[] key, 
            int length
        )
        {
            ISerializable edo = EDObjectContext.CreateInstance();
            edo.IV = iv;
            edo.Key = key;
            return edo.DecryptoObject(password, length);
        }

        public static bool ChangePasswordValid(this string newPassword, string confPassword)
        {
            return newPassword == confPassword;
        }
    }
}
