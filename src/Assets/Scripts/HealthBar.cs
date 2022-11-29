using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    //PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        //Find the game object in the scene that has the tag player and then it set the player health variable to the component 
        //that is on the player object. 
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.value = playerHealth.health;
    }
}
