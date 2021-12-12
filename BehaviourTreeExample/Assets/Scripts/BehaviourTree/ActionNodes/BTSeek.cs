using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSeek : BTNode
{
    private NavMeshAgent agent;
    private Transform target;
    private float speed;
    private float keepDistance;

    private Vector3? lastRequest;
    public BTSeek(NavMeshAgent _agent, Transform _target, float _speed, float _keepDistance)
    {
        agent = _agent;
        agent.speed = speed;
        target = _target;
        keepDistance = _keepDistance;
    }
    public override void OnEnter()
    {
        if (!agent.pathPending && !agent.hasPath)
        {
            agent.SetDestination(target.position);
        }
        initialised = true;
    }
    public override void OnExit()
    {
        if (lastRequest != null && agent.gameObject.activeSelf)
        {
            agent.Warp(agent.transform.position);
            agent.ResetPath();
        }
        lastRequest = null;
        initialised = false;
    }
    public override BTResult Run()
    {
        if (target == null)
        {
            return BTResult.Failed;
        }
        if (agent.pathPending)
        {
            return BTResult.Running;
        }
        var position = target.position;
        if(lastRequest != position)
        {
            if (!agent.SetDestination(position))
            {
                return BTResult.Failed;
            }
        }
        lastRequest = position;
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + keepDistance)
        {
            return BTResult.Success;
        }
        return BTResult.Running;
    }

}
