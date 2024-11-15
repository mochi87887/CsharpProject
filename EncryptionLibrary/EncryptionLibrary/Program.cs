using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EncryptionLibrary;

class Program
{
    private static string? publicKey1;
    private static string? privateKey1;
    private static string? publicKey2;
    private static string? privateKey2;

    [STAThread]
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("請選擇功能:");
            Console.WriteLine("1. 生成RSA金鑰並匯出成txt檔");
            Console.WriteLine("2. 使用匯入的金鑰加密內容");
            Console.WriteLine("3. 使用公鑰解密內容");
            Console.WriteLine("4. 使用私鑰解密內容");
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
                    DecryptWithPublicKey();
                    break;
                case "4":
                    DecryptWithPrivateKey();
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
        using (var rsa1 = new RSACryptoServiceProvider(2048))
        using (var rsa2 = new RSACryptoServiceProvider(2048))
        {
            // 取得第一組公鑰和私鑰
            publicKey1 = Convert.ToBase64String(rsa1.ExportRSAPublicKey());
            privateKey1 = Convert.ToBase64String(rsa1.ExportRSAPrivateKey());

            // 取得第二組公鑰和私鑰
            publicKey2 = Convert.ToBase64String(rsa2.ExportRSAPublicKey());
            privateKey2 = Convert.ToBase64String(rsa2.ExportRSAPrivateKey());

            // 輸出到控制台
            Console.WriteLine("第一組公鑰:");
            Console.WriteLine(publicKey1);
            Console.WriteLine();
            Console.WriteLine("第一組私鑰:");
            Console.WriteLine(privateKey1);
            Console.WriteLine();
            Console.WriteLine("第二組公鑰:");
            Console.WriteLine(publicKey2);
            Console.WriteLine();
            Console.WriteLine("第二組私鑰:");
            Console.WriteLine(privateKey2);

            // 固定儲存位置到下載資料夾，並包含時間戳記
            string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string fileName = $"RSAKeys_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string filePath = Path.Combine(downloadFolder, fileName);

            // 將內容寫入txt檔案
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("第一組公鑰:");
            sb.AppendLine(publicKey1);
            sb.AppendLine();
            sb.AppendLine("第一組私鑰:");
            sb.AppendLine(privateKey1);
            sb.AppendLine();
            sb.AppendLine("第二組公鑰:");
            sb.AppendLine(publicKey2);
            sb.AppendLine();
            sb.AppendLine("第二組私鑰:");
            sb.AppendLine(privateKey2);

            File.WriteAllText(filePath, sb.ToString());
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
        if (keys.Length >= 8)
        {
            publicKey1 = keys[1].Trim();
            privateKey1 = keys[3].Trim();
            publicKey2 = keys[5].Trim();
            privateKey2 = keys[7].Trim();
        }
        else
        {
            Console.WriteLine("金鑰檔案格式不正確。");
            return;
        }

        Console.WriteLine("請選擇加密方式:");
        Console.WriteLine("1. 使用第一組公鑰加密");
        Console.WriteLine("2. 使用第二組私鑰加密");
        string? encryptionChoice = Console.ReadLine();

        Console.WriteLine("請輸入要加密的內容:");
        string? data = Console.ReadLine();

        if (data == null)
        {
            Console.WriteLine("輸入的內容不能為空");
            return;
        }

        string encryptedData;
        if (encryptionChoice == "1")
        {
            // 使用第一組公鑰加密
            encryptedData = RSAEncryption.EncryptWithPublicKey(data, publicKey1);
        }
        else if (encryptionChoice == "2")
        {
            // 使用第二組私鑰加密
            encryptedData = RSAEncryption.EncryptWithPrivateKey(data, privateKey2);
        }
        else
        {
            Console.WriteLine("無效的選項，請重新輸入。");
            return;
        }

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


    static void DecryptWithPublicKey()
    {
        // 輸入公鑰
        Console.WriteLine("請貼上公鑰:");
        publicKey1 = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(publicKey1))
        {
            Console.WriteLine("公鑰不能為空");
            return;
        }

        // 輸入已加密的內容
        Console.WriteLine("請輸入已加密的內容:");
        string? encryptedData = Console.ReadLine();

        if (string.IsNullOrEmpty(encryptedData))
        {
            Console.WriteLine("已加密的內容不能為空");
            return;
        }

        // 解密
        string decryptedData = RSAEncryption.DecryptWithPublicKey(encryptedData, publicKey1);
        Console.WriteLine($"解密後的內容: {decryptedData}");
    }


    static void DecryptWithPrivateKey()
    {
        // 輸入私鑰
        Console.WriteLine("請貼上私鑰:");
        privateKey1 = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(privateKey1))
        {
            Console.WriteLine("私鑰不能為空");
            return;
        }

        // 輸入已加密的內容
        Console.WriteLine("請輸入已加密的內容:");
        string? encryptedData = Console.ReadLine();

        if (string.IsNullOrEmpty(encryptedData))
        {
            Console.WriteLine("已加密的內容不能為空");
            return;
        }

        // 解密
        string decryptedData = RSAEncryption.DecryptWithPrivateKey(encryptedData, privateKey1);
        Console.WriteLine($"解密後的內容: {decryptedData}");
    }
}
