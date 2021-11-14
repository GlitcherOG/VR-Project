using System.Collections;
using UnityEngine;

public class AnimalAnimationController : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;
    
    [SerializeField,Tooltip("The amount of space between the user and animal.")] 
    private float goToPlayerDistance;

    private Rigidbody rb;
    private Animator deerAnimator;

    [Header("Deer Patrol State")]
    public Transform[] patrolSpots;
    private int randomSpot; //Choose a random position for the deer to go to
    private float waitTime;
    public float startWaitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        
        deerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Target").transform;

        randomSpot = Random.Range(0, patrolSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        bool interactPlayer = Vector3.Distance(transform.position, target.position) < goToPlayerDistance;
        
        //If the distance between target and animal is in range, then walk toward the player
        if (interactPlayer)
        {
            //Set IsWalking bool to true
            deerAnimator.SetBool("IsWalking", true);
            
            //Follow user
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        
        //If not in range of player, patrol
        if(!interactPlayer)
        {
            Patrol();
        }
        
        //If the deer is already at the target position then go to Idle
        if(Vector3.Distance(transform.position, target.position) == 0)
        {
            //Set IsWalking bool to false to make the deer idle
            deerAnimator.SetBool("IsWalking", false);
        }
    }

    /// <summary>
    /// Visual representation of the sight range.
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, goToPlayerDistance);
    }

    public void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolSpots[randomSpot].position, moveSpeed * Time.deltaTime);
        
        //If deer has moved to a random spot, eating animation/wait a few seconds
        if (Vector3.Distance(transform.position, patrolSpots[randomSpot].position) < 0.2f)
        {
            StartCoroutine(EatAnimation());
            
            /*//If wait time has passed
            if (waitTime <= 0)
            {
                //Walk animation between points
                deerAnimator.SetBool("IsWalking", true);
                
                //Move to another point
                randomSpot = Random.Range(0, patrolSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                //Eat animation
                deerAnimator.SetBool("IsEating", true);
                
                //Make deer idle
                deerAnimator.SetBool("IsWalking", false);
                
                //Decrease countdown
                waitTime -= Time.deltaTime;
            }*/
        }
    }

    private IEnumerator EatAnimation()
    {
        //If wait time has passed
        if (waitTime <= 0)
        {
            //Walk animation between points
            deerAnimator.SetBool("IsWalking", true);
                
            //Move to a random point in the array
            randomSpot = Random.Range(0, patrolSpots.Length);
            waitTime = startWaitTime;
        }
        else
        {
            //Make deer idle
            deerAnimator.SetBool("IsWalking", false);
            
            //Eat animation
            deerAnimator.SetBool("IsEating", true);
            
            //Make deer eat until wait time 
            yield return new WaitForSeconds(waitTime);
            
            //Decrease countdown
            waitTime -= Time.deltaTime;
        }   
    }
}
