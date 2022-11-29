using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera FPSCamera;

    Ray ray;
    RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        
        FPSCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
        ray.origin = FPSCamera.transform.position;
        ray.direction = FPSCamera.transform.forward;
        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;

    }
}
