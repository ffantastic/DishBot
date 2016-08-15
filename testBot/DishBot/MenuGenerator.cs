using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using testBot.Bean;

namespace testBot.DishBot
{
    public class MenuGenerator
    {
        public static string Generate(User user)
        {
            AnalyzeTaste(user);

            return "这是你的菜单：大盘鸡  78元，黑椒牛柳(微辣) 52元，干锅牛蛙(中辣) 58元，白灼芥兰  24元，4碗米饭  4元";
        }

        private static void AnalyzeTaste(User user)
        {
            var WVector = user.WVector;

            
        }


    }

}