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
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        buildingLayer = LayerMask.GetMask("Building");
    }

    // Update is called once per frame
    void Update()
    {
        //leftCheck = Physics2D.Raycast(new Vector2(transform.position.x - grid.tileSize, transform.position.y), Vector2.left, grid.tileSize / 4);
        //rightCheck = Physics2D.Raycast(new Vector2(transform.position.x + grid.tileSize, transform.position.y), Vector2.right, grid.tileSize / 4);
        leftCheck = Physics2D.OverlapBox(new Vector3(transform.position.x - grid.tileSize, transform.position.y, 0), new Vector2(grid.tileSize / 2, grid.tileSize / 2), 0f, buildingLayer);
        rightCheck = Physics2D.OverlapBox(new Vector3(transform.position.x + grid.tileSize, transform.position.y, 0), new Vector2(grid.tileSize / 2, grid.tileSize / 2), 0f, buildingLayer);

        if(leftCheck.GetComponent<Collider>().gameObject.CompareTag("Wall") || rightCheck.GetComponent<Collider>().gameObject.CompareTag("Wall"))
        {
            GetComponent<SpriteRenderer>().sprite = horizontalWall;
        }
        else //if(!leftCheck.collider.gameObject.CompareTag("Wall") && !rightCheck.collider.gameObject.CompareTag("Wall"))
        {
            GetComponent<SpriteRenderer>().sprite = verticalWall;
        }
    }
}
