using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTNode
{
    private BTNode[] children;
    private int index = 0;
    public BTSequence(params BTNode[] _children)
    {
        children = _children;
    }
    public override BTResult Run()
    {
        for(; index < children.Length; index++)
        {
            if (!children[index].initialised)
            {
                children[index].OnEnter();
            }
            BTResult result = children[index].Run();
            switch (result)
            {
                case BTResult.Success: children[index].OnExit(); break;
                case BTResult.Running: return BTResult.Running;
                case BTResult.Failed: index = 0; children[index].OnExit(); return BTResult.Failed;
            }
        }
        index = 0;
        return BTResult.Success;
    }
}
