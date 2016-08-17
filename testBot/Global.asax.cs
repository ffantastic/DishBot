using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using testBot.Data;
using System.IO;

namespace testBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ReadData(Cache.TheWholeDishes, @"dishMenu.txt");
        }

        private void ReadData(List<Dish> TheWholeDishes, string filePath)
        {
            var dir = Directory.GetCurrentDirectory();
            if (File.Exists(filePath))
            {
                foreach (var eachline in File.ReadAllLines(filePath))
                {
                    string[] cols = eachline.Split(new char[] { ' ', '\t' });

                    Dish eachDish = new Dish();
                    eachDish.Name = cols[0];
                    eachDish.Price = Convert.ToInt32(cols[1]);
                    eachDish.Amount = Convert.ToDouble(cols[2]);

                    foreach (var m in cols[3].ToString().Split('/'))
                    {
                        if (!string.IsNullOrWhiteSpace(m))
                        {
                            eachDish.Materials.Add(m);
                        }
                    }

                    eachDish.SourTaste = Convert.ToDouble(cols[4]);
                    eachDish.SweetTaste = Convert.ToDouble(cols[5]);
                    eachDish.BitterTaste = Convert.ToDouble(cols[6]);
                    eachDish.HotTaste = Convert.ToDouble(cols[7]);
                    eachDish.Type = cols[8];
                    eachDish.Score = Convert.ToDouble(cols[9]);
                    eachDish.Speed = Convert.ToDouble(cols[10]);

                    TheWholeDishes.Add(eachDish);
                }
            }
        }        
    }
}
