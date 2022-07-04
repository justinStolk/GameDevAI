using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBool : BTNode
{
    private Blackboard bb;
    private string booleanName;
    private bool bValue;

    public BTSetBool(Blackboard blackboard, string boolName, bool boolValue)
    {
        bb = blackboard;
        booleanName = boolName;
        bValue = boolValue;
    }
    public override BTResult Run()
    {
        bb.SetValue<bool>(booleanName, bValue);
        return BTResult.Success;
    }

}
