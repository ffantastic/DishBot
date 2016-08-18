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
            if (text.Contains("重新开始"))
            {
                user.Reset();
                questionState.init();
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