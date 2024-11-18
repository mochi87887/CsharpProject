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
            Console.WriteLine("3. 貼上公鑰私鑰並解密內容");
            Console.WriteLine("4. 貼上私鑰和密文進行解密");
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
                case "3":
                    PasteKeysAndDecryptData();
                    break;
                case "4":
                    PastePrivateKeyAndDecryptData();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("無效的選項，請重新輸入。");
                    Console.WriteLine();
                    break;
            }
        }
    }

    // 數字1
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

            // 固定儲存位置到下載資料夾，並包含時間戳記
            string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string fileName = $"RSAKeys_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(downloadFolder, fileName);

            // 將內容寫入txt檔案
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("公鑰:");
            sb.AppendLine(publicKey);
            sb.AppendLine();
            sb.AppendLine("私鑰:");
            sb.AppendLine(privateKey);

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine("金鑰已儲存到 " + filePath);
            Console.WriteLine();
        }
    }

    // 數字2
    static void ImportKeysAndEncryptData()
    {
        // 輸入下載資料夾中要匯入的txt檔案名稱
        Console.WriteLine("請輸入下載資料夾中要匯入的txt檔案名稱:");
        string? fileName = Console.ReadLine();

        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("檔案名稱不能為空");
            Console.WriteLine();
            return;
        }

        string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string filePath = Path.Combine(downloadFolder, fileName);

        if (!File.Exists(filePath))
        {
            Console.WriteLine("檔案不存在");
            Console.WriteLine();
            return;
        }

        string[] keys = File.ReadAllLines(filePath);
        if (keys.Length >= 4)
        {
            publicKey = keys[1].Trim();
            privateKey = keys[3].Trim();
        }
        else
        {
            Console.WriteLine("金鑰檔案格式不正確。");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("請輸入要加密的內容:");
        string? data = Console.ReadLine();

        if (data == null)
        {
            Console.WriteLine("輸入的內容不能為空");
            Console.WriteLine();
            return;
        }

        // 加密
        string encryptedData = RSAEncryption.Encrypt(data, publicKey);
        Console.WriteLine($"加密後的內容: {encryptedData}");

        // 將輸入的內容和加密後的內容寫入txt檔案
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("輸入的內容:");
        sb.AppendLine(data);
        sb.AppendLine();
        sb.AppendLine("加密後的內容:");
        sb.AppendLine(encryptedData);

        File.AppendAllText(filePath, sb.ToString());
        Console.WriteLine("加密內容已儲存到 " + filePath);
        Console.WriteLine();
    }

    // 數字3
    static void PasteKeysAndDecryptData()
    {
        // 輸入公鑰和私鑰
        Console.WriteLine("請貼上公鑰:");
        publicKey = Console.ReadLine()?.Trim();
        Console.WriteLine();
        Console.WriteLine("請貼上私鑰:");
        privateKey = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (string.IsNullOrEmpty(publicKey) || string.IsNullOrEmpty(privateKey))
        {
            Console.WriteLine("公鑰和私鑰不能為空");
            Console.WriteLine();
            return;
        }

        // 輸入已加密的內容
        Console.WriteLine("請輸入已加密的內容:");
        Console.WriteLine();
        string? encryptedData = Console.ReadLine();

        if (string.IsNullOrEmpty(encryptedData))
        {
            Console.WriteLine("已加密的內容不能為空");
            Console.WriteLine();
            return;
        }

        // 解密
        string decryptedData = RSAEncryption.Decrypt(encryptedData, privateKey);
        Console.WriteLine($"解密後的內容: {decryptedData}");
        Console.WriteLine();
    }

    // 數字4
    static void PastePrivateKeyAndDecryptData()
    {
        // 輸入私鑰
        Console.WriteLine("請貼上私鑰:");
        privateKey = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (string.IsNullOrEmpty(privateKey))
        {
            Console.WriteLine("私鑰不能為空");
            Console.WriteLine();
            return;
        }

        // 輸入已加密的內容
        Console.WriteLine("請輸入已加密的內容:");
        Console.WriteLine();
        string? encryptedData = Console.ReadLine();

        if (string.IsNullOrEmpty(encryptedData))
        {
            Console.WriteLine("已加密的內容不能為空");
            Console.WriteLine();
            return;
        }

        // 解密
        string decryptedData = RSAEncryption.Decrypt(encryptedData, privateKey);
        Console.WriteLine($"解密後的內容: {decryptedData}");
        Console.WriteLine();
    }
}
