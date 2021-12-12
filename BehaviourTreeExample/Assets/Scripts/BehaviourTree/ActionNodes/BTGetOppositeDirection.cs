using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGetOppositeDirection : BTNode
{
    private Blackboard blackboard;
    private Vector3 origin;
    private Vector3 aim;
    private string saveName;
    public BTGetOppositeDirection(Blackboard _blackboard, Vector3 _origin, Vector3 aimObject, string savedAs)
    {
        blackboard = _blackboard;
        origin = _origin;
        aim = aimObject;
        saveName = savedAs;
    }
    public override BTResult Run()
    {
        Vector3 dir = aim - origin;
        dir *= -1;
        dir = dir.normalized;
        blackboard.SetValue<Vector3>(saveName, dir);
        return BTResult.Success;
    }
}
