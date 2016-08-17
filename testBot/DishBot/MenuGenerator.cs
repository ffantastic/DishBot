using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using testBot.Bean;
using testBot.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace testBot.DishBot
{
    public class Menu
    {
        public List<Dish> dishlist;
        public int MainFoodNum = 0;
        public int TotalCost = 0;
        public int NumberOfFood = 0;
        public bool BudgetError = false;
        public bool otherError = false;
        public string message = "The menu is ready";

    }

    public class MenuGenerator
    {
        public static Menu Generate(User user)
        {           

            List<int> taste = new List<int>(user.WVector);

            List<Dish> clientMenu = new List<Dish>(Cache.TheWholeDishes);
            clientMenu = RemoveHatingFood(user, clientMenu);
            
            // score for every dishes
            for(int i = 0; i < clientMenu.Count; ++i)
            {
                EvaluateTaste(clientMenu[i], taste);
            }

            // divided into four types
            List<Dish> coldDish;
            List<Dish> meatDish;
            List<Dish> vegeDish;
            List<Dish> soupDish;
            HashSet<string> Material = new HashSet<string>();
            DivideByType(clientMenu, out coldDish, out meatDish, out vegeDish, out soupDish);


            // recommend algorithm
            int people = user.WVector[0] / 10000 + user.WVector[0] % 10000;
            List<int> dishType = new List<int> { 3, 2, 4, 3, 2, 1, 1, 3, 2, 1, 2, 1, 1, 4, 3, 2, 3, 2, 4 ,3 , 2, 4, 3, 3, 2, 4, 3, 2, 1, 1, 3, 2, 1, 2, 1, 1, 4, 3, 2, 3, 2, 4, 3 , 3, 2,};
            int maxDishNum = 2 + people;
            int maxCost = user.WVector[1];
            maxCost -= user.WVector[6];
            int cost = 0;
            int dishNum = 0;
            int idx = 0;

            // 
            Menu retMenu = new Menu();
            if (user.WVector[6] < 0)
            {
                retMenu.otherError = true;
                retMenu.message = "Error: the number of main is below zero???";
                return retMenu;
            }

            retMenu.MainFoodNum = user.WVector[6];
            if(maxCost < 20 || maxCost/people <= 15)
            {
                retMenu.BudgetError = true;
                retMenu.message = "The budget is insufficient";
                return retMenu;
            }

            while (cost < maxCost && dishNum < maxDishNum && idx < 40)
            {
                Dish select = null;
                switch(dishType[idx])
                {
                    case 1:
                        select = SelectDish(coldDish, Material);
                        break;
                    case 2:
                        select = SelectDish(coldDish, Material);
                        break;
                    case 3:
                        select = SelectDish(coldDish, Material);
                        break;
                    case 4:
                        select = SelectDish(coldDish, Material);
                        break;
                }

                if(select != null)
                {
                    if (cost + select.Price < maxCost)
                    {
                        cost = cost + select.Price;
                        ++dishNum;
                        retMenu.dishlist.Add(select);
                    }
                }
                ++idx;
            }

            return retMenu;                       
        }

        private static Dish SelectDish(List<Dish>inputDish, HashSet<string> Material)
        {
            Dish ret = null;
            while(inputDish.Any() && Material.Contains(inputDish[0].Materials[0]))
            {
                inputDish.RemoveAt(0);
            }

            if(inputDish.Any())
            {
                ret = inputDish[0];
                inputDish.RemoveAt(0);
            }
            return ret;
        }

        private static List<Dish> RemoveHatingFood(User user, List<Dish> clientMenu)
        {
            List<Dish> remainMenu = new List<Dish>();

            foreach(var dish in clientMenu)
            {
                if(!dish.Materials.Intersect(user.NotEatFood).ToList().Any())
                {
                    remainMenu.Add(dish);
                }                
            }

            return remainMenu;
        }
        

        private static double EvaluateTaste(Dish dish, List<int> taste)
        {
            dish.Score = dish.Score * 20;
            
            if(Math.Abs(taste[2] - dish.HotTaste*2) > 4 )
            {
                dish.Score *= 0.9;
                if (Math.Abs(taste[2] - dish.HotTaste * 2) > 6)
                {
                    dish.Score *= 0.9;
                    if (Math.Abs(taste[2] - dish.HotTaste * 2) > 8)
                    {
                        dish.Score *= 0.9;
                    }
                }
            }

            if (Math.Abs(taste[3] - dish.SweetTaste * 2) > 4)
            {
                dish.Score *= 0.9;
                if (Math.Abs(taste[3] - dish.SweetTaste * 2) > 6)
                {
                    dish.Score *= 0.9;
                    if (Math.Abs(taste[3] - dish.SweetTaste * 2) > 8)
                    {
                        dish.Score *= 0.9;
                    }
                }
            }

            if (Math.Abs(taste[4] - dish.BitterTaste * 2) > 4)
            {
                dish.Score *= 0.9;
                if (Math.Abs(taste[4] - dish.BitterTaste * 2) > 6)
                {
                    dish.Score *= 0.9;
                    if (Math.Abs(taste[4] - dish.BitterTaste * 2) > 8)
                    {
                        dish.Score *= 0.9;
                    }
                }
            }

            return dish.Score;
        }

        private static void DivideByType(List<Dish> inputDishes, out List<Dish> coldDishes, out List<Dish>meatDishes, out List<Dish>vegetDishes, out List<Dish>soupDishes)
        {
            coldDishes = new List<Dish>();
            meatDishes = new List<Dish>();
            vegetDishes = new List<Dish>();
            soupDishes = new List<Dish>();

            Regex meatPtn = new Regex(@"牛|羊|猪|肉|鸭|鹅|虾|鸡");

            foreach(var dish in inputDishes)
            {
                if(string.Equals(dish.Type,"热"))
                {
                    if(meatPtn.IsMatch(dish.Materials[0]))
                    {
                        meatDishes.Add(dish);
                    }
                    else
                    {
                        vegetDishes.Add(dish);
                    }
                }

                if (string.Equals(dish.Name, "凉"))
                {
                    coldDishes.Add(dish);
                }

                if (string.Equals(dish.Name, "汤"))
                {
                    soupDishes.Add(dish);
                }
            }
        }



    }
}