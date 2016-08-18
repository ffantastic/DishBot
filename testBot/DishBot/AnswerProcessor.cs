using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using testBot.Bean;
using testBot.Data;

namespace testBot.DishBot
{
    public class AnswerProcessor
    {
        private delegate bool IAnswerProcessor(User user, string text);

        private static Dictionary<int, IAnswerProcessor> hashToProcessor;

        static AnswerProcessor()
        {
            hashToProcessor = new Dictionary<int, IAnswerProcessor>();
            hashToProcessor[0] = processor0;
            hashToProcessor[1] = processor1;
            hashToProcessor[2] = processor2;
            hashToProcessor[3] = processor3;
            hashToProcessor[4] = processor4;
            hashToProcessor[5] = processor5;
            hashToProcessor[6] = processor6;
            hashToProcessor[7] = processor7;
        }

        public static bool Process(int QuestionId, User user, string text)
        {
            IAnswerProcessor processor = hashToProcessor[QuestionId];
            return processor(user, text);
        }

        private static bool processor0(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                return false;
            }

            string[] patterns =
            {
                @"([\d]+)[^\d]*男[^\d]*([\d]+)[^\d]*女",
                @"([\d]+)[^\d]*女[^\d]*([\d]+)[^\d]*男",
                @"男[^\d]*([\d]+)[^\d]*女[^\d]*([\d]+)",
                @"女[^\d]*([\d]+)[^\d]*男[^\d]*([\d]+)",
                @"([\d]+)[^\d]*男[^\d]*女[^\d]*([\d]+)",
                @"([\d]+)[^\d]*女[^\d]*男[^\d]*([\d]+)",
                @"男[^\d]*([\d]+)[^\d]*([\d]+)[^\d]*女",
                @"女[^\d]*([\d]+)[^\d]*([\d]+)[^\d]*男",
                @"[各|都][^\d]*([\d]+)",
                @"([\d]+)[^\d]*男",
                @"男[^\d]*([\d]+)",
                @"([\d]+)[^\d]*女",
                @"女[^\d]*([\d]+)"
            };

            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int nMen = 0;
            int nWomen = 0;

            int index = -1;
            foreach (Regex r in regex)
            {
                ++index;
                try
                {
                    if(index == 0 || index == 2 || index == 4 || index == 6)
                    {
                        nMen = int.Parse(r.Match(text).Groups[1].Value);
                        nWomen = int.Parse(r.Match(text).Groups[2].Value);
                    }
                    
                    if(index == 1 || index == 3 || index == 5 || index == 7)
                    {
                        nMen = int.Parse(r.Match(text).Groups[2].Value);
                        nWomen = int.Parse(r.Match(text).Groups[1].Value);
                    }

                    if(index == 8)
                    {
                        nMen = int.Parse(r.Match(text).Groups[1].Value);
                        nWomen = nMen;
                    }

                    if(index == 9 || index == 10)
                    {
                        nMen = int.Parse(r.Match(text).Groups[1].Value);
                    }

                    if (index == 11 || index == 12)
                    {
                        nWomen = int.Parse(r.Match(text).Groups[1].Value);
                    }
                }
                catch
                {
                    continue;
                }

                break;
            }

            if(nMen != 0 || nWomen != 0)
            {
                user.GetWVector()[0] = 10000 * nMen + nWomen;
                return true;
            }

            return false;
        }

        private static bool processor1(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                user.GetWVector()[1] = int.MaxValue;
                return true;
            }

            string[] patterns =
            {
                @"([\d]+)"
            };
            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int money;

            foreach (Regex r in regex)
            {
                try
                {
                    money = int.Parse(r.Match(text).Groups[1].Value);
                }
                catch
                {
                    continue;
                }

                user.GetWVector()[1] = money;
                return true;
            }

            return false;
        }

        private static bool processor2(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                user.GetWVector()[2] = 3;
                return true;
            }

            string[] patterns =
            {
                @"([\d]+)"
            };
            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int value;

            foreach (Regex r in regex)
            {
                try
                {
                    value = int.Parse(r.Match(text).Groups[1].Value);
                }
                catch
                {
                    continue;
                }

                if(value >=0 && value <= 10)
                {
                    user.GetWVector()[2] = value;
                    return true;
                }
            }

            return false;
        }

        private static bool processor3(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                user.GetWVector()[3] = 2;
                return true;
            }

            string[] patterns =
            {
                @"([\d]+)"
            };
            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int value;

            foreach (Regex r in regex)
            {
                try
                {
                    value = int.Parse(r.Match(text).Groups[1].Value);
                }
                catch
                {
                    continue;
                }

                if (value >= 0 && value <= 10)
                {
                    user.GetWVector()[3] = value;
                    return true;
                }
            }

            return false;
        }

        private static bool processor4(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                user.GetWVector()[4] = 5;
                return true;
            }

            string[] patterns =
            {
                @"([\d]+)"
            };
            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int value;

            foreach (Regex r in regex)
            {
                try
                {
                    value = int.Parse(r.Match(text).Groups[1].Value);
                }
                catch
                {
                    continue;
                }

                if (value >= 0 && value <= 10)
                {
                    user.GetWVector()[4] = value;
                    return true;
                }
            }

            return false;
        }

        private static bool processor5(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                user.GetWVector()[5] = 0;
                return true;
            }

            string[] AnswerNo =
            {
                "无",
                "没"
            };

            foreach(var str in AnswerNo)
            {
                if (text.Contains(str))
                {
                    user.GetWVector()[5] = 0;
                    return true;
                }
            }

            IList<string> hatingMaterials = Food.GetHatingMaterial(text);

            if(hatingMaterials.Count > 0)
            {
                user.GetWVector()[5] = 1;
                foreach(var element in hatingMaterials)
                {
                    user.GetHatingMaterials().Add(element);
                }
                return true;
            }

            string[] AnswerYes =
            {
                "有",
                "嗯"
            };

            foreach (var str in AnswerYes)
            {
                if (text.Contains(str))
                {
                    user.GetWVector()[5] = 1;
                    return true;
                }
            }

            return false;
        }

        private static bool processor6(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                return true;
            }

            IList<string> hatingMaterials = Food.GetHatingMaterial(text);

            if (hatingMaterials.Count > 0)
            {
                foreach (var element in hatingMaterials)
                {
                    user.GetHatingMaterials().Add(element);
                }
                return true;
            }

            return true;
        }

        private static bool processor7(User user, string text)
        {
            if (text.Contains("跳过"))
            {
                int nMen = user.GetWVector()[0] / 10000;
                int nWomen = user.GetWVector()[0] % 10000;
                user.GetWVector()[6] = nMen;
                return true;
            }

            string[] patterns =
            {
                @"([\d]+)"
            };
            IList<Regex> regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                regex.Add(new Regex(pattern));
            }

            int value;

            foreach (Regex r in regex)
            {
                try
                {
                    value = int.Parse(r.Match(text).Groups[1].Value);
                }
                catch
                {
                    continue;
                }

                user.GetWVector()[6] = value;
                return true;
            }

            return false;
        }

    }

}