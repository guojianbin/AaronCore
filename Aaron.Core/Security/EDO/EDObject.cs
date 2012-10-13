using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Aaron.Core.Security.EDO
{
    public class EDObject : ISerializable
    {
        private Rijndael rijndael;

        private byte[] iv;

        private byte[] key;

        private int len;

        private ICryptoTransform CreateEncryptor()
        {
            return RijndaelAlg.CreateEncryptor(Key, IV);
        }

        private ICryptoTransform CreateDecryptor()
        {
            return RijndaelAlg.CreateDecryptor(Key, IV);
        }

        public EDObject(Rijndael _rijindael)
        {
            rijndael = _rijindael;
        }

        public Rijndael RijndaelAlg
        {
            get { return rijndael; }
            set { rijndael = value; }
        }

        #region ISerializable Members

        public byte[] IV
        {
            get { return iv ?? rijndael.IV; }
            set { iv = value; }
        }

        public byte[] Key
        {
            get { return key ?? rijndael.Key; }
            set { key = value; }
        }

        public int TextBytesLength
        {
            get { return len; }
            set { len = value; }
        }

        public byte[] EncryptoObject(string str)
        {
            byte[] textBytes = (new UnicodeEncoding()).GetBytes(str);
            TextBytesLength = textBytes.Length;
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, this.CreateEncryptor(), CryptoStreamMode.Write);
            csEncrypt.Write(textBytes, 0, textBytes.Length);
            csEncrypt.Close();

            byte[] encryptedTextBytes = msEncrypt.ToArray();
            msEncrypt.Close();

            return encryptedTextBytes == null ? null : encryptedTextBytes;
        }

        public string DecryptoObject(byte[] bts, int strLength)
        {
            MemoryStream msDecrypt = new MemoryStream(bts);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, this.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] decryptedTextBytes = new Byte[strLength];
            csDecrypt.Read(decryptedTextBytes, 0, strLength);

            csDecrypt.Close();
            msDecrypt.Close();

            return decryptedTextBytes == null ? null : (new UnicodeEncoding()).GetString(decryptedTextBytes);
        }

        #endregion
    }
}
