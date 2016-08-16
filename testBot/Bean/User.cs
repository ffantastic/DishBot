using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using testBot.Data;

namespace testBot.Bean
{
    public class User : ISerializable
    {
        private static IList<int> InitVector = new List<int> { -1, -1, -1, -1, -1, -1, -1 };

        public IList<string> HatingMaterials = new List<string>();

        /// <summary>
        /// WVector[0]: instruction: 30004 means 3 men and 4 women. // 10000*nMen + nWomen.
        /// </summary>
        internal IList<int> WVector = new List<int>(InitVector);

        public User()
        {
        }

        public User(SerializationInfo info, StreamingContext context)
        {
            InitVector = info.GetValue("InitVector", typeof(IList<int>)) as IList<int>;
            HatingMaterials = info.GetValue("HatingMaterials", typeof(IList<string>)) as IList<string>;
            WVector = info.GetValue("WVector", typeof(IList<int>)) as IList<int>;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InitVector", InitVector, typeof(IList<int>));
            info.AddValue("HatingMaterials", HatingMaterials, typeof(IList<string>));
            info.AddValue("WVector", WVector, typeof(IList<int>));
        }

        public void Reset()
        {
            WVector = new List<int>(InitVector);
            HatingMaterials = new List<string>();
        }

        public void ModifyWVector(int Index, int Value = -1)
        {
            WVector[Index] = Value;
        }

        public IList<int> GetWVector()
        {
            return WVector;
        }

        public IList<string> GetHatingMaterials()
        {
            return HatingMaterials;
        }

    }

}