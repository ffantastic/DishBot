using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using testBot.Bean;
using testBot.DishBot;

namespace testBot.Utils
{
    public class SessionUtils
    {
        public const string KEY_USER = "user_session_key";
        public const string KEY_QSTATE = "question_state_session_key";
        public static async Task<User> GetUser(Activity activity) {
            return await Get<User>(activity, KEY_USER);
        }
        public static async Task SetUser(Activity activity,User user)
        {
            await Set<User>(activity, KEY_USER, user);
        }
        public static async Task<QuestionState> GetQuestionState(Activity activity)
        {
            return await Get<QuestionState>(activity, KEY_QSTATE);
        }
        public static async Task SettQuestionState(Activity activity,QuestionState qs)
        {
            await Set<QuestionState>(activity, KEY_QSTATE, qs);
        }

        public static async Task Set<V>(Activity activity, string key, V v)
        {

          
       
            StateClient stateClient = activity.GetStateClient();
           
            //BotData botData = new BotData();
            //botData.SetProperty<V>(key, v);
            //await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, botData);
            //BotData response = await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, botData);

            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            userData.SetProperty<V>(key, v);
            await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
        }
        public static async Task<V> Get<V>(Activity activity, string key)
        {
            StateClient stateClient = activity.GetStateClient();
            //BotState botState = new BotState(stateClient);
            BotData botData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            V v= botData.GetProperty<V>(key);

            //StateClient stateClient = activity.GetStateClient();
            //BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            //V v = userData.GetProperty<V>(key);
            return v;
        }
    }
}