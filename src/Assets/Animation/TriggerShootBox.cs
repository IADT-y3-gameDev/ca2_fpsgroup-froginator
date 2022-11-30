using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShootBox : MonoBehaviour
{
    [SerializeField] private Animator myShootBox = null;
    [SerializeField] private bool showTrigger = false;
    [SerializeField] private bool hideTrigger = false;

    private void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){

            if(showTrigger){
                myShootBox.Play("ShowBox",0,0.0f);
                gameObject.SetActive(false);
            }
            else if(hideTrigger){
                myShootBox.Play("HideBox",0,0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
