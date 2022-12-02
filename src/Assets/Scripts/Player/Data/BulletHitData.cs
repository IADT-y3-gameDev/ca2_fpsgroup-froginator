using UnityEngine;
using System.Collections;

public class BulletHitData : MonoBehaviour
{

   public GameObject particle;

 //When we touch something the bulletmesh will be disabled.
 //the parent empty object continues until it is destroyed
 //set by ttl variable.
 void OnCollisionEnter(Collision other)
 {
      //find the contact point on the object we collided with
      ContactPoint contact = other.contacts[0];
      //set the exact position and roation we hit the collider at
      Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
      Vector3 pos = contact.point;
      //Spawn our particle using the above parameters
      Instantiate(particle, pos, rot);
      //disable bullet Mesh
      gameObject.SetActive(false);
 }


}
