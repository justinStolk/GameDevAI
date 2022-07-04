using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTStopAgent : BTNode
{
    private NavMeshAgent _agent;

    public BTStopAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public override BTResult Run()
    {
        _agent.isStopped = true;
        return BTResult.Success;
    }
}
