using System;
using System.DirectoryServices.AccountManagement;

class Program
{
    static void Main(string[] args)
    {
        string groupName = "{Your Group Name}"; // 你要測試的群組名稱

        try
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                if (group != null)
                {
                    Console.WriteLine($"群組 '{groupName}' 的成員有：");
                    foreach (Principal p in group.GetMembers())
                    {
                        Console.WriteLine(p.SamAccountName);
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"找不到群組 '{groupName}'");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"發生錯誤: {ex.Message}");
        }
    }
}