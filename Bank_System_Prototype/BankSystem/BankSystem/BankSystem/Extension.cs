using System;
using System.Collections.Generic;

namespace BankSystem
{
    public static class Extension
    {

        public static void AddToList<T>(this T item, List<T> col)
        {
            col.Add(item);
        }

        public static void RemoveFromList<T>(this T item, List<T> col)
        {
            col.Remove(item);
        }
    }
}