using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange;
    public bool playerInSight;

    void Awake()
    {
        player = GameObject.Find("Rabbit").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (playerInSight)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            Debug.DrawLine(transform.position, walkPoint);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 3f)
        {
            //Debug.Log("walkpoint false");
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //generate a random point to walk to
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //Debug.Log("new walkpoint set");

        //check if 
        /*if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            Debug.DrawLine(transform.position, walkPoint);
            Debug.Log("walkpoint true");
            walkPointSet = true;
        }*/
        if (!Physics.Raycast(transform.position, walkPoint, walkPointRange, whatIsPlayer))
        {
            Debug.DrawLine(transform.position, walkPoint);
            Debug.DrawRay(transform.position, walkPoint, Color.green);
            walkPointSet = true;
        }
    }

    private void Chase()
    {
        walkPointSet = false;
        agent.SetDestination(player.position);
    }
}
