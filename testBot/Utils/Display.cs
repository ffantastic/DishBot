using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.Data;

namespace testBot.Utils
{
    public class Display
    {
        public static string CR = "  \n";

        public static string Show(IList<Dish> menu, User user)
        {
            string content = $"您的菜单如下：{CR}";

            double price = 0;

            foreach(var dish in menu)
            {
                content += string.Format("{0} {1}{2}", dish.Name, dish.Price, CR);
                price += dish.Price;
            }

            content += $"主食{user.GetWVector()[6]}份{CR}";

            content += $"总计{price}元。";

            return content;
        }
    }
}