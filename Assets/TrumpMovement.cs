using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpMovement : MonoBehaviour
{
    public float walkSpeed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y + walkSpeed * Time.deltaTime, transform.position.z);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position =  new Vector3(transform.position.x -walkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y -walkSpeed * Time.deltaTime, transform.position.z);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position =  new Vector3(transform.position.x + walkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            //rb.velocity = new Vector2(0,0);
        }
    }
}
