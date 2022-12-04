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


            PausedGame();

            }

            else if(IsPaused == true){

            ResumeGame();
            
            }


        }
        

    }

    public void PausedGame(){

        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;


    }

    public void ResumeGame(){

        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

    }


}
