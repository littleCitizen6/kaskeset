using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kaskeset.Common.Extensions
{
    public static class ListExtension
    {
        public static string ToSeperateByVerticalString<T>(this List<T> list)
        {
            StringBuilder builder = new StringBuilder();
            list.ForEach(item => builder.Append($"{item}|"));
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
        public static List<string> ConvertListToStrings<T>(this List<T> list)
        {
            List<string> convertedList = new List<string>();
            list.ForEach(item => convertedList.Add(item.ToString()));
            return convertedList;
        }
        public static List<T> ConvertListToType<T>(this List<string> list) 
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            List<T> convertedList = new List<T>();
            list.ForEach(item => convertedList.Add((T) converter.ConvertFromString(item)));
            return convertedList;
        }

    }
}
