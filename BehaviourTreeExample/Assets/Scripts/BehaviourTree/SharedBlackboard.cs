using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedBlackboard
{
    public static Dictionary<string, object> Values = new Dictionary<string, object>();

    public static T GetValue<T>(string name)
    {
        return Values.ContainsKey(name) ? (T)Values[name] : default(T);
    }

    public static void SetValue<T>(string name, T item)
    {
        if (Values.ContainsKey(name))
        {
            Values[name] = item;
        }
        else
        {
            Values.Add(name, item);
        }
    }
}
