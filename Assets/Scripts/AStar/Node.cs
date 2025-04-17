using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    public GameObject[] allGridTiles;
    public GridScript grid;
    public LogicScript logic;

    public float gScore;
    public float hScore;

    public bool onPath = false;
    public bool onEnemy = false;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
    }
    /*
    public void FindNeighbors()
    {
        allGridTiles = GameObject.FindGameObjectsWithTag("Grid");

        for (int i = 0; i < allGridTiles.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, allGridTiles[i].transform.position) < grid.tileSize * 1.3 && Vector3.Distance(this.transform.position, allGridTiles[i].transform.position) > grid.tileSize / 2f)
            {
                connections.Add(allGridTiles[i].GetComponent<Node>());
            }
        }
    }
    */
    public float FScore()
    {
        return gScore + hScore;
    }
    void Update()
    {
        onPath = OnPath();
        onEnemy = OnEnemy();

        if(connections.Count > 0)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i] != null)
                {
                    //Debug.DrawLine(this.transform.position, connections[i].transform.position, Color.red);
                }
            }
        }
    }
    public bool OnPath()
    {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject path in paths)
        {
            if (Vector2.Distance(path.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }

        return false;
    }
    public bool OnEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }

        return false;
    }
}
