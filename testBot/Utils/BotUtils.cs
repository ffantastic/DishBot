using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.Data;
using testBot.DishBot;

namespace testBot.Utils
{
    public class BotUtils
    {
        public static string GetAnswer(User user, QuestionState questionState, string text,string userId)
        {
            if (text.Contains("重新开始") || text.Contains("点菜") || text.Contains("开始点菜"))
            {
                user.Reset();
                questionState.init();
            }

            if (questionState.Current == null)
            {
                if(text.Contains("下单") || text.Contains("是") || text.Contains("确认") || text.Contains("嗯"))
                {
                    return "下单成功！";
                }

                if(text.Contains("否") || text.Contains("不"))
                {
                    return "请输入\"点菜\"重新开始";
                }
            }

            if (questionState.Current != null)
            {
                if(!AnswerProcessor.Process(questionState.Current.Id, user, text))
                {
                    string ret = Tips.Process(questionState.Current.Id);
                    try
                    {
                        ret = TuringUtils.GetTuringAnswer(userId, text) + Display.CR + $"({ret})";
                    }
                    catch
                    {
                        //log
                    }

                    return ret;
                }
                else
                {
                    if(questionState.Current.Id == 1)
                    {
                        int nMen = user.GetWVector()[0] / 10000;
                        int nWomen = user.GetWVector()[0] % 10000;
                        int budget = user.GetWVector()[1];

                        if(budget < 20 || budget/(nMen+nWomen) <= 15)
                        {
                            return "您的预算过低，请重新输入。";
                        }
                    }
                }

                while (skipNextQuestion(questionState, user))
                {
                    questionState.Next();
                }
            }

            if (questionState.IsFinished())
            {
                DishMenu menu = MenuGenerator.Generate(user);

                string ret = Display.Show(menu);
                
                user.Reset();
                questionState.init();
                return ret;
            }

            return testQuestionState(user, questionState, text);

        }

        private static string testQuestionState(User user, QuestionState questionState, string text)
        {
            //string msg = $"curent: { questionState.Current}, finished: {questionState.IsFinished()}, next: {questionState.Next()}";
            string msg = $"{questionState.Next()}";

            return msg;
        }

        private static bool skipNextQuestion(QuestionState questionState, User user)
        {
            return false;
        }

    }

}