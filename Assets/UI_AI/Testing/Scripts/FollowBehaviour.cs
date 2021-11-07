using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;
    
    [SerializeField,Tooltip("The amount of space between the user and animal.")] 
    private float stopDistance;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If distance between AI & user is greater than stop distance = stop in front of user
        if (Vector3.Distance(transform.position, target.position) > stopDistance)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            FollowUser();
        }
    }

    public void FollowUser()
    {
        //Follow user
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Visual representation of the sight range.
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
