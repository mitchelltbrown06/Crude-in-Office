using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpMovement : MonoBehaviour
{
    public float walkSpeed;
    public Rigidbody2D rb;
    public GridScript grid;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W) && transform.position.y < .96)
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y + walkSpeed * Time.deltaTime, transform.position.z);
        }
        if(Input.GetKey(KeyCode.A) && transform.position.x > -1.65)
        {
            transform.position =  new Vector3(transform.position.x -walkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if(Input.GetKey(KeyCode.S) && transform.position.y > -1)
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y -walkSpeed * Time.deltaTime, transform.position.z);
        }
        if(Input.GetKey(KeyCode.D) && transform.position.x < 1.65)
        {
            transform.position =  new Vector3(transform.position.x + walkSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            //rb.velocity = new Vector2(0,0);
        }
    }
}
