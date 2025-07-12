using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GridScript grid;
    public Node openJob;
    private LogicScript logic;
    public List<GameObject> rejectionList;
    public List<Node> pathConnections;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        openJob = null;
        grid = GameObject.FindObjectOfType<GridScript>();
        foreach (GameObject path in logic.FindPathsInRange(transform.position, grid.tileSize))
        {
            GetComponent<Node>().connections.Add(logic.FindClosestTile(path.transform.position).GetComponent<Node>());
            logic.FindClosestTile(path.transform.position).GetComponent<Node>().connections.Add(GetComponent<Node>());
            pathConnections.Add(logic.FindClosestTile(path.transform.position).GetComponent<Node>());
        }
    }
}
