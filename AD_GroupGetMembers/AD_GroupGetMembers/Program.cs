using System;
using System.DirectoryServices.AccountManagement;

class Program
{
    static void Main(string[] args)
    {
        // 要測試的群組名稱
        string groupName = "{Your Group Name}";

        try
        {
            // 使用 PrincipalContext 來連接到 Active Directory 域
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                // 根據群組名稱查找 GroupPrincipal 物件
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                if (group != null)
                {
                    // 如果找到群組，顯示群組名稱
                    Console.WriteLine($"群組 '{groupName}' 的成員有：");
                    // 顯示群組成員
                    DisplayGroupMembers(group);
                    Console.ReadKey();
                }
                else
                {
                    // 如果找不到群組，顯示錯誤訊息
                    Console.WriteLine($"找不到群組 '{groupName}'");
                }
            }
        }
        catch (Exception ex)
        {
            // 捕捉並顯示例外錯誤訊息
            Console.WriteLine($"發生錯誤: {ex.Message}");
        }

        // 等待使用者按下任意鍵後結束程式
        Console.ReadKey();
    }

    // 顯示群組成員的方法
    static void DisplayGroupMembers(GroupPrincipal group)
    {
        // 遍歷群組中的所有成員
        foreach (Principal p in group.GetMembers())
        {
            // 如果成員是使用者
            if (p is UserPrincipal user)
            {
                // 顯示使用者的顯示名稱
                Console.WriteLine(user.DisplayName);
            }
            // 如果成員是子群組
            else if (p is GroupPrincipal subGroup)
            {
                // 顯示子群組名稱
                Console.WriteLine($"群組 '{subGroup.Name}' 的成員有：");
                // 遞迴顯示子群組成員
                DisplayGroupMembers(subGroup);
            }
        }
    }
}

