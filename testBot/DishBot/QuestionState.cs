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
        public Dictionary<int, QuestionGroup> P2QG { get; set; }
        public PriorityQueue<QuestionGroup> Queue { get; set; }

        private Random random = new Random();

        public QuestionState()
        {


        }
        public void init() {
            List<Question> list = loadData();
            Total = list.Count;
            P2QG = new Dictionary<int, QuestionGroup>();
            Queue = new PriorityQueue<QuestionGroup>();
            Id2Question = new Dictionary<int, Question>();
            list.ForEach(item =>
            {
                Id2Question.Add(item.Id, item);
                QuestionGroup qg;
                if (P2QG.TryGetValue(item.Priority, out qg))
                {
                    qg.Map.Add(item.Id, item);
                }
                else {
                    qg = new QuestionGroup(new Dictionary<int, Question>() { { item.Id, item} }, item.Priority);
                    P2QG.Add(qg.Priority, qg);
                }
            });
            //init linked list
            list.Where(item =>
            {
                return item.NextId != -1;
            }).Select(item => item).ToList().ForEach(item =>
            {
                item.Next = Id2Question[item.NextId];
            });

            foreach (int k in P2QG.Keys) {
                Queue.Push(P2QG[k]);
            }

            //if (Queue.Count > 0) {
            //    QuestionGroup qg = Queue.Top();
            //    if (qg.Map.Count > 0) {
            //        Current = randomSelect(qg.Map);
            //    }
            //}
            Current = null;
        }
        public Question Next()
        {
            if (Id2Question.Count == 0)
            {
                return null;
            }

            //map count >=2
            if (Current != null)
            {
                while (Current.Next != null)
                {
                    Current = Current.Next;
                    if (Id2Question.ContainsKey(Current.Id))
                    {
                        Id2Question.Remove(Current.Id);
                        QuestionGroup qg = P2QG[Current.Priority];
                        qg.Map.Remove(Current.Id);
                        return Current;
                    }
                }
            }
            while (Queue.Count > 0) {
                QuestionGroup qg = Queue.Top();
                if (qg == null || qg.Map.Count == 0) {
                    Queue.Pop();
                    continue;
                }
                Current = randomSelect(qg.Map);
                break;
            }
            
            return Current;
        }
        public bool IsFinished() {
            return Id2Question.Count == 0;
        }
        private Question randomSelect(Dictionary<int,Question> map)
        {
            int no = random.Next(map.Count);
            KeyValuePair<int, Question> pair = map.ElementAt(no);
            map.Remove(pair.Key);
            Id2Question.Remove(pair.Key);
            return pair.Value;
        }
        private List<Question> loadData()
        {
            List<Question> list = JsonConvert.DeserializeObject<List<Question>>(QuestionDB.JsonList);
            //list.Sort((x, y) =>
            //{
            //    return x.Priority - y.Priority;
            //});
            return list;
        }


    }
}