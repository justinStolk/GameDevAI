using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTNode
{
    private BTNode[] children;
    private int index = 0;

    private bool dynamic;
    public BTSelector(bool isDynamic, params BTNode[] _children)
    {
        dynamic = isDynamic;
        children = _children;
    }

    public override BTResult Run()
    {
        for (var i = dynamic ? 0 : index ; index < children.Length; index++)
        {
            if (!children[index].initialised)
            {
                children[index].OnEnter();
            }
            BTResult result = children[index].Run();
            switch (result)
            {
                case BTResult.Success: index = 0; return BTResult.Success;
                case BTResult.Running: index = i; return BTResult.Running;
                case BTResult.Failed: break;
            }
        }
        index = 0;
        return BTResult.Failed;
    }
}
