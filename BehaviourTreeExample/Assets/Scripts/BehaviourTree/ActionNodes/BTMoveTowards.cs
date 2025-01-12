using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveTowards : BTNode
{
    public Transform target;
    private NavMeshAgent agent;
    private Blackboard blackboard;
    private float keepDistance;
    private bool persistentTarget;
    private string targetsName;
    public BTMoveTowards(Blackboard _blackboard, NavMeshAgent _agent, string targetName, float _keepDistance, bool _persistentTarget)
    {
        blackboard = _blackboard;
        agent = _agent;
        keepDistance = _keepDistance;
        targetsName = targetName;
        //target = blackboard.GetValue<Transform>(targetName);
        persistentTarget = _persistentTarget;
        //agent.SetDestination(target.position);
    }
    public override void OnEnter()
    {
        agent.ResetPath();
        agent.isStopped = false;
        target = blackboard.GetValue<Transform>(targetsName);
        if (!agent.pathPending && !agent.hasPath)
        {
                agent.SetDestination(target.position);
        }
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override BTResult Run()
    {
        agent.isStopped = false;
        Transform storedTarget = blackboard.GetValue<Transform>(targetsName);
        if (persistentTarget && target.position != agent.destination)
        {
            agent.SetDestination(target.position);
            blackboard.SetValue<Transform>(targetsName, target);
        }
        if(agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("Move Towards has failed due to invalid path");
            return BTResult.Failed;
        }
        if (Vector3.Distance(agent.transform.position, target.position) <= agent.stoppingDistance + keepDistance|| !persistentTarget && Vector3.Distance(agent.transform.position, agent.destination) <= agent.stoppingDistance + keepDistance )
        {
            agent.isStopped = true;
            return BTResult.Success;
        }
        return BTResult.Running;
     
    }
}
