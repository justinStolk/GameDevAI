using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTimerEvaluator : BTNode
{
    private Blackboard bb;
    private string timeName;
    private float timeTarget;
    public BTTimerEvaluator(Blackboard blackboard, string timerName, float targetTime)
    {
        bb = blackboard;
        timeName = timerName;
        timeTarget = targetTime;
    }
    public override BTResult Run()
    {
        if(bb.GetValue<float>(timeName) >= timeTarget)
        {
            bb.SetValue<float>(timeName, 0);
            return BTResult.Success;
        }
        return BTResult.Failed;
    }

}
