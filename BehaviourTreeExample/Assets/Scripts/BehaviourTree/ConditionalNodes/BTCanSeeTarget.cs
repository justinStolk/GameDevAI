using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCanSeeTarget : BTNode
{
    private Transform viewPoint;
    private Transform target;
    private float viewAngle;
    private float viewRadius;
    private float awarenessRadius;
    private List<Transform> visibleTargets;
    public BTCanSeeTarget(Transform _viewPoint, Transform _target, float _viewAngle, float _viewRadius, float _awarenessRadius)
    {
        target = _target;
        viewPoint = _viewPoint;
        viewAngle = _viewAngle;
        viewRadius = _viewRadius;
        awarenessRadius = _awarenessRadius;
    }

    public override BTResult Run()
    {
        //visibleTargets.Clear();
        Vector3 directionToTarget = (target.position - viewPoint.position).normalized;
        if (Physics.Raycast(viewPoint.position, directionToTarget, out RaycastHit hit, viewRadius))
        {
            bool targetInView = hit.transform.CompareTag("Player");
            if (Vector3.Angle(viewPoint.forward, directionToTarget) < viewAngle / 2 && targetInView || Vector3.Distance(viewPoint.position, target.position) < awarenessRadius && targetInView)
            {
                return BTResult.Success;
            }
        }
        //if(Vector3.Angle(viewPoint.forward, directionToTarget) < viewAngle / 2 /*|| Vector3.Distance(viewPoint.position, target.position) <= awarenessRadius*/)
        //{
        //    if(Physics.Raycast(viewPoint.position, directionToTarget, out RaycastHit hit, viewRadius))
        //    {
        //        bool targetInView = hit.transform.CompareTag("Player");
        //        //SharedBlackboard.SetValue<bool>("SeePlayer", true);
        //        if (Vector3.Distance(viewPoint.position, target.position) < awarenessRadius|| targetInView)
        //        {
        //            return BTResult.Success;
        //        }
        //    }
        //}
        //SharedBlackboard.SetValue<bool>("SeePlayer", false);
        return BTResult.Failed;
        /*
        Collider[] targetsInViewRadius = Physics.OverlapSphere(viewPoint.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            if (Vector3.Angle(viewPoint.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(viewPoint.position, target.position);
                if (!Physics.Raycast(viewPoint.position, directionToTarget, distanceToTarget))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        return visibleTargets.Count > 0 ? BTResult.Success : BTResult.Failed;  
        */
    }

}
