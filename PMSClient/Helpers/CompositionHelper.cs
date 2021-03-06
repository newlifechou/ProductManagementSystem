﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSClient.Helpers
{
    public enum TargetType
    {
        CIGS,
        CuGaSe,
        InSe,
        InS,
        Mo,
        WTi,
        Unknown
    }

    public static class CompositionHelper
    {

        public static string GetCompositionAbbr(string s)
        {
            //成分标准化
            string std = s.Replace(" ", "")
                .Replace("(", "").Replace(")", "").Replace("atomic", "").Replace("%", "");
            //成分缩写
            string abbr = "";
            if (IsCIGS(std))
            {
                abbr = "CIGS";
            }
            else
            {
                var matches = System.Text.RegularExpressions.Regex.Matches(std, @"[a-zA-Z]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                foreach (var item in matches)
                {
                    abbr += item.ToString();
                }
            }
            return abbr;
        }
        private static bool IsCIGS(string source)
        {
            var s = source.ToLower();
            return s.Contains("cu") && s.Contains("in") && s.Contains("ga") && s.Contains("se");
        }
        /// <summary>
        /// 将靶材成分按照原子数降序重排
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public static string ConvertToAtmDescend(string composition)
        {
            List<CompositionModel> elements = new List<CompositionModel>();
            string pattern = @"([a-zA-Z]+)([0-9]+([.]{1}[0-9]+){0,1})";
            var matches = Regex.Matches(composition, pattern);
            foreach (Match item in matches)
            {
                var element = item.Groups[1].Value;
                var value = double.Parse(item.Groups[2].Value);
                elements.Add(new CompositionModel { Element = element, Value = value });
            }
            elements.Sort();

            StringBuilder sb = new StringBuilder();
            foreach (var item in elements)
            {
                sb.Append(item.Element);
                sb.Append(item.Value);
            }
            return sb.ToString();
        }
        public static string CheckAllAdditive(string composition)
        {
            string postfix = "";
            postfix = CheckSingleAdditive(composition, "NaF");
            if (!string.IsNullOrEmpty(postfix)) return postfix;
            postfix = CheckSingleAdditive(composition, "KF");
            if (!string.IsNullOrEmpty(postfix)) return postfix;
            postfix = CheckSingleAdditive(composition, "RbF");
            if (!string.IsNullOrEmpty(postfix)) return postfix;
            postfix = CheckSingleAdditive(composition, "Na2S");

            if (!string.IsNullOrEmpty(postfix))
                return postfix;
            else
                return string.Empty;
        }

        private static string CheckSingleAdditive(string composition, string additive)
        {
            if (composition.Contains(additive))
                return additive;
            else
                return string.Empty;
        }


        public static string RemoveNumbers(string compostion)
        {
            string pattern = @"(\-|\+)?\d+(\.\d+)?";
            return Regex.Replace(compostion, pattern, "");
        }



        public static string GetGa(string comp)
        {
            if (WhatItIs(comp) == TargetType.CIGS)
            {
                string pattern = @"Ga(\d+\.\d+|\d+)";
                var match = Regex.Match(comp, pattern);
                return match.Groups[0].Value;
            }
            return "无法判断";
        }
        public static TargetType WhatItIs(string comp)
        {
            if (comp.Contains("Mo"))
            {
                return TargetType.Mo;
            }

            if (comp.Contains("WTi"))
            {
                return TargetType.WTi;
            }

            if (comp.Contains("Cu"))
            {
                if (comp.Contains("In"))
                {
                    return TargetType.CIGS;
                }
                else if (comp.Contains("Ga"))
                {
                    return TargetType.CuGaSe;
                }
                else
                {
                    return TargetType.Unknown;
                }
            }
            else
            {
                if (comp.Contains("Se"))
                {
                    return TargetType.InSe;
                }
                else
                {
                    return TargetType.InS;
                }
            }
        }

        public static string GetRoughCompositon(string composition, string split = "-")
        {
            if (string.IsNullOrEmpty(composition))
                return composition;

            string pattern = @"[0-9]+(\.?[0-9]+)?";
            string result = Regex.Replace(composition, pattern, split, RegexOptions.IgnoreCase);
            if (result.EndsWith(split))
                result = result.Remove(result.Length - 1);
            return result;
        }



    }
}
