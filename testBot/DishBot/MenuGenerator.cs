using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.Utils;

namespace testBot.DishBot
{
    public class MenuGenerator
    {
        public static string Generate(User user)
        {
            return $"这是您的菜单：{Display.CR}大盘鸡 78元{Display.CR}黑椒牛柳(微辣) 52元{Display.CR}干锅牛蛙(中辣) 58元{Display.CR}白灼芥兰 24元{Display.CR}4碗米饭 4元";
        }
    }
}