using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject oilrig;
    public GameObject mint;
    public LogicScript Logic;
    public GridScript grid;
    //Vector3 spawnLocation;
    private float nearestBuildingDistance;
    float distance;
    public GameObject closestTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTile();
        if(Input.GetKeyDown(KeyCode.Space) && Logic.Coins >= Logic.OilRigPrice && Logic.Equiped == "OilRig")
        {
            Instantiate(oilrig, new Vector3(closestTile.transform.position.x + grid.tileSize / 2, closestTile.transform.position.y + grid.tileSize / 2, 0), Quaternion.identity);
            Logic.PurchaseOilRig();
        }
        if(Input.GetKeyDown(KeyCode.Space) && Logic.Coins >= Logic.MintPrice && Logic.Equiped == "Mint")
        {
            Instantiate(mint, new Vector3(closestTile.transform.position.x + grid.tileSize / 2, closestTile.transform.position.y + grid.tileSize / 2, 0), Quaternion.identity);
            Logic.PurchaseMint();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
  //  {
        //Debug.Log("collision");
        //if (collision.gameObject.tag == "Grid")
        //{
         //   spawnLocation = collision.transform.position;
       //     Debug.Log("Grid location: "+ spawnLocation);
       /// }
   // }
   void FindClosestTile()
    {
        nearestBuildingDistance = float.MaxValue;
        Tiles = GameObject.FindGameObjectsWithTag("Grid");

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector3.Distance(this.transform.position, Tiles[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(distance < nearestBuildingDistance)
                {
                    closestTile = Tiles[i];
                    nearestBuildingDistance = distance;
                }
            }
        }
    }
}