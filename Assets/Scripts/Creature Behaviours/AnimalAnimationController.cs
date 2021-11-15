using UnityEngine;

public class AnimalAnimationController : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;
    private float turnSmoothVelocity;
    public float turnSpeed;

    [SerializeField, Tooltip("The amount of space between the user and animal.")]
    private float goToPlayerDistance;

    private Animator animalAnimator;

    [Header("AI Patrol State")] public Transform[] patrolSpots;
    private int randomSpot; //Choose a random position for the deer to go to
    private float waitTime;
    public float startWaitTime;

    [Header("Turtle RB")] private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;

        animalAnimator = GetComponent<Animator>();

        //Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Target").transform;

        //Set up the random points for the deer to walk to
        randomSpot = Random.Range(0, patrolSpots.Length);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Smooth rotation between walk points
        float targetAngle = Mathf.Atan2(patrolSpots[randomSpot].transform.position.x - transform.position.x,
            patrolSpots[randomSpot].transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Interact with player
        bool interactPlayer = Vector3.Distance(transform.position, target.position) < goToPlayerDistance;

        //If the distance between target and animal is in range, then walk toward the player
        if (interactPlayer)
        {
            //Set IsWalking bool to true
            animalAnimator.SetBool("IsWalking", true);

            //Follow user
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            //If the deer is already at the target position then go to Idle
            if (Vector3.Distance(transform.position, target.position) == 0)
            {
                //Set IsWalking bool to false to make the deer idle
                animalAnimator.SetBool("IsWalking", false);
            }
        }

        //If not in range of player, patrol
        if (!interactPlayer)
        {
            Patrol();
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
        transform.position = Vector3.MoveTowards(transform.position, patrolSpots[randomSpot].position,
            moveSpeed * Time.deltaTime);

        //If deer has moved to a random spot, eating animation/wait a few seconds
        if (Vector3.Distance(transform.position, patrolSpots[randomSpot].position) < 0.2f)
        {
            //If wait time has passed
            if (waitTime <= 0)
            {
                //Walk animation between points
                animalAnimator.SetBool("IsWalking", true);

                //Move to another point
                randomSpot = Random.Range(0, patrolSpots.Length);

                waitTime = startWaitTime;
            }
            else
            {
                //Make deer idle
                animalAnimator.SetBool("IsWalking", false);

                //Decrease countdown
                waitTime -= Time.deltaTime;
            }
        }
    }
}
