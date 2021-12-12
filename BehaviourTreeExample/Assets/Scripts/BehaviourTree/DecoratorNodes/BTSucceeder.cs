using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSucceeder : BTNode
{
    BTNode childNode;

    public BTSucceeder(BTNode child)
    {
        childNode = child;
    }

    public override BTResult Run()
    {
        switch (childNode.Run())
        {
            case BTResult.Running: 
                return BTResult.Running;
        }
        return BTResult.Success;
    }

}
