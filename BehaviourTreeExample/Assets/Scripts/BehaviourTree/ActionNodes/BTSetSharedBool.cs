using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetSharedBool : BTNode
{
    private bool boolValue;
    private string boolName;

    public BTSetSharedBool(string boolName, bool boolValue)
    {
        this.boolName = boolName;
        this.boolValue = boolValue;
    }

    public override BTResult Run()
    {
        SharedBlackboard.SetValue(boolName, boolValue);
        return BTResult.Success;
    }
}
