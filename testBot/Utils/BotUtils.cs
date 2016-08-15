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
            if(questionState.Current != null)
            {
                if(!AnswerProcessor.Process(questionState.Current.Id, user, text))
                {
                    return "输入不合法，请重新回答";
                }

                while(skipNextQuestion(questionState, user))
                {
                    questionState.Next();
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

        private static bool skipNextQuestion(QuestionState questionState, User user)
        {
            //questionState.
            if(questionState.Current.Id == 5 && user.GetWVector()[5] == 0 || 
                questionState.Current.Id == 5 && user.GetWVector()[5] == 1 && user.GetNotEatFood().Count > 0)
            {
                return true;
            }

            return false;
        }

    }
}