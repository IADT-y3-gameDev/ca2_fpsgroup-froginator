using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    
        public float maxTime = 1;
        private float timer;
 

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > maxTime) {
            Destroy(gameObject);
        }
    }
}
