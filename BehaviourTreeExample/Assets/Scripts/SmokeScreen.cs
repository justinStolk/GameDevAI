using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreen : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            SharedBlackboard.SetValue("SeePlayer", false);
        }
    }
}
