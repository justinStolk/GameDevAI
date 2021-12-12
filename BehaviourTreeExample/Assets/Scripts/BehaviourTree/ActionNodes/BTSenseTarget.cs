using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSenseTarget : BTNode
{
    private Blackboard blackboard;
    private Transform viewPoint;
    private FieldOfView fov;
    private List<Transform> visibleTargets =  new List<Transform>();
    public BTSenseTarget(Blackboard _blackboard, Transform _viewPoint, FieldOfView _fieldOfView)
    {
        blackboard = _blackboard;
        viewPoint = _viewPoint;
        fov = _fieldOfView;
    }

    public override void OnEnter()
    {
        Debug.Log("On enter triggered");
        base.OnEnter();
    }
    public override BTResult Run()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(viewPoint.position, fov.viewRadius, fov.targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - viewPoint.position).normalized;
            if (Vector3.Angle(viewPoint.forward, directionToTarget) < fov.viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(viewPoint.position, target.position);
                if (!Physics.Raycast(viewPoint.position, directionToTarget, distanceToTarget, fov.obstacleMask))
                {
                    if(Physics.Raycast(viewPoint.position, directionToTarget, distanceToTarget, fov.targetMask))
                    {
                        visibleTargets.Add(target);
                        float distance = int.MaxValue;
                        Transform closestTarget = null;
                        foreach(Transform t in visibleTargets)
                        {
                            if(Vector3.Distance(t.position, viewPoint.position) < distance)
                            {
                                closestTarget = t;
                                distance = Vector3.Distance(t.position, viewPoint.position);
                            }
                        }
                        blackboard.SetValue<Transform>("target", closestTarget);

                    }
                }
            }
        }
        if (visibleTargets.Count > 0)
            return BTResult.Success;

        return BTResult.Failed;
    }
}
