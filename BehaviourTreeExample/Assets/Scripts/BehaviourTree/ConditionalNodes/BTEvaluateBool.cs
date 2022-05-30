using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEvaluateBool : BTNode
{
    string evaluator;
    public BTEvaluateBool(string boolToEvaluate)
    {
        evaluator = boolToEvaluate;
    }
    public override BTResult Run()
    {
        return SharedBlackboard.GetValue<bool>(evaluator) ? BTResult.Success : BTResult.Failed;
    }
}
