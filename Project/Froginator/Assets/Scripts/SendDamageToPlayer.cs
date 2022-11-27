using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageToPlayer : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            
            other.transform.SendMessage("ApplyDamage", 5);
        }
    }
}
