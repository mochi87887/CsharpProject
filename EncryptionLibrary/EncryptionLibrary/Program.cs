using System;
using System.Security.Cryptography;
using System.Text;
using EncryptionLibrary;

class Program
{
    private static string? publicKey;
    private static string? privateKey;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("請選擇功能:");
            Console.WriteLine("1. 生成RSA金鑰");
            Console.WriteLine("2. 使用生成的金鑰加密內容");
            Console.WriteLine("0. 關閉程式");
            Console.Write("請輸入選項: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateRSAKeys();
                    break;
                case "2":
                    EncryptAndDecryptData();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("無效的選項，請重新輸入。");
                    break;
            }
        }
    }

    static void GenerateRSAKeys()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            // 取得公鑰和私鑰
            publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            // 輸出到控制台
            Console.WriteLine("公鑰:");
            Console.WriteLine(publicKey);
            Console.WriteLine();
            Console.WriteLine("私鑰:");
            Console.WriteLine(privateKey);
        }
    }

    static void EncryptAndDecryptData()
    {
        if (string.IsNullOrEmpty(publicKey) || string.IsNullOrEmpty(privateKey))
        {
            Console.WriteLine("請先生成RSA金鑰 (選擇功能 1)");
            return;
        }

        Console.WriteLine("請輸入要加密的內容:");
        string? data = Console.ReadLine();

        if (data == null)
        {
            Console.WriteLine("輸入的內容不能為空");
            return;
        }

        // 加密
        string encryptedData = RSAEncryption.Encrypt(data, publicKey);
        Console.WriteLine($"加密後的內容: {encryptedData}");

        // 解密
        string decryptedData = RSAEncryption.Decrypt(encryptedData, privateKey);
        Console.WriteLine($"解密後的內容: {decryptedData}");
    }
}
