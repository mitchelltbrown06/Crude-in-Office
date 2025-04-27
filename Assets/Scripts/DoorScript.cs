using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject[] npcs;
    public GridScript grid;
    public Node openJob;

    void Start()
    {
        openJob = null;
        grid = GameObject.FindObjectOfType<GridScript>();
        npcs = GameObject.FindGameObjectsWithTag("npc");
        GetComponent<Node>().connections.Add(FindClosestTile(FindClosestPath(transform.position).transform.position).GetComponent<Node>());
        FindClosestTile(FindClosestPath(transform.position).transform.position).GetComponent<Node>().connections.Add(GetComponent<Node>());
    }
 /*\\
    // Update is called once per frame
    void Update()
    {
        CheckNPCS();
    }
    void CheckNPCS()
    {
        foreach(GameObject npc in npcs)
        {
            if(Vector2.Distance(npc.transform.position, transform.position) < grid.tileSize / 2)
            {
                npc.GetComponent<npcController>().jobToDo = true;
                npc.GetComponent<npcController>().jobNode = openJobNode;
                openJobNode.occupied = true;
            }
        }
    }
    */
    public GameObject FindClosestPath(Vector3 position)
    {
        float nearestDistance = float.MaxValue;
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        GameObject path = null;

        if (paths.Length > 0)
        {
            for(int i = 0; i < paths.Length; i++)
            {
                float distance = Vector3.Distance(position, paths[i].transform.position);

                if(distance < nearestDistance)
                {
                    path = paths[i];
                    nearestDistance = distance;
                }
            }
            return path;
        }
        return null;
    }
    public GameObject FindClosestTile(Vector3 position)
    {
        float nearestDistance = float.MaxValue;
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Grid");
        GameObject closeTile = null;

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                float distance = Vector3.Distance(position, Tiles[i].transform.position);

                if(distance < nearestDistance)
                {
                    closeTile = Tiles[i];
                    nearestDistance = distance;
                }
            }
            if(closeTile != null)
            {
                return closeTile;
            }
        }
        return null;
    }
}
