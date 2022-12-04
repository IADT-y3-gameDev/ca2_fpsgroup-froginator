using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
    {
        //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("Player"))
        {
           SceneManager.LoadScene("SwampScene");
        }
    }
}
