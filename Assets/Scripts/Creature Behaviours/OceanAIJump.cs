using UnityEngine;

public class OceanAIJump : MonoBehaviour
{
    private Animator fishAnim;
    [SerializeField] private bool jump;

    private void Start()
    {
        fishAnim = GetComponent<Animator>();
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }

        if (jump.Equals(true))
            fishAnim.SetBool("IsJumping", true);

        if (jump.Equals(false))
            fishAnim.SetBool("IsJumping", false);
    }
}
