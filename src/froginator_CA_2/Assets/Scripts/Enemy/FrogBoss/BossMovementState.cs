using UnityEngine;

public class BossMovementState : MonoBehaviour
{   
    [Header("References")]
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    [Header("Patrol State")]
    public Vector3 walkPoint;
    public float walkPointRange;

    [Header("Attack State")]
    public float EmotionalDamage;
    public float PhysicalDamage;
    public float LaserDamage;
    public float LaserCoolDownTimer;
    bool alreadyAttacked;

    [Header("Bullets")]
    public Transform laserPoint;
    public TrailRenderer laserTrail;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Camera")]
    public Camera FPSCamera;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

   
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            agent.SetDestination(transform.position);
            ///Attack code here
            ShootLaser();
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), LaserCoolDownTimer);
        }
    }

    private void ShootLaser(){

        var laser = Instantiate(laserTrail, laserPoint.position, Quaternion.identity);
        laser.AddPosition(laserPoint.position);
        {
            laser.transform.position = transform.position + (FPSCamera.transform.forward * 200);
        }


        RaycastHit hit;
        if(Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, attackRange)){

            Debug.Log(hit.transform.name);

            PlayerMovementState target = hit.transform.GetComponent<PlayerMovementState>();
            if(target != null){

                target.ApplyLaserDamage(LaserDamage);

            }

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    
}