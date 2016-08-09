using System;
using System.Collections.Generic;
using System.Linq;
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
        public Question(int id,string content,Question next,int priority,int nextId) {
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
    }
}