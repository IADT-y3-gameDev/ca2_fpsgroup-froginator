using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public float damage;
    public float range;
    public float fireRate;

    public float reloadTimer;
    public float maxReloadTimer;

    public float nextTimeToFire = 0f;


    public bool isReloaded;

    [Header("Ammo")]
    public float ammo;
    public float maxAmmoPer;
    public float maxAmmo;


    [Header("Camera")]
    public Camera FPSCamera;

    [Header("Bullets")]
    public Transform bulletPoint;
    public TrailRenderer bulletTrail;


     void Start()
    {
        
        isReloaded = true;
        reloadTimer = maxReloadTimer;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo != 0 && isReloaded ){

            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

        }

        if(Input.GetKeyDown(KeyCode.R) || ammo == 0){
            
            if(reloadTimer > 0){

            isReloaded = false;
            reloadTimer -= Time.deltaTime;

            }
            else{

                isReloaded = true;

            }

            StartCoroutine(Reloading());
            Invoke(nameof(Reloaded), reloadTimer);
            
            if (ammo > 0 && ammo < maxAmmoPer)
            {
                float tempAmmo;
                tempAmmo = maxAmmoPer - ammo;
                maxAmmo -= tempAmmo;
            }

            else
            {
                maxAmmo -= maxAmmoPer;
                ammo = maxAmmoPer;
            }
            

        }

        

        
    }

    private void Shoot(){

        ammo--;
        var bullet = Instantiate(bulletTrail, bulletPoint.position, Quaternion.identity);
        bullet.AddPosition(bulletPoint.position);
        {
            bullet.transform.position = transform.position + (FPSCamera.transform.forward * 200);
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

    private IEnumerator Reloading(){

        yield return new WaitForSeconds(3f);
        
        
    }

    
    private void Reloaded()
    {
        isReloaded = true;
        reloadTimer = maxReloadTimer;

        
        
    }



}
