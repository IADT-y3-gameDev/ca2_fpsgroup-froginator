using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    // Targets are only active when the player is inside
    // the trigger and overrides the setting of action.
    public TriggerAction action = TriggerAction.Activate;
    public Triggerable[] targets;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
           TriggerTargets();

        }
    }
   
    public void TriggerTargets ()
    {
        foreach (Triggerable t in targets)
            {
                // Check in case a target is destroyed
                if (t != null)
                {
                    t.Trigger(action);
                }
            }
         
    }

    //Gizmos
    void OnDrawGizmos ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.25f);

        if (targets != null)
        {
            foreach (Triggerable t in targets)
            {
                if (t != null)
                {
                    Gizmos.DrawLine(transform.position, t.transform.position);
                }
            }
        }        
    }
}
