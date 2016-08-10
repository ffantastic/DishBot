using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testBot.Data
{
    public class QuestionDB
    {
        public const string JsonList = @"[
            {'Id':0,'Content':'question1','Priority':0,'NextId':1}, 
            {'Id':1,'Content':'question2','Priority':1,'NextId':5},
            {'Id':2,'Content':'question3','Priority':2,'NextId':-1},
            {'Id':3,'Content':'question4','Priority':2,'NextId':-1},
            {'Id':4,'Content':'question5','Priority':2,'NextId':-1},
            {'Id':5,'Content':'question6','Priority':2,'NextId':-1}
            ]";
    }
}