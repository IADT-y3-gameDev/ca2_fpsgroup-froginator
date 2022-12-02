using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paused : MonoBehaviour
{
    
    public GameObject PauseMenu;
    public bool IsPaused;

    void Start(){

        PauseMenu.SetActive(false);
        IsPaused = false;

    }

    void Update(){

        if(Input.GetKeyDown("escape")){

            if(IsPaused == false){

            PauseMenu.SetActive(true);
            IsPaused = true;

            }

            else if(IsPaused == true){

            PauseMenu.SetActive(false);
            IsPaused = false;
            
            }


        }
        

    }


}
