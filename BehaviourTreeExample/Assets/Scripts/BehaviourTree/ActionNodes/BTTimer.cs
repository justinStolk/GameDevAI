using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTimer : BTNode 
{
    private Blackboard bb;
    private string blackboardName;
    private float timer;

    public BTTimer(Blackboard blackboard, string timerName)
    {
        bb = blackboard;
        blackboardName = timerName;
    }

    //This is a timer that you can and probably want to update every frame. Therefore, it is recommended to put it inside a Parallel Node. It always returns running, as it's never finished.
    //Use the TimerEvaluator to evaluate it (this will also reset the timer, hence why we need a name for the timer)

    public override BTResult Run()
    {
        timer = bb.GetValue<float>(blackboardName) + Time.deltaTime;
        bb.SetValue<float>(blackboardName, timer);
        return BTResult.Running;
    }
}
