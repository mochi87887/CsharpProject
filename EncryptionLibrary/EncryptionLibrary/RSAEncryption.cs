using System.Security.Cryptography;
using System.Text;

namespace EncryptionLibrary
{
    public static class RSAEncryption
    {
        /// <summary>
        /// 使用公鑰加密資料
        /// </summary>
        /// <param name="data">要加密的資料</param>
        /// <param name="publicKey">Base64 編碼的公鑰</param>
        /// <returns>Base64 編碼的加密資料</returns>
        public static string EncryptWithPublicKey(string data, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                // 匯入公鑰
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

                // 將資料轉換為位元組陣列
                var dataToEncrypt = Encoding.UTF8.GetBytes(data);

                // 使用公鑰加密資料
                var encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);

                // 將加密後的資料轉換為 Base64 字串
                return Convert.ToBase64String(encryptedData);
            }
        }

        /// <summary>
        /// 使用私鑰加密資料
        /// </summary>
        /// <param name="data">要加密的資料</param>
        /// <param name="privateKey">Base64 編碼的私鑰</param>
        /// <returns>Base64 編碼的加密資料</returns>
        public static string EncryptWithPrivateKey(string data, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                // 匯入私鑰
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

                // 將資料轉換為位元組陣列
                var dataToEncrypt = Encoding.UTF8.GetBytes(data);

                // 使用私鑰加密資料
                var encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);

                // 將加密後的資料轉換為 Base64 字串
                return Convert.ToBase64String(encryptedData);
            }
        }

        /// <summary>
        /// 使用私鑰解密資料
        /// </summary>
        /// <param name="encryptedData">Base64 編碼的加密資料</param>
        /// <param name="privateKey">Base64 編碼的私鑰</param>
        /// <returns>解密後的原始資料</returns>
        public static string DecryptWithPrivateKey(string encryptedData, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                // 匯入私鑰
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

                // 將加密資料轉換為位元組陣列
                var dataToDecrypt = Convert.FromBase64String(encryptedData);

                // 使用私鑰解密資料
                var decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.Pkcs1);

                // 將解密後的資料轉換為字串
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

        /// <summary>
        /// 使用公鑰解密資料
        /// </summary>
        /// <param name="encryptedData">Base64 編碼的加密資料</param>
        /// <param name="publicKey">Base64 編碼的公鑰</param>
        /// <returns>解密後的原始資料</returns>
        public static string DecryptWithPublicKey(string encryptedData, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                // 匯入公鑰
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

                // 將加密資料轉換為位元組陣列
                var dataToDecrypt = Convert.FromBase64String(encryptedData);

                // 使用公鑰解密資料
                var decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.Pkcs1);

                // 將解密後的資料轉換為字串
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}
