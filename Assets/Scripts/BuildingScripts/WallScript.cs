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
        Debug.DrawRay(
            start: new Vector2(transform.position.x - grid.tileSize /2, transform.position.y - grid.tileSize * .49f),
            dir: Vector2.left,
            color: Color.black,
            duration: 5f,
            depthTest: false);
        RaycastHit2D leftHit = Physics2D.Raycast(
            origin: new Vector2(transform.position.x - grid.tileSize /2, transform.position.y),
            direction: Vector2.left,
            distance: grid.tileSize);
        if(leftHit.collider.gameObject.transform.parent.gameObject.CompareTag("Wall"))
        {
            leftWall = leftHit.collider.gameObject.transform.parent.gameObject;
            leftWallScript = leftWall.GetComponent<WallScript>();
        }
/*
        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + grid.tileSize, transform.position.y), Vector2.right, grid.tileSize, buildingLayer);
        rightWallScript = rightWall.GetComponent<WallScript>();
*/
        if (leftWall != null && leftWall.CompareTag("Wall"))
        {
            leftWallScript.WallToYourRight();
            spriteRenderer.sprite = horizontalWall;

            transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        }
/*
        if (rightWall != null && rightWall.CompareTag("Wall"))
        {
            rightWallScript.WallToYourLeft();
            spriteRenderer.sprite = horizontalWall;

            transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        }
*/
    }
/*
    public void WallToYourLeft()
    {
        GetComponent<SpriteRenderer>().sprite = horizontalWall;
        transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
    }
*/
    public void WallToYourRight()
    {
        GetComponent<SpriteRenderer>().sprite = horizontalWall;
        transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
