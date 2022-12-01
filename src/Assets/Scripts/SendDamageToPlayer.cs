    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageToPlayer : MonoBehaviour
{
    public int damage;

    void OnCollisionStay(Collision other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            
            other.transform.SendMessage("ApplyDamage", damage);
        }
    }
}
