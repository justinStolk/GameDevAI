using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInView : BTNode
{
    private Transform viewPoint;
    private Transform target;
    private float viewAngle;
    private float viewRadius;
    private string tag;

    public BTInView(Transform _viewPoint, Transform _target, float _viewAngle, float _viewRadius, string targetTag)
    {
        target = _target;
        viewPoint = _viewPoint;
        viewAngle = _viewAngle;
        viewRadius = _viewRadius;
        tag = targetTag;
    }

    public override BTResult Run()
    {
        Vector3 directionToTarget = (target.position - viewPoint.position).normalized;
        if (Vector3.Angle(viewPoint.forward, directionToTarget) < viewAngle / 2)
        {
            if (Physics.Raycast(viewPoint.position, directionToTarget, out RaycastHit hit, viewRadius))
            {
                return hit.transform.CompareTag(tag) ? BTResult.Success : BTResult.Failed;
            }
        }
        return BTResult.Failed;
    }
}
