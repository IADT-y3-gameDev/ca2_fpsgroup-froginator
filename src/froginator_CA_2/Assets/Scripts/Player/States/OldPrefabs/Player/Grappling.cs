using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovementState pms;
    public GameObject GrapplingGun;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

     [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    [Header("Grappling")]
    public float maxGrappleDistance = 25f;
    public float grappleDelayTime = 0.5f;
    public float overshootYAxis = 2f;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd = 2.5f;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        pms = GetComponent<PlayerMovementState>();
        GrapplingGun.SetActive(false);
    }

    private void Update()
    {
        // input
        if (Input.GetKeyDown(grappleKey)){ 
            StartGrapple();
        }
        
        if(Input.GetMouseButton(1))
        {
            GrapplingGun.SetActive(true);
        }else
        {
            GrapplingGun.SetActive(false);
        }

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;

             
    }

    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, gunTip.position);
    }

    

    public void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExcecuteGrapple), grappleDelayTime);
        }

        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    public void ExcecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOfArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOfArc = overshootYAxis;

        if(pms.grounded){
        pms.JumpToPosition(grapplePoint, highestPointOfArc);
        }

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

    public void OnObjectTouch()
    {
        if (pms.activeGrapple) StopGrapple();
    }


    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }


}