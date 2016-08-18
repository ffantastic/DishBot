using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.Data;
using testBot.DishBot;

namespace testBot.Utils
{
    public class Display
    {
        public static string CR = "\n\r";

        public static string Show(DishMenu menu)
        {
            if (menu.BudgetError)
            {
                return "您的预算过低，无法生成菜单";
            }

            if (menu.otherError)
            {
                return "未知错误";
            }

            string content = $"您的菜单如下：{CR}";

            foreach(var dish in menu.dishlist)
            {
                content += string.Format("{0}{1}元{2}", dish.Name.PadRight(7), dish.Price, CR);
            }

            string mainfood = string.Format("{0}份米饭", menu.MainFoodNum);
            content += string.Format("{0}{1}元{2}", mainfood.PadRight(7), menu.MainFoodNum.ToString(), CR);

            content += string.Format("总计:      {0}元" , menu.TotalCost);

            return content;
        }
    }
}