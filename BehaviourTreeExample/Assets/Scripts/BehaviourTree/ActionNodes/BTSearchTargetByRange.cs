using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSearchTargetByRange : BTNode
{
    private Transform seeker;
    private Transform target;
    private float searchRange;
    public BTSearchTargetByRange(Transform _seeker, Transform _target, float _searchRange)
    {
        seeker = _seeker;
        target = _target;
        searchRange = _searchRange;
    }
    public override BTResult Run()
    {
        if(Vector3.Distance(target.position, seeker.position) <= searchRange)
        {
            Debug.Log("Found target!");
            return BTResult.Success;
        }
        Debug.Log("Failed finding target!");
        return BTResult.Failed;
    }
}
