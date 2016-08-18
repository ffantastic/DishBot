using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testBot.Data
{
    public class ChineseToDigit
    {
        private static Dictionary<string, int> hashTable;

        static ChineseToDigit()
        {
            hashTable = new Dictionary<string, int>();
            hashTable["零"] = 0;
            hashTable["一"] = 1;
            hashTable["二"] = 2;
            hashTable["三"] = 3;
            hashTable["四"] = 4;
            hashTable["五"] = 5;
            hashTable["六"] = 6;
            hashTable["七"] = 7;
            hashTable["八"] = 8;
            hashTable["九"] = 9;
            hashTable["十"] = 10;
        }

        public static int GetDigit(string text)
        {
            return hashTable[text];
        }
    }
}