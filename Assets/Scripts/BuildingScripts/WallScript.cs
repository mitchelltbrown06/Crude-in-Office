using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Sprite verticalWall;
    public Sprite horizontalWall;
    public GridScript grid;
    private Collider2D leftCheck;
    private Collider2D rightCheck;
    public LayerMask buildingLayer;

    public GameObject leftWall;
    public WallScript leftWallScript;
    public GameObject rightWall;
    public WallScript rightWallScript;

    public Collider2D currentCollider;

    private SpriteRenderer spriteRenderer;

    public GameObject[] walls;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;

        //GetComponent<SpriteRenderer>().sprite = verticalWall;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = verticalWall;

        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        buildingLayer = LayerMask.GetMask("Building");

        UpdateNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNeighbors()
    {   
        LeftRightCheck();

        if (leftWall != null)
        {
            leftWallScript.WallToYourRight();
            spriteRenderer.sprite = horizontalWall;

            transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (rightWall != null)
        {
            rightWallScript.WallToYourLeft();
            spriteRenderer.sprite = horizontalWall;

            transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    public void WallToYourLeft()
    {
        GetComponent<SpriteRenderer>().sprite = horizontalWall;
        transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
    }
    public void WallToYourRight()
    {
        GetComponent<SpriteRenderer>().sprite = horizontalWall;
        transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
    }
    public void LeftRightCheck()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach(GameObject wall in walls)
        {
            if(Mathf.Abs((transform.position.x - wall.transform.position.x) - grid.tileSize) < .1f && Mathf.Abs(transform.position.y - wall.transform.position.y) < .1f)
            {
                Debug.Log("left wall");
                leftWall = wall;
                leftWallScript = wall.GetComponent<WallScript>();
            }
            else if(Mathf.Abs((wall.transform.position.x - transform.position.x) - grid.tileSize) < .1f && Mathf.Abs(transform.position.y - wall.transform.position.y) < .1f)
            {
                Debug.Log("Right Wall");
                rightWall = wall;
                rightWallScript = wall.GetComponent<WallScript>();
            }
        }
    }
}