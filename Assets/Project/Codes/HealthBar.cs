using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HealthBar : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 lookAtTarget = transform.position + Camera.main.transform.forward;
        transform.LookAt(lookAtTarget, Camera.main.transform.up);
    }
}
