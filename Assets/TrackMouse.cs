using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMouse : MonoBehaviour
{
    private Vector3 mousePosition;
    public GameObject objectCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, Camera.main.ScreenToWorldPoint(mousePosition).y, 0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        objectCollision = collision.gameObject;
    }
}
