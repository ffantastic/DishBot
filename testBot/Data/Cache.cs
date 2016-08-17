using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testBot.DishBot;

namespace testBot.Data
{
    public class Cache
    {
        public static List<Dish> TheWholeDishes = new List<Dish>();
        public static Dictionary<UUID, QuestionState> QuestionStateCache = new Dictionary<UUID, QuestionState>();
    }

    public class UUID
    {
        public string ChannelId;
        public string Id;

        public UUID(string ChannelId,string Id)
        {
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
            {
                return true;
            }

            if (obj is UUID)
            {
                UUID o = obj as UUID;
                if (this.ChannelId == o.ChannelId && this.Id == o.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
    public class Dish
    {
        public string Name = "";
        public List<string> Materials = new List<string>();

        public int Price = -1;
        public double Amount = -1;
        public double SourTaste = -1;
        public double SweetTaste = -1;
        public double BitterTaste = -1;
        public double HotTaste = -1;
        public double Score = 100;
        public double Speed = -1;
        public string Type = "";

    }
}