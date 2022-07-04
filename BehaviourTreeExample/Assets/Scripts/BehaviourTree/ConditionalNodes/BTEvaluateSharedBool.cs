using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEvaluateSharedBool : BTNode
{
    string evaluator;
    public BTEvaluateSharedBool(string boolToEvaluate)
    {
        evaluator = boolToEvaluate;
    }
    public override BTResult Run()
    {
        return SharedBlackboard.GetValue<bool>(evaluator) ? BTResult.Success : BTResult.Failed;
    }
}
