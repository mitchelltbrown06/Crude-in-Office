using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject[] npcs;
    public GridScript grid;
    public Node openJob;
    private LogicScript logic;
    public List<GameObject> rejectionList;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        openJob = null;
        grid = GameObject.FindObjectOfType<GridScript>();
        npcs = GameObject.FindGameObjectsWithTag("npc");
        foreach(GameObject path in logic.FindPathsInRange(transform.position, grid.tileSize))
        {
            GetComponent<Node>().connections.Add(logic.FindClosestTile(path.transform.position).GetComponent<Node>());
            logic.FindClosestTile(path.transform.position).GetComponent<Node>().connections.Add(GetComponent<Node>());
        }
        
    }
}
