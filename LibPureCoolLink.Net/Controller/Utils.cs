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
            var key = enc.GetBytes(_key);
            var initVector = enc.GetBytes(_initVector);


            using var aesAlgo = Aes.Create();
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