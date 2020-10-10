using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LibPureCoolLink.Net.Controller
{
    
    public static class Utils
    {
        private static string _key = "AQIDBAUGBwgJCgsMDQ4PEBESExQVFhcYGRobHB0eHyA=";
        private static string _initVector = "AAAAAAAAAAAAAAAAAAAAAA==";
        /// <summary>
        /// Decrypts local credentials.
        /// </summary>
        /// <param name="localCredentials"></param>
        /// <returns>Decrypted credentials</returns>
        public static string DecryptLocalCredentials(string localCredentials)
        {
            var enc = new ASCIIEncoding();
            var encryptedText = enc.GetBytes(localCredentials);
          //       \x01\x02\x03\x04\x05\x06\x07\x08\t\n\x0b\x0c\r\x0e\x0f\x10' \
          // , 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f
            var key = new byte[28]
                {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x0b, 0x0c, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f};
            // var key = enc.GetBytes(_key);
            var initVector = enc.GetBytes(_initVector);


            using var aesAlgo = Aes.Create();
            aesAlgo.Mode = CipherMode.CBC;
            aesAlgo.Key = key;
            aesAlgo.IV = initVector;

            var decryptor = aesAlgo.CreateDecryptor(aesAlgo.Key, aesAlgo.IV);
            using var memoryStream = new MemoryStream(encryptedText);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            var toReturn = streamReader.ReadToEnd();

            return toReturn;
        }
    }
}