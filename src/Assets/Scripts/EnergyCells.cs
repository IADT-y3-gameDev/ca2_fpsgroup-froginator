using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCells : MonoBehaviour
{ 
    public Gun smg;
    public Gun sniper;
    public Gun pistol;

    public int ammoAmount;
    public bool respawn;
    public float delaySpawn;
    


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            smg.ApplyAmmo(ammoAmount);
            if (respawn)
            {
                Invoke("Respawn", delaySpawn);
            }
            sniper.ApplyAmmo(ammoAmount);
                if (respawn)
                {
                    Invoke("Respawn", delaySpawn);
                }
            pistol.ApplyAmmo(ammoAmount);
                if (respawn)
                {
                    Invoke("Respawn", delaySpawn);
                }
        }
    }

    void Respawn()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
