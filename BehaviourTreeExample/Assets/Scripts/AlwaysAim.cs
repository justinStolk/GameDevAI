using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAim : MonoBehaviour
{
    [SerializeField] private Transform aimObject;

    private void LateUpdate()
    {
        if (transform.rotation.eulerAngles.y != aimObject.rotation.eulerAngles.y)
        {
            transform.rotation = Quaternion.Euler(0, aimObject.rotation.eulerAngles.y, 0);
        }
    }
}
