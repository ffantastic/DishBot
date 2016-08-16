using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using testBot.Bean;
using testBot.Data;
using System.IO;

namespace testBot.DishBot
{
    public class MenuGenerator
    {
        public static string Generate(User user)
        {
            List<Dish> TheWholeDishes = Cache.TheWholeDishes;

            return AnalyzeTaste(user);            
        }

        private static void GetScore(List<int>WVector, List<Dish> TheWholeDishes, out List<double>Score)
        {
            Score = new List<double>();
            foreach(var eachDish in TheWholeDishes)
            {
                Score.Add(EvaluateTaste(eachDish, WVector));
            }
        }

        private static double EvaluateTaste(Dish dish, List<int> WVector)
        {
            double score = dish.Score * 20;





            return 0;
        }

        private static string AnalyzeTaste(User user)
        {
            var WVector = user.WVector;
            List<Dish> TheWholeDishes = Cache.TheWholeDishes;
            string dishStr = "";

            foreach(var each in TheWholeDishes)
            {                
                dishStr += each.Name + " " +each.Price.ToString()+";";
            }

            return dishStr;
        }

        private static void DivideByType(List<Dish> TheWholeDishes, out List<Dish>meatDishes, out List<Dish>vegetDishes, out List<Dish>soupDishes)
        {
            meatDishes = new List<Dish>();
            vegetDishes = new List<Dish>();
            soupDishes = new List<Dish>();


        }


    }

}