using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossLaser : MonoBehaviour
{
    [Header("References")]
    public float damage;
    public float range;
   
    public float Cooldown = 0;

    [Header("Camera")]
    public Camera FPSCamera;


    [Header("Bullets")]
    public Transform laserPoint;
    public TrailRenderer laserTrail;


     void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.L)){

            Shoot();

        }

       

        

        
    }

    private void Shoot(){

        var laser = Instantiate(laserTrail, laserPoint.position, Quaternion.identity);
        laser.AddPosition(laserPoint.position);
        {
            laser.transform.position = transform.position + (FPSCamera.transform.forward * 200);
        }


        RaycastHit hit;
        if(Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range)){

            Debug.Log(hit.transform.name);

            BossTarget target = hit.transform.GetComponent<BossTarget>();
            if(target != null){

                target.takeDamage(damage);

            }

        }

    }

    



}
