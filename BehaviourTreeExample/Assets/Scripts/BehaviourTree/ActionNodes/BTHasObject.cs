using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTHasObject : BTNode
{
    private string objectName;
    private Blackboard blackboard;
    public BTHasObject(Blackboard _blackboard, string _objectName)
    {
        blackboard = _blackboard;
    }

    public override BTResult Run()
    {
        return blackboard.GetValue<GameObject>(objectName) ? BTResult.Success : BTResult.Failed;
    }
}
