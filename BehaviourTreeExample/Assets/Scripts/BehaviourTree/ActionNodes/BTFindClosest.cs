using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BTFindClosest : BTNode
{
    private Transform seeker;
    private string savedName;
    private string targetName;
    private Blackboard bb;
    public BTFindClosest(Blackboard blackboard ,Transform _seeker, string targetsName, string savedAs)
    {
        bb = blackboard;
        seeker = _seeker;
        targetName = targetsName;
        savedName = savedAs;
    }
    public override BTResult Run()
    {
        List<Transform> targets = bb.GetValue<List<Transform>>(targetName);
        if (targets.Count == 0)
        {
            return BTResult.Failed;
        }
        Transform closestTarget = targets.OrderBy(t => Vector3.Distance(seeker.position, t.transform.position)).First();
        bb.SetValue(savedName, closestTarget);
        return BTResult.Success;
    }
}
