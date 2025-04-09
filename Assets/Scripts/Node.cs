using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    public GameObject[] allGridTiles;
    public GridScript grid;

    public float gScore;
    public float hScore;

    public bool onWall = false;

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        FindNeighbors();
    }
    public void FindNeighbors()
    {
        allGridTiles = GameObject.FindGameObjectsWithTag("Grid");

        for (int i = 0; i < allGridTiles.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, allGridTiles[i].transform.position) < grid.tileSize * 1.5 && Vector3.Distance(this.transform.position, allGridTiles[i].transform.position) > grid.tileSize / 2f)
            {
                connections.Add(allGridTiles[i].GetComponent<Node>());
            }
        }
    }
    public float FScore()
    {
        return gScore + hScore;
    }
    void Update()
    {
        onWall = OnWall();

        if(connections.Count > 0)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i] != null)
                {
                    Debug.DrawLine(this.transform.position, connections[i].transform.position, Color.red);
                }
            }
        }
    }
    public bool OnWall()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach(GameObject wall in walls)
        {
            if (Vector2.Distance(wall.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }

        return false;
    }
}
