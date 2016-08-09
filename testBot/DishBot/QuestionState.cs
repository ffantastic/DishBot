using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.Bean;
using testBot.Data;

namespace testBot.DishBot
{
    public class QuestionState
    {
        public int Total { get; set; }
        public Question Current { get;  set; }
        public Dictionary<int, Question> Id2Question { get; set; }
        private Random random = new Random();

        public QuestionState()
        {


        }
        public void init() {
            List<Question> list = loadData();
            Total = list.Count;

            //init linked list
            Id2Question = new Dictionary<int, Question>();
            list.ForEach(item =>
            {
                Id2Question.Add(item.Id, item);
            });
            list.Where(item =>
            {
                return item.NextId != -1;
            }).Select(item => item).ToList().ForEach(item =>
            {
                item.Next = Id2Question[item.NextId];
            });

            Current = list[0];
        }
        public Question Next()
        {
            if (Id2Question.Count == 0)
            {
                return null;
            }

            if (Id2Question.Count == 1)
            {
                Id2Question.Clear();
                Current = null;
                return null;
            }
            //map count >=2
            if (Current != null)
            {
                Id2Question.Remove(Current.Id);
                while (Current.Next != null)
                {
                    Current = Current.Next;
                    if (Id2Question.ContainsKey(Current.Id))
                        return Current;
                }
            }

            Current = randomSelect();
            return Current;
        }
        public bool IsFinished() {
            return Id2Question.Count == 0;
        }
        private Question randomSelect()
        {
            int no = random.Next(Id2Question.Count);
            KeyValuePair<int, Question> pair = Id2Question.ElementAt(no);
            return pair.Value;
        }
        private List<Question> loadData()
        {
            List<Question> list = JsonConvert.DeserializeObject<List<Question>>(QuestionDB.JsonList);
            list.Sort((x, y) =>
            {
                return x.Priority - y.Priority;
            });
            return list;
        }


    }
}