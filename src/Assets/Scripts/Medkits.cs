using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkits : MonoBehaviour
{
    
    public int healthAmount;
    public bool respawn;
    public float delaySpawn;


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            other.transform.SendMessage("ApplyHealth", healthAmount);
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
