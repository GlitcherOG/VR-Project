using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShooter : MonoBehaviour
{
    public GameObject bird;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject temp = Instantiate(bird, this.transform.position, this.transform.rotation);
        temp.GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
    }
}
