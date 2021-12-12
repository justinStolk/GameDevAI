using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTarget : BTNode
{
    private Blackboard blackboard;
    private Transform target;
    public BTSetTarget(Blackboard _blackboard, Transform newTarget)
    {
        blackboard = _blackboard;
        target = newTarget;
    }

    public override BTResult Run()
    {
        blackboard.SetValue<Transform>("target", target);
        return BTResult.Success;
    }

}
