using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.DishBot;

namespace testBot.Utils
{
    public class BotUtils
    {
        public static string GetAnswer(User user, QuestionState questionState, string text)
        {
            // test here
            MenuGenerator.Generate(user);
            if (questionState.Current != null && !questionState.IsFinished())
            {
                if(!AnswerProcessor.Process(questionState.Current.Id, user, text))
                {
                    return "输入不合法，请重新回答";
                }
            }

            if (questionState.IsFinished())
            {
                string ret = MenuGenerator.Generate(user);
                if (ret != null)
                {
                    user.Reset();
                    questionState.init();
                    return ret;
                }
                else
                {
                    user.Reset();
                    questionState.init();
                    return "菜单生成错误";
                }
            }

            return testQuestionState(user, questionState, text);

        }

        private static string testQuestionState(User user, QuestionState questionState, string text)
        {
            //string msg = $"curent: { questionState.Current}, finished: {questionState.IsFinished()}, next: {questionState.Next()}";
            string msg = $"{questionState.Next()}";

            return msg;
        }

    }
}