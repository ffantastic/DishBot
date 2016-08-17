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
        public static string CR = "  \n";

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
                content += string.Format("{0} {1}{2}", dish.Name, dish.Price, CR);
            }

            content += $"主食{menu.MainFoodNum}份{CR}";

            content += $"总计{menu.TotalCost}元。";

            return content;
        }
    }
}