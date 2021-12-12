using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTConditionalCheckDistanceTo : BTNode
{
    public enum CheckType { EQUAL_TO, GREATER_THAN, LESS_THAN, GREATER_OR_EQUAL, LESS_OR_EQUAL }
    private GameObject objectA;
    private GameObject objectB;
    private float distance;
    private CheckType checkType;
    public BTConditionalCheckDistanceTo(GameObject _objectA, GameObject _objectB, float _distance, CheckType _checkType)
    {
        objectA = _objectA;
        objectB = _objectB;
        distance = _distance;
        checkType = _checkType;
    }
    public override BTResult Run()
    {

        //Debug.Log(Vector3.Distance(objectA.transform.position, objectB.transform.position));
        switch (checkType)
        {
            case CheckType.EQUAL_TO:
                if (Vector3.Distance(objectA.transform.position, objectB.transform.position) == distance)
                    return BTResult.Success;
                break;
            case CheckType.GREATER_THAN:
                if (Vector3.Distance(objectA.transform.position, objectB.transform.position) > distance)
                    return BTResult.Success;
                break;
            case CheckType.LESS_THAN:
                if (Vector3.Distance(objectA.transform.position, objectB.transform.position) < distance)
                    return BTResult.Success;
                break;
            case CheckType.GREATER_OR_EQUAL:
                if (Vector3.Distance(objectA.transform.position, objectB.transform.position) >= distance)
                    return BTResult.Success;
                break;
            case CheckType.LESS_OR_EQUAL:
                if (Vector3.Distance(objectA.transform.position, objectB.transform.position) <= distance)
                    return BTResult.Success;
                break;
        }
        return BTResult.Failed;
    }

}
