using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidenPathfinding : MonoBehaviour

{
    public LayerMask buildingLayer;
    public LayerMask gridLayer;

    public JoeBidenScript movement;

    public GridScript grid;

    public GameObject[] Buildings;
    public GameObject closestBuilding;
    public float nearestBuildingDistance;
    public float buildingDistance;

    public Node[] nodes;
    public Node nodeAtBuilding;
    public float nearestNodeDistance;
    public float nodeDistance;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        Player = GameObject.FindGameObjectWithTag("Player");
        buildingLayer = LayerMask.GetMask("Building");
        gridLayer = LayerMask.GetMask("Building");
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBuilding();
        FindNodeAtBuilding();
    }

    public void FindClosestBuilding()
    {
        nearestBuildingDistance = float.MaxValue;
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
    }
    public void FindNodeAtBuilding()
    {
        nearestNodeDistance = float.MaxValue;
        nodes = FindObjectsOfType<Node>();

        if (nodes.Length > 0)
        {
            for(int i = 0; i < nodes.Length; i++)
            {
                nodeDistance = Vector2.Distance(closestBuilding.transform.position, nodes[i].transform.position);
                if(nodeDistance < nearestNodeDistance)
                {
                    nodeAtBuilding = nodes[i];
                    nearestNodeDistance = nodeDistance;
                }
            }
        }
    }
}
