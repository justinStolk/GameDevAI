using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRaycast : BTNode
{
    private Vector3 orig, dir;
    private float range;
    private string tag;
    public BTRaycast(Vector3 origin, Vector3 direction, float maxRange, string targetTag)
    {
        orig = origin;
        dir = direction;
        range = maxRange;
        tag = targetTag;
    }


    public override BTResult Run()
    {
        if(Physics.Raycast(orig, dir, out RaycastHit hit, range))
        {
            return hit.transform.CompareTag(tag) ? BTResult.Success : BTResult.Failed;
        }
        return BTResult.Failed;
    }
}
