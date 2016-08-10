using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace testBot.Bean
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Question Next { get; set; }
        public int Priority { get; set; }
        public int NextId { get; set; }

        public Question() { }
        //public Question(SerializationInfo info, StreamingContext context)
        //{
        //    Id = (int)info.GetValue("Id", typeof(int));
        //    Content = (string)info.GetValue("Content", typeof(string));
        //    Next = (Question)info.GetValue("Next", typeof(Question));
        //    Priority = (int)info.GetValue("Priority", typeof(int));
        //    NextId = (int)info.GetValue("NextId", typeof(int));
         
        //}
        public Question(int id, string content, Question next, int priority, int nextId)
        {
            this.Id = id;
            this.Content = content;
            this.Next = next;
            this.Priority = priority;
            this.NextId = nextId;
        }
        public override string ToString()
        {
            return Content;
        }

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{

        //    info.AddValue("Id", Id, typeof(int));
        //    info.AddValue("Content", Content, typeof(string));
        //    info.AddValue("Next", Next, typeof(Question));
        //    info.AddValue("Priority", Priority, typeof(int));
        //    info.AddValue("NextId", NextId, typeof(int));
        //}
    }
}