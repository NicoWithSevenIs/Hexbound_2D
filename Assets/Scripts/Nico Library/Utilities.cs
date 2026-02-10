using UnityEngine;
using System.Collections.Generic;
using System;
public static class Utilities
{
    public static void RemoveAll<T>(List<T> list, Func<T, bool> predicate)
    {
        for(int i = list.Count - 1; i >= 0; i--)
        {
            if (predicate(list[i]))
                list.RemoveAt(i);       
        }
    }
}
