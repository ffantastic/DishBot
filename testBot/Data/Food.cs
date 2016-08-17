using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testBot.Data
{
    public class Food
    {
        public static HashSet<string> HatingMaterials = null;

        private static void Init()
        {
            if(HatingMaterials != null)
            {
                return;
            }

            HashSet<string> hatingMaterial = new HashSet<string>();

            foreach(var dish in Cache.TheWholeDishes)
            {
                foreach(var material in dish.Materials)
                {
                    hatingMaterial.Add(material);
                }
            }

            HatingMaterials = hatingMaterial;
        }

        public static IList<string> GetHatingMaterial(string text)
        {
            Init();

            IList<string> hatingMaterials = new List<string>();

            foreach(var material in HatingMaterials)
            {
                if (text.Contains(material))
                {
                    hatingMaterials.Add(material);
                }
            }

            return hatingMaterials;
        }
    }

}