using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridScript grid;

    //all the shit for finding the closest grid tile initially and making sure the object is on that tile.
    public float nearestDistance;
    public GameObject closestTile;
    public GameObject[] Tiles;
    float distance;
    
    //finding the next space to go to
    public GameObject target;
    public Vector2 nextDirection;

    //Checking for walls
    public GameObject[] Walls;

    //Create a checkpoint system
    public Vector3 lastPosition;
    public Vector3 checkpointPosition;
    public Vector2 checkpointDirection;

    public bool onGrid = false;

    public float debugCooldown = .2f;
    float debugTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindNextDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGrid)
        {
            FindClosestTile();
            transform.position = closestTile.transform.position;
            lastPosition = transform.position;
            onGrid = true;
        }
        if(debugTimer < debugCooldown)
        {
            debugTimer += Time.deltaTime;
        }
        if(target != null && debugTimer >= debugCooldown)
        {
            //go to next tile assuming there's nothing there
            FindNextDirection();
            MoveToNextTile();

            //check if there's a wall there
            FindClosestWall();

            //if there is a wall there do this
            if (nearestDistance < grid.tileSize / 2)
            {
                //go back to last position
                transform.position = lastPosition;
                //set this point as the checkpoint that you'll go back to while trying to find the next path around this wall
                checkpointPosition = lastPosition;
                checkpointDirection = nextDirection;
                //go left (based off of the last direction you moved)
                CheckLeft();
                //see if you're in a wall now
                FindClosestWall();
                if (nearestDistance < grid.tileSize / 2)
                {
                    //if you are in a wall, go back tot he checkpoint and checkright
                    transform.position = checkpointPosition;
                    CheckRight();
                    //check if you are in a wall
                    FindClosestWall();
                    if(nearestDistance < grid.tileSize / 2)
                    {
                        //go back to checkpoint
                        transform.position = checkpointPosition;
                    }
                }
            }

            //save position to last position
            lastPosition = transform.position;
            //reset timer
            debugTimer = 0;
        }
        
    }
    void FindClosestTile()
    {
        nearestDistance = float.MaxValue;
        Tiles = GameObject.FindGameObjectsWithTag("Grid");

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector3.Distance(transform.position, Tiles[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(distance < nearestDistance)
                {
                    closestTile = Tiles[i];
                    nearestDistance = distance;
                }
            }
        }
    }
    void FindClosestWall()
    {
        nearestDistance = float.MaxValue;
        Walls = GameObject.FindGameObjectsWithTag("Wall");

        if (Walls.Length > 0)
        {
            for(int i = 0; i < Walls.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector3.Distance(transform.position, Walls[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(distance < nearestDistance)
                {
                    closestTile = Tiles[i];
                    nearestDistance = distance;
                }
            }
        }
    }
    void FindNextDirection()
    {
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > Mathf.Abs(target.transform.position.y - transform.position.y))
        {
            if(target.transform.position.x > transform.position.x)
            {
                nextDirection = Vector2.right;
            }
            if(target.transform.position.x < transform.position.x)
            {
                nextDirection = Vector2.left;
            }
        }
        if (Mathf.Abs(target.transform.position.x - transform.position.x) < Mathf.Abs(target.transform.position.y - transform.position.y))
        {
            if(target.transform.position.y > transform.position.y)
            {
                nextDirection = Vector2.up;
            }
            if(target.transform.position.y < transform.position.y)
            {
                nextDirection = Vector2.down;
            }
        }
    }
    void MoveToNextTile()
    {
        transform.position = new Vector3(transform.position.x + nextDirection.x * grid.tileSize, transform.position.y + nextDirection.y * grid.tileSize, 0);
    }
    void CheckLeft()
    {
        if (checkpointDirection == Vector2.up)
        {
            transform.position = new Vector3(transform.position.x + Vector2.left.x * grid.tileSize, transform.position.y + Vector2.left.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.down)
        {
            transform.position = new Vector3(transform.position.x + Vector2.right.x * grid.tileSize, transform.position.y + Vector2.right.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.left)
        {
            transform.position = new Vector3(transform.position.x + Vector2.down.x * grid.tileSize, transform.position.y + Vector2.down.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.right)
        {
            transform.position = new Vector3(transform.position.x + Vector2.up.x * grid.tileSize, transform.position.y + Vector2.up.y * grid.tileSize, 0);
        }        
    }
    void CheckRight()
    {
        if (checkpointDirection == Vector2.up)
        {
            transform.position = new Vector3(transform.position.x + Vector2.right.x * grid.tileSize, transform.position.y + Vector2.right.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.down)
        {
            transform.position = new Vector3(transform.position.x + Vector2.left.x * grid.tileSize, transform.position.y + Vector2.left.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.left)
        {
            transform.position = new Vector3(transform.position.x + Vector2.up.x * grid.tileSize, transform.position.y + Vector2.up.y * grid.tileSize, 0);
        }
        if (checkpointDirection == Vector2.right)
        {
            transform.position = new Vector3(transform.position.x + Vector2.down.x * grid.tileSize, transform.position.y + Vector2.down.y * grid.tileSize, 0);
        }        
    }

}
