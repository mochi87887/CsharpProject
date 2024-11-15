using System.Security.Cryptography;
using System.Text;

namespace EncryptionLibrary
{
    public static class RSAEncryption
    {
        public static string Encrypt(string data, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
                var dataToEncrypt = Encoding.UTF8.GetBytes(data);
                var encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string Decrypt(string encryptedData, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
                var dataToDecrypt = Convert.FromBase64String(encryptedData);
                var decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}
