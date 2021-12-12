using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTNode
{
    BTNode childNode;

    public BTInverter(BTNode child)
    {
        childNode = child;
    }
    public override BTResult Run()
    {
        switch (childNode.Run())
        {
            case BTResult.Success: return BTResult.Failed;
            case BTResult.Running: return BTResult.Running;
            case BTResult.Failed: return BTResult.Success;
        }
        Debug.Log("Issue found inverting child node, returning failure");
        return BTResult.Failed;
    }
}
