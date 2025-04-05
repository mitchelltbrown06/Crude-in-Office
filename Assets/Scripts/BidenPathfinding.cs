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
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBuilding();
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
}
