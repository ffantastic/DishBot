using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using testBot.Bean;
using testBot.Data;

namespace testBot.DishBot
{
    public class QuestionState : ISerializable
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
        public QuestionState(SerializationInfo info, StreamingContext context)
        {
            Total = (int)info.GetValue("Total", typeof(int));
            Current = info.GetValue("Current", typeof(Question)) as Question;
            Id2Question = info.GetValue("Id2Question", typeof(Dictionary<int, Question>)) as Dictionary<int, Question>;
            if (Current != null) {
                int nextId = (int)info.GetValue("CurrentNextId", typeof(int));
                if (Id2Question.ContainsKey(nextId))
                    Current.Next = Id2Question[nextId];
            }
            //rebuild Linked list
            Id2Question.ToList().Where(pair=> {
                return pair.Value.NextId != -1;
            }).Select(pair=>pair.Value).ToList().ForEach(item=> {
                item.Next = Id2Question[item.NextId];
            });

            //rebuild P2QG
            P2QG = new Dictionary<int, QuestionGroup>();
            Id2Question.ToList().ForEach(pair=> {
                QuestionGroup qg;
                if (P2QG.TryGetValue(pair.Value.Priority, out qg))
                {
                    qg.Map.Add(pair.Key, pair.Value);
                }
                else {
                    qg = new QuestionGroup(new Dictionary<int, Question>() { { pair.Key, pair.Value } }, pair.Value.Priority);
                    P2QG.Add(pair.Value.Priority,qg);
                }
            });
            //rebuild Queue
            Queue = new PriorityQueue<QuestionGroup>();
            P2QG.Select(pair => pair.Value).ToList().ForEach(item=>Queue.Push(item));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Total",Total,typeof(int));
            if (Current != null)
                info.AddValue("CurrentNextId", Current.NextId, typeof(int));
            info.AddValue("Current", Current, typeof(Question));
            info.AddValue("Id2Question", Id2Question, typeof(Dictionary<int, Question>));
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