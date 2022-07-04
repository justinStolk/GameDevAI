using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSpawnAtTransform : BTNode
{
    private Blackboard bb;
    private GameObject spawn;
    private string tName;
    private bool parented;
    public BTSpawnAtTransform(Blackboard blackboard, string transformName, GameObject spawnedObject, bool parentUnderTransform)
    {
        bb = blackboard;
        tName = transformName;
        spawn = spawnedObject;
        parented = parentUnderTransform;
    }
    public override BTResult Run()
    {
        Transform spawnObject = bb.GetValue<Transform>(tName);
        if (parented)
        {
            Object.Instantiate(spawn, spawnObject);
        }
        else 
        {
            Object.Instantiate(spawn, spawnObject.position, Quaternion.identity);
        }
        return BTResult.Success;

    }


}
