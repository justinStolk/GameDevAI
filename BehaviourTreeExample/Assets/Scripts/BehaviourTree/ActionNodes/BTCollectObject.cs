using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCollectObject : BTNode
{
    private string objectName;
    private GameObject gameObject;
    private Blackboard blackboard;

    public BTCollectObject(Blackboard _blackboard, GameObject objectToCollect,string _objectName)
    {
        blackboard = _blackboard;
        gameObject = objectToCollect;
        objectName = _objectName;
    }
    public override BTResult Run()
    {
        blackboard.SetValue<GameObject>(objectName, gameObject);
        return BTResult.Success;
    }
}
