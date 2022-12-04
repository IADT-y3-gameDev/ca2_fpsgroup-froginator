using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Frog;
    public int PosX;
    public int PosZ;
    public int EnemyCount;
    public int FrogCount;
    

    void Start(){

        StartCoroutine(EnemySpawn());

    }

    IEnumerator EnemySpawn(){

        while(FrogCount < EnemyCount){

            PosX = Random.Range(1, 30);
            PosZ = Random.Range(1, 30);
            Instantiate(Frog, new Vector3(transform.position.x + PosX, transform.position.y, transform.position.z + PosZ), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            FrogCount += 1;
            
        }

    }
}

