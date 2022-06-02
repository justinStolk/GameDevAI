using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTThrowObject : BTNode
{
    private float coolDown;
    private float timer = 0;
    private float throwingForce;
    private Rigidbody objectToThrow;
    private string origin;
    private string target;
    private Blackboard bb;

    public BTThrowObject(Blackboard blackboard, Rigidbody throwable, string originName, string targetName, float _throwingForce, float _coolDown)
    {
        coolDown = _coolDown;
        objectToThrow = throwable;
        origin = originName;
        target = targetName;
        throwingForce = _throwingForce;
        bb = blackboard;
    }

    public override BTResult Run()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = coolDown;
            Transform orig = bb.GetValue<Transform>(origin);
            Transform targ = bb.GetValue<Transform>(target);
            Rigidbody thrown = Object.Instantiate(objectToThrow, orig.position, Quaternion.identity);
            Vector3 dir = targ.position - orig.position;
            thrown.AddForce(dir.normalized * throwingForce);
            return BTResult.Success;
        }
        return BTResult.Running;
    }
}
