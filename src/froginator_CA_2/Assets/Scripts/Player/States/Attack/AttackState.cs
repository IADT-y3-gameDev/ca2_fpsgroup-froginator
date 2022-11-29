using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackState : MonoBehaviour
{

    [Header("References")]
    public int weaponIndex = 0;
    public GameObject Pistol;
    public GameObject Sniper;
    public GameObject SMG;


    

    private void Start()
    {
       
        //Hide the hand when we start the game and have ammo.
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int prevWeaponIndex = weaponIndex;

       
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){

            if(weaponIndex >= transform.childCount - 1){
                weaponIndex = 0;
            }else{

                weaponIndex++;

            }

        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f){

            if(weaponIndex <= 0){
                
                weaponIndex = transform.childCount - 1;

            }else{

                weaponIndex--;

            }

        }

        if(prevWeaponIndex != weaponIndex){

            SelectWeapon();

        }
       
    }

    
    private void SelectWeapon(){

        int i = 0;
        foreach (Transform weapon in transform){

            if(i == weaponIndex){

                weapon.gameObject.SetActive(true);

            }else{

                weapon.gameObject.SetActive(false);

            }

            i++;

        }

    }



}