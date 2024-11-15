using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EncryptionLibrary;

class Program
{
    private static string? publicKey;
    private static string? privateKey;

    [STAThread]
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("請選擇功能:");
            Console.WriteLine("1. 生成RSA金鑰並匯出成txt檔");
            Console.WriteLine("2. 使用匯入的金鑰加密內容");
            Console.WriteLine("0. 關閉程式");
            Console.Write("請輸入選項: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateAndExportRSAKeys();
                    break;
                case "2":
                    ImportKeysAndEncryptData();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("無效的選項，請重新輸入。");
                    break;
            }
        }
    }

    static void GenerateAndExportRSAKeys()
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

            // 固定儲存位置到下載資料夾
            string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadFolder, "RSAKeys.txt");

            File.WriteAllText(filePath, $"PublicKey:{publicKey}\nPrivateKey:{privateKey}");
            Console.WriteLine("金鑰已儲存到 " + filePath);
        }
    }

    static void ImportKeysAndEncryptData()
    {
        // 輸入下載資料夾中要匯入的txt檔案名稱
        Console.WriteLine("請輸入下載資料夾中要匯入的txt檔案名稱:");
        string? fileName = Console.ReadLine();

        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("檔案名稱不能為空");
            return;
        }

        string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string filePath = Path.Combine(downloadFolder, fileName);

        if (!File.Exists(filePath))
        {
            Console.WriteLine("檔案不存在");
            return;
        }

        string[] keys = File.ReadAllLines(filePath);
        if (keys.Length == 2)
        {
            publicKey = keys[0].Replace("PublicKey:", "");
            privateKey = keys[1].Replace("PrivateKey:", "");
        }
        else
        {
            Console.WriteLine("金鑰檔案格式不正確。");
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
