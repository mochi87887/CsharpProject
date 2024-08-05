using System;
using System.Collections.Generic;

namespace Array_and_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        public static void ArrayFP() 
        {
            //宣告了一個要放很多個【string】的Array
            //變數名稱是【DayOfWeek】，這個Array總共要放【7】個【string】型態的東西
            string[] DayOfWeek = new string[7];

            DayOfWeek[0] = "星期一";
            DayOfWeek[1] = "星期二";
            DayOfWeek[2] = "星期三";
            DayOfWeek[3] = "星期四";
            DayOfWeek[4] = "星期五";
            DayOfWeek[5] = "星期六";
            DayOfWeek[6] = "星期日";

            //另類寫法
            //string[] DayOfWeek = { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
        }


        public static void ListFP()
        {
            //宣告了一個List變數，變數名稱【DayOfWeek】
            List<string> DayOfWeek = new List<string>();

            //List使用前必須要先透過【Add()】方式將資料加入List中，之後一樣可以透過變數名稱加中括弧[]與索引值來指定資料
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);
            DayOfWeek.Add(null);

            DayOfWeek[0] = "星期一";
            DayOfWeek[1] = "星期二";
            DayOfWeek[2] = "星期三";
            DayOfWeek[3] = "星期四";
            DayOfWeek[4] = "星期五";
            DayOfWeek[5] = "星期六";
            DayOfWeek[6] = "星期日";
        }
    }
}
