using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Sprite verticalWall;
    public Sprite horizontalWall;
    public GridScript grid;
    public LayerMask buildingLayer;
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        buildingLayer = LayerMask.GetMask("Building");
    }

    // Update is called once per frame
    void Update()
    {
        //if(leftCheck.GetComponent<Collider>().gameObject.CompareTag("Wall") || rightCheck.GetComponent<Collider>().gameObject.CompareTag("Wall"))
      //  {
            //GetComponent<SpriteRenderer>().sprite = horizontalWall;
       // }
       // else
       // {
       //     GetComponent<SpriteRenderer>().sprite = verticalWall;
       // }
    }
}
