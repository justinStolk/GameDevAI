using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGetOppositeDirection : BTNode
{
    private Blackboard blackboard;
    private string targetName;
    private string saveName;
    private string loadName;

    public BTGetOppositeDirection(Blackboard _blackboard, string target, string originLoadedAs, string savedAs)
    {
        blackboard = _blackboard;
        targetName = target;
        saveName = savedAs;
        loadName = originLoadedAs;
    }
    public override BTResult Run()
    {
        Vector3 invertPoint = blackboard.GetValue<Vector3>(loadName);
        Debug.Log($"The obstacle is at {invertPoint}");
        Vector3 targetPoint = blackboard.GetValue<Transform>(targetName).position;
        Debug.Log($"When spotted, the guard is at {targetPoint}");
        Vector3 dir = targetPoint - invertPoint;
        Vector3 target = invertPoint - dir;
        target.y = 0;
        Debug.Log($"I want to move to {target}");
        //dir *= -1;
        //dir = dir.normalized;
        blackboard.SetValue(saveName, dir);
        return BTResult.Success;
    }
}
