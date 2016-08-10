using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.DishBot;

namespace testBot.Data
{
    public class Cache
    {
        public static Dictionary<UUID, QuestionState> QuestionStateCache = new Dictionary<UUID, QuestionState>();
    }
    public class UUID {
        public string ChannelId;
        public string Id;

        public UUID(string ChannelId,string Id) {
            this.ChannelId = ChannelId;
            this.Id = Id;
        }
        public override int GetHashCode()
        {
            return ChannelId.GetHashCode() * 101 + Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj is UUID) {
                UUID o = obj as UUID;
                if (this.ChannelId == o.ChannelId && this.Id == o.Id
                    )
                    return true;
            }
            return false;
        }
    }
}