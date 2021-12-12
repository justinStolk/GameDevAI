using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    public Dictionary<string, object> Values = new Dictionary<string, object>();

    public T GetValue<T>(string name)
    {
        return Values.ContainsKey(name) ? (T)Values[name] : default(T);
    }

    public void SetValue<T>(string name, T item)
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

    internal void SetValue<T>(string v, object player)
    {
        throw new NotImplementedException();
    }
}
