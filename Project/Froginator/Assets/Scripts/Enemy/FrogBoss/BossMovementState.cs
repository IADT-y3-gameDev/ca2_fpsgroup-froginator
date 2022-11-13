using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovementState : MonoBehaviour
{
    [Header("Movement")]
    private Transform goal;
    private NavMeshAgent agent;
    public float JumpCD;
    public float maxJumpCD = 10;

    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
     bool readyToJump;



    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;



    [Header("Damage")]
    public float knockbackTime = 1;
    public float kick = 1.8f;
     private bool hit;
    private ContactPoint contact;
    private float timer;



    [Header("Ground Check")]
    public float bossHeight;
    public LayerMask GroundDetection;
     bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Awake(){

        goal = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("moving towards" ,goal);
        agent = GetComponent<NavMeshAgent>();

        timer = knockbackTime;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        

        if(JumpCD == 0){
        readyToJump = true;
        }
    }

    private void Update()
    {

        if(JumpCD > 0){
            JumpCD -= Time.deltaTime;
            readyToJump = false;
        }
        else{

            readyToJump = true;

        }

        transform.LookAt(player);

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, bossHeight * 0.5f + 0.3f, GroundDetection);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


        if (hit) {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * kick, contact.point, ForceMode.Impulse);
            hit = false;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            if (knockbackTime < timer)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                agent.SetDestination(goal.position);
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(readyToJump && grounded)
        {
            readyToJump = false;

            StartCoroutine(JumpAttack());

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        agent.SetDirection(player.Position);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private IEnumerator JumpAttack(){

        yield return new WaitForSeconds(0.25f);
        Jump();
        

    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        JumpCD = maxJumpCD;

        if(grounded) {

            Debug.Log("GroundPound");

        }
        
    }

    private void OnCollisionEnter(Collision other){

        if (other.transform.CompareTag("bullet"))
        {
            contact = other.contacts[0];
            hit = true;
        }

    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}