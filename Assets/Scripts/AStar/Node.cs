using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;
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

        logic.nodesInScene.Add(this);
    }
    public float FScore()
    {
        return gScore + hScore;
    }
    void Update()
    {
        if (connections.Count > 0)
        {
            foreach (Node node in connections)
            {
                if (node == null)
                {
                    connections.Remove(node);
                    return;
                }
                else
                {
                    Debug.DrawLine(this.transform.position, node.transform.position, Color.red);
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
}
