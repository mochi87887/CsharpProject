using System;

namespace LoopTimeUse
{
    class Program
    {
        public static string startDate = "";
        public static string endDate = "";
        static void Main(string[] args)
        {
            for (int i = 100; i < 112; i++)
            {
                startDate = i.ToString() + "/01/01";
                endDate = i.ToString() + "/04/30";
                Console.WriteLine(startDate.ToString());
                Console.WriteLine(endDate.ToString());
                Console.WriteLine("第一段");

                startDate = i.ToString() + "/05/01";
                endDate = i.ToString() + "/08/31";
                Console.WriteLine(startDate.ToString());
                Console.WriteLine(endDate.ToString());
                Console.WriteLine("第二段");

                startDate = i.ToString() + "/09/01";
                endDate = i.ToString() + "/12/31";
                Console.WriteLine(startDate.ToString());
                Console.WriteLine(endDate.ToString());
                Console.WriteLine("第三段");

                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
