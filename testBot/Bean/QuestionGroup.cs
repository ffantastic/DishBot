using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testBot.Bean
{
    public class QuestionGroup : IComparable
    {
        public Dictionary<int, Question> Map;
        public int Priority;

        public QuestionGroup() {
        }
        public QuestionGroup(Dictionary<int, Question> m,int p) {
            this.Map = m;
            this.Priority = p;
        }

        public int CompareTo(object obj)
        {
            if (obj is QuestionGroup) {
                QuestionGroup qg = obj as QuestionGroup;
                return this.Priority - qg.Priority;
            }
            return 0;
        }
    }
    class GroupComparer : IComparer<QuestionGroup>
    {
        public int Compare(QuestionGroup x, QuestionGroup y)
        {
            return x.Priority - y.Priority;
        }
    }
}