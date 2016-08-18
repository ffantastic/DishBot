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
                    // return Tips.Process(questionState.Current.Id);
                    return TuringUtils.GetTuringAnswer(userId, text);
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
            //questionState.
            if(questionState.Current.Id == 5 && user.GetWVector()[5] == 0 || 
                questionState.Current.Id == 5 && user.GetWVector()[5] == 1 && user.HatingMaterials.Count > 0)
            {
                return true;
            }
 
            return false;
        }

    }

}