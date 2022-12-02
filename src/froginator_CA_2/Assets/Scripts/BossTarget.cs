using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTarget : MonoBehaviour
{
    [Header("References")]
    public float bossHP = 100;

    public void takeDamage(float damage){

        bossHP -= damage;
        if(bossHP <= 0f){

            Die();

        }

    }

    void Die(){

        Destroy(gameObject);

    }

}
