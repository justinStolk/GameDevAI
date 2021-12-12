using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDebug : BTNode
{
    private string message;
    public BTDebug(string _message)
    {
        message = _message;
    }
    public override BTResult Run()
    {
        Debug.Log(message);
        return BTResult.Success;
    }
}
