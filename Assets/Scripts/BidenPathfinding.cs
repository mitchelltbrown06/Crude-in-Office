using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidenPathfinding : MonoBehaviour

{
    public LayerMask buildingLayer;
    public LayerMask gridLayer;

    public JoeBidenScript movement;

    public OilRigScript OilRigScript;

    public GridScript grid;

    public GameObject[] Buildings;
    public GameObject closestBuilding;
    public float nearestBuildingDistance;
    public float buildingDistance;

    public GameObject[] Tiles;
    public GameObject closestTile;
    public float nearestGridDistance;
    public float gridDistance;

    public GameObject highlightedTile;

    public GameObject nextTile;

    public Vector3 nextDirection;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        buildingLayer = LayerMask.GetMask("Building");
        gridLayer = LayerMask.GetMask("Building");
        FindClosestTile();
        Debug.Log("position: " + Physics2D.OverlapBox(new Vector3(0, 0, 0), new Vector2(.1f, .1f), 0f, gridLayer).GetComponent<Collider2D>().gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBuilding();
        EasiestPath();
        FindTileInDirection();
    }
    public void EasiestPath()
    {
        if (Mathf.Abs(closestBuilding.transform.position.x - transform.position.x) > Mathf.Abs(closestBuilding.transform.position.y - transform.position.y)) 
        {
            if (closestBuilding.transform.position.x < transform.position.x)
            {
                nextDirection = Vector3.left;
            }
            else
            {
                nextDirection = Vector3.right;
            }
        }
        if (Mathf.Abs(closestBuilding.transform.position.x - transform.position.x) < Mathf.Abs(closestBuilding.transform.position.y - transform.position.y)) 
        {
            if (closestBuilding.transform.position.y < transform.position.y)
            {
                nextDirection = Vector3.down;
            }
            else
            {
                nextDirection = Vector3.up;
            }
        }
    }
    public void FindTileInDirection()
    {
        Debug.Log("Finding Tile in direction");
        CastBoxGrid((/*nextDirection.x * grid.tileSize + */transform.position.x), (/*nextDirection.y * grid.tileSize + */transform.position.y));
        nextTile = highlightedTile;
    }

    private void CastBoxBuilding(float xPosition, float yPosition)
    {
        Physics2D.OverlapBox(new Vector3(xPosition, yPosition, 0f), new Vector2(.1f, .1f), 0f, buildingLayer);
    }
    private void CastBoxGrid(float xPosition, float yPosition)
    {
        Debug.Log("cast box location: " + xPosition + yPosition);
        highlightedTile = Physics2D.OverlapBox(new Vector3(xPosition, yPosition, 0), new Vector2(.1f, .1f), 0f, gridLayer).GetComponent<Collider2D>().gameObject;
    }

    public void FindClosestBuilding()
    {
        nearestBuildingDistance = 10000000;
        Buildings = GameObject.FindGameObjectsWithTag("Building");

        if (Buildings.Length > 0)
        {
            for(int i = 0; i < Buildings.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                buildingDistance = Vector3.Distance(this.transform.position, Buildings[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(buildingDistance < nearestBuildingDistance)
                {
                    closestBuilding = Buildings[i];
                    nearestBuildingDistance = buildingDistance;
                }
            }
        }
        else 
        {
        closestBuilding = Player;
        }

        OilRigScript = closestBuilding.GetComponent<OilRigScript>();
    }
    void FindClosestTile()
    {
        nearestGridDistance = float.MaxValue;
        Tiles = GameObject.FindGameObjectsWithTag("Grid");

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                gridDistance = Vector3.Distance(this.transform.position, Tiles[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(gridDistance < nearestGridDistance)
                {
                    closestTile = Tiles[i];
                    nearestGridDistance = gridDistance;
                }
            }
        }
    }
    
}
