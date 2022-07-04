using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallel : BTNode
{
    public enum ParallelPolicy { ON_CHILD_FAIL, ON_ALL_CHILDREN_FAIL, ON_CHILD_SUCCESS, ON_ALL_CHILDREN_SUCCESS }
    private ParallelPolicy policy;
    private BTNode[] children;
    public BTParallel(ParallelPolicy _policy, params BTNode[] _children)
    {
        children = _children;
        policy = _policy;
    }
    public override BTResult Run()
    {
        foreach (BTNode child in children)
        {
            if (!child.initialised)
            {
                child.OnEnter();
            }
        }
        switch (policy)
        {
            case ParallelPolicy.ON_CHILD_FAIL:
                foreach(BTNode n in children)
                {
                    BTResult childResult = n.Run();
                    if(childResult == BTResult.Failed)
                    {
                        return BTResult.Success;
                    }
                }
                break;
            case ParallelPolicy.ON_ALL_CHILDREN_FAIL:
                foreach (BTNode n in children)
                {
                    BTResult childResult = n.Run();
                    if (childResult == BTResult.Success)
                    {
                        return BTResult.Failed;
                    }
                    if (childResult == BTResult.Running)
                    {
                        return BTResult.Running;
                    }
                }
                return BTResult.Success;
            case ParallelPolicy.ON_CHILD_SUCCESS:
                foreach (BTNode n in children)
                {
                    BTResult childResult = n.Run();
                    if (childResult == BTResult.Success)
                    {
                        return BTResult.Success;
                    }
                }
                break;
            case ParallelPolicy.ON_ALL_CHILDREN_SUCCESS:
                foreach (BTNode n in children)
                {
                    BTResult childResult = n.Run();
                    if (childResult == BTResult.Failed)
                    {
                        return BTResult.Failed;
                    }
                    if (childResult == BTResult.Running)
                    {
                        return BTResult.Running;
                    }
                }
                return BTResult.Success;
        }
        return BTResult.Running;

    }

}
