using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTNode
{
    private float waitTime;
    private float timer = 0;
    public BTWait(float _waitTime)
    {
        waitTime = _waitTime;
    }
    public override BTResult Run()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            timer = 0;
            return BTResult.Success;
        }
        return BTResult.Running;
    }
}
