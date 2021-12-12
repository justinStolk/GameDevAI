using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToPosition : BTNode
{
    public Vector3 target;
    private NavMeshAgent agent;
    private Blackboard blackboard;
    private float keepDistance;
    private string storedPosName;
    public BTMoveToPosition(Blackboard _blackboard, NavMeshAgent _agent, float _keepDistance, string storedPositionName)
    {
        blackboard = _blackboard;
        agent = _agent;
        keepDistance = _keepDistance;
        storedPosName = storedPositionName; 
        target = blackboard.GetValue<Vector3>(storedPosName);
        agent.SetDestination(target);
    }
    public override void OnEnter()
    {
        agent.ResetPath();
        agent.isStopped = false;
        target = blackboard.GetValue<Vector3>(storedPosName);
        if (!agent.pathPending && !agent.hasPath)
        {
            agent.SetDestination(target);
        }
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override BTResult Run()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("Move Towards has failed due to invalid path");
            return BTResult.Failed;
        }
        if (Vector3.Distance(agent.transform.position, target) <= agent.stoppingDistance + keepDistance)
        {
            agent.isStopped = true;
            Debug.Log("Found target and at position!");
            return BTResult.Success;
        }
        return BTResult.Running;
    }
}
