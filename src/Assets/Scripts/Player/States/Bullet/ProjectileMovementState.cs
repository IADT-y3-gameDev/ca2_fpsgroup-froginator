using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementState : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
