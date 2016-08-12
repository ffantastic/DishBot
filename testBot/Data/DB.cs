using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testBot.Data
{
    public class QuestionDB
    {
        public const string JsonList = @"[
            {'Id':0,'Content':'你们有几男几女','Priority':0,'NextId':1}, 
            {'Id':1,'Content':'预计花费不超过多少元','Priority':1,'NextId':-1},
            {'Id':2,'Content':'总体来讲要多辣的菜，0代表不辣，10代表辣得直喝水','Priority':2,'NextId':-1},
            {'Id':3,'Content':'菜的总体甜度呢，0代表不甜，10代表甜腻了','Priority':2,'NextId':-1},
            {'Id':4,'Content':'咸淡程度是多少，0代表不咸，10代表咸得下不了口','Priority':2,'NextId':-1},
            {'Id':5,'Content':'有不吃的菜或配料吗','Priority':5,'NextId':-1},
            {'Id':6,'Content':'你们要几份主食','Priority':5,'NextId':-1}
            ]";
    }
}