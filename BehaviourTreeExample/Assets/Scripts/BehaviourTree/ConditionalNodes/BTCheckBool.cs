using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckBool : BTNode
{
    private Blackboard bb;
    private string booleanName;
    public BTCheckBool(Blackboard blackboard, string boolName)
    {
        bb = blackboard;
        booleanName = boolName;
    }
    public override BTResult Run()
    {
        return bb.GetValue<bool>(booleanName) ? BTResult.Success : BTResult.Failed;    
    }
}
