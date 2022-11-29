using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Door : Triggerable
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector3 moveOffset;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _targetPosition;
    private Coroutine _update;
    private Rigidbody _rigidbody;
    
    void Awake ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        // Transform the offset so that it works even when the door is rotated
        Vector3 offsetLocal = transform.TransformVector(moveOffset);

        _startPosition = transform.position;
        _endPosition = _startPosition + offsetLocal;
    }

    public override void Trigger(TriggerAction action)
    {
         // Support the door opening and closing
        if (action == TriggerAction.Toggle)
        {
            //ternary operator same as:
            //if (_targetPosition == _endPosition) {_targetPosition = _startPosition} else {_targetPosition = _endPosition};
            _targetPosition = (_targetPosition == _endPosition) ? _startPosition : _endPosition; 
        }
        else
        {
            _targetPosition = (action == TriggerAction.Deactivate) ? _startPosition : _endPosition;
        }

            if (_update != null)
        {
            StopCoroutine(_update);
            _update = null;
        }
        _update = StartCoroutine(MoveToTarget());
    }
    
    // The door only needs to update when opening or closing
    IEnumerator MoveToTarget ()
    {
        while (true)
        {   //calculate distance to the target position
            Vector3 offset = _targetPosition - transform.position;
            float distance = offset.magnitude;
            //calculate distance we can move this frame. 
            float moveDistance = moveSpeed * Time.deltaTime;

            // Keep moving towards target until we are close enough
            if (moveDistance < distance)
            {
                Vector3 move = offset.normalized * moveDistance;
                _rigidbody.MovePosition(transform.position + move);
                yield return null;
            }
            else
            {
                break;
            }
        }

        _rigidbody.MovePosition(_targetPosition);
        _update = null;
    }

   
}
