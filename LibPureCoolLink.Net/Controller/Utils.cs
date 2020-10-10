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
          // key
          // 0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f20
            var key = new byte[32]
                {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x20};
            // init Vector
            // 00000000000000000000000000000000
            var initVector = new byte[16]
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };


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