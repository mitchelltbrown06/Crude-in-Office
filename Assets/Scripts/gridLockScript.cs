using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridLockScript : MonoBehaviour
{
    public GridScript grid;

    public float nearestDistance;
    public GameObject closestTile;
    public GameObject[] Tiles;
    float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTile();
        transform.position = closestTile.transform.position;
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
}