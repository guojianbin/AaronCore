using System.Security.Cryptography;

namespace Aaron.Core.Security.EDO
{
    public interface ISerializable
    {
        byte[] IV { get; set; }

        byte[] Key { get; set; }

        int TextBytesLength { get; set; }

        byte[] EncryptoObject(string str);

        string DecryptoObject(byte[] bts, int strLength);

        Rijndael RijndaelAlg { get; set; }
    }
}
