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

                
                 
            return testQuestionState(user, questionState, text);
        }
        private static string testQuestionState(User user, QuestionState questionState, string text) {
            string msg = $"curent { questionState.Current}, finished {questionState.IsFinished()}, next {questionState.Next()}";
            return msg;
        }
    }
}