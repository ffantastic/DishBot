using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Data;

namespace testBot.Bean
{
    public class NotEats
    {
        private static Dictionary<string, Food> hashTable;

        static NotEats()
        {
            hashTable = new Dictionary<string, Food>();

            hashTable["羊"] = Food.YangRou;
            hashTable["牛"] = Food.NiuRou;
            hashTable["猪"] = Food.ZhuRou;
            hashTable["狗"] = Food.GouRou;
            hashTable["鸡蛋"] = Food.JiDan;
            hashTable["牛蛙"] = Food.NiuWa;
            hashTable["蛇"] = Food.SheRou;

            hashTable["香菜"] = Food.XiangCai;
            hashTable["葱"] = Food.Cong;
            hashTable["蒜"] = Food.Suan;
            hashTable["大葱"] = Food.DaCong;
            hashTable["酸菜"] = Food.SuanCai;
            hashTable["姜"] = Food.Jiang;
        }

        public static IList<Food> GetNotEats(string text)
        {
            IList<Food> notEats = new List<Food>();

            foreach(var table in hashTable)
            {
                if (text.Contains(table.Key))
                {
                    notEats.Add(table.Value);
                }
            }

            return notEats;
        }

    }

}