using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple triggerable to activate or deactive any objects it's attached to
// Useful for spawners etc.
public class TargetActivator : Triggerable
{
    public bool deactivateOnAwake = true;

    void Awake ()
    {
        if (deactivateOnAwake)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Trigger (TriggerAction action)
    {
   

        if (action == TriggerAction.Activate)
        {
            gameObject.SetActive(true);
        }
        else if (action == TriggerAction.Deactivate)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

    }
}