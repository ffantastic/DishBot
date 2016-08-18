using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Utils;

namespace testBot.DishBot
{
    public class Tips
    {
        private delegate string Tipper(int currentQuestionId);

        private static Dictionary<int, Tipper> hashToProcessor;

        static Tips()
        {
            hashToProcessor = new Dictionary<int, Tipper>();
            hashToProcessor[0] = processor0;
            hashToProcessor[1] = processor1;
            hashToProcessor[2] = processor2;
            hashToProcessor[3] = processor3;
            hashToProcessor[4] = processor4;
            hashToProcessor[5] = processor5;
            hashToProcessor[6] = processor6;
        }

        public static string Process(int QuestionId)
        {
            Tipper processor = hashToProcessor[QuestionId];
            return processor(QuestionId);
        }

        private static string processor0(int currentQuestionId)
        {
            return $"温馨提示，格式如：{Display.CR}1男1女";
        }

        private static string processor1(int currentQuestionId)
        {
            return $"温馨提示，格式如：{Display.CR}200";
        }

        private static string processor2(int currentQuestionId)
        {
            return $"温馨提示，数字范围是0-10";
        }

        private static string processor3(int currentQuestionId)
        {
            return $"温馨提示，数字范围是0-10";
        }

        private static string processor4(int currentQuestionId)
        {
            return $"温馨提示，数字范围是0-10";
        }

        private static string processor5(int currentQuestionId)
        {
            return $"温馨提示，格式如：{Display.CR}有/没有";
        }

        private static string processor6(int currentQuestionId)
        {
            return $"温馨提示，格式如：{Display.CR}2";
        }
    }
}