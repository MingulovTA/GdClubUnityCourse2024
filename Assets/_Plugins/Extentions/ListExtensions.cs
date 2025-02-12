using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Arpa_common.General.Extentions
{
    public static class ListExtensions
    {
        private static readonly System.Random _random = new System.Random();
        
        public static List<TEnum> GetValues<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }
        
        public static bool TryParse2<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            return Enum.TryParse<TEnum>(value, false, out result);
        }
        
        public static T AddTo<T>(this T self, ICollection<T> coll)
        {
            coll.Add(self);
            return self;
        }

        public static bool IsOneOf<T>(this T self, params T[] elems)
        {
            return elems.Contains(self);
        }

        public static bool IsOneOf<T>(this T self, List<T> elems)
        {
            return elems.Contains(self);
        }
        
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }
        
        public static T GetRandomItem<T>(this List<T> list)
        {
            return list.IsNullOrEmpty()
                ? default(T)
                : list[Random.Range(0, list.Count)];
        }
        
        public static void Shuffle<T>(this List<T> list)
        {
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = _random.Next(n + 1);  
                var value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }
        }
    }
}
