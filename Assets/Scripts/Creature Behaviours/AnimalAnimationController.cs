using UnityEngine;

public class AnimalAnimationController : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;
    
    [SerializeField,Tooltip("The amount of space between the user and animal.")] 
    private float goToPlayerDistance;

    private Rigidbody rb;
    private Animator deerAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        deerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If the distance between target and animal is in range, then walk toward the player
        if (Vector3.Distance(transform.position, target.position) < goToPlayerDistance)
        {
            //Set IsWalking bool to true
            deerAnimator.SetBool("IsWalking", true);
            
            //Follow user
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        
        //If distance between AI & target is less than 0 (if the deer is already at the target) then go to Idle
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
}
