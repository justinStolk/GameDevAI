using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFindTaggedObjects : BTNode
{
    private Blackboard blackboard;
    private string tag;
    private string storedAs;
    private float range;
    public BTFindTaggedObjects(Blackboard _blackboard, string _tag, string _storedAs, float _range )
    {
        blackboard = _blackboard;
        tag = _tag;
        storedAs = _storedAs;
        range = _range;
    }
    public override BTResult Run()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        blackboard.SetValue<GameObject[]>(storedAs, targets);
        return BTResult.Success;
    }
}
