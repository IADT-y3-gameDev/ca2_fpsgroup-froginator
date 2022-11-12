using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Switch : MonoBehaviour
{
    public TriggerAction action = TriggerAction.Trigger;
    public Triggerable[] targets;

    public Material activeMaterial;
    public Material inactiveMaterial;

    private MeshRenderer _renderer;
    private bool pressed = false;

    void Awake ()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.sharedMaterial = activeMaterial;
    }

    void OnCollisionEnter (Collision collision)
    {
        if (!pressed && collision.gameObject.CompareTag("Player"))
        {    

            _renderer.sharedMaterial = inactiveMaterial;
            pressed = true;
            TriggerTargets(action);
        }

        

        Debug.Log("Pressed: " + pressed);

        StartCoroutine ("DelayedReset");

    }

    public void TriggerTargets (TriggerAction action)

    {
        foreach (Triggerable t in targets)
        {
            if (t != null)
            {
                t.Trigger(action);
            }
        }
    }

    private IEnumerator DelayedReset() {
        yield return new WaitForSeconds(4);
        Debug.Log("Switch has been reset");
        _renderer.sharedMaterial = activeMaterial;
        pressed = false;
     
    } 

    //Gizmo that draws lines to each item that is triggerable from 
    //switch
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;

        // null check to avoid editor error when creating switch
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