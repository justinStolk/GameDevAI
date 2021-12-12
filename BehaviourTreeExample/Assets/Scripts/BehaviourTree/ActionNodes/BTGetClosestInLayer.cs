using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGetClosestInLayer : BTNode
{
    private Blackboard blackboard;
    private string savedAs;
    private LayerMask layerMask;
    private Vector3 from;
    private float checkDistance;
    public BTGetClosestInLayer(Blackboard _blackboard, Vector3 _from, float _checkDistance, string _savedAs, LayerMask _layerMask) 
    {
        blackboard = _blackboard;
        savedAs = _savedAs;
        layerMask = _layerMask;
        from = _from;
        checkDistance = _checkDistance;
    }
    public override BTResult Run()
    {
        Collider[] results = Physics.OverlapSphere(from, checkDistance, layerMask);
        if (results.Length == 0)
        {
            return BTResult.Failed;
        } 
        float dist = float.MaxValue;
        GameObject closest = null;
        foreach(Collider c in results)
        {
            if(Vector3.Distance(from, c.transform.position) < dist)
            {
                dist = Vector3.Distance(from, c.transform.position);
                closest = c.gameObject;
            }
        }
        blackboard.SetValue<GameObject>(savedAs, closest);
        return BTResult.Success;
    }
}
