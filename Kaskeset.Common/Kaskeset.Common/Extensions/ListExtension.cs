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

        public static List<T> ConvertListType<T>(this List<string> list) 
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            List<T> convertedList = new List<T>();
            list.ForEach(item => convertedList.Add((T) converter.ConvertFromString(item)));
            return convertedList;
        }
    }
}
