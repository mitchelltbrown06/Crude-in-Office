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
    public bool onBuilding = false;
    public bool onEntranceOrExit = false;
    
    void Start()
    {

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();

    }
    public float FScore()
    {
        return gScore + hScore;
    }
    void Update()
    {
        onPath = OnPath();
        onEnemy = OnEnemy();
        onBuilding = OnBuilding();
        //onEntranceOrExit = OnEntranceOrExit();

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
    public bool OnBuilding()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        foreach(GameObject building in buildings)
        {
            if (Vector2.Distance(building.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }

        return false;
    }
    public bool OnEntranceOrExit()
    {
        if(GameObject.FindObjectOfType<EntranceScript>() != null)
        {
            GameObject entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
            if(Vector2.Distance(entrance.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }
        if(GameObject.FindObjectOfType<ExitScript>() != null)
        {
            GameObject exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
            if(Vector2.Distance(exit.transform.position, transform.position) < grid.tileSize / 2)
            {
                return true;
            }
        }
        return false;
    }
}
