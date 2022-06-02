using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BTFindTaggedObjects : BTNode
{
    private Blackboard blackboard;
    private string tag;
    private string storedAs;
    public BTFindTaggedObjects(Blackboard _blackboard, string _tag, string _storedAs)
    {
        blackboard = _blackboard;
        tag = _tag;
        storedAs = _storedAs;
    }
    public override BTResult Run()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        List<Transform> l = new();
        foreach(GameObject t in targets)
        {
            l.Add(t.transform);
        }
        Debug.Log(targets.Length);
        blackboard.SetValue(storedAs, l);
        return BTResult.Success;
    }
}
