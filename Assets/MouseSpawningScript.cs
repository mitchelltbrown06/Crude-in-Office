using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawningScript : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject oilrig;
    public GameObject mint;
    public LogicScript Logic;
    public GridScript grid;
    //Vector3 spawnLocation;
    private float nearestDistance;
    float distance;
    public GameObject closestTile;
    private Vector3 mousePosition;
    public TrackMouse mouseTracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        FindCursorTile();
        if(Input.GetMouseButtonDown(0) && !mouseTracker.objectCollision.CompareTag("Button"))
        {
            if(Logic.Coins >= Logic.OilRigPrice && Logic.Equiped == "OilRig")
            {
                Instantiate(oilrig, new Vector3(closestTile.transform.position.x + grid.tileSize / 2, closestTile.transform.position.y + grid.tileSize / 2, 0), Quaternion.identity);
                Logic.PurchaseOilRig();
            }
            if(Logic.Coins >= Logic.MintPrice && Logic.Equiped == "Mint")
            {
                Instantiate(mint, new Vector3(closestTile.transform.position.x + grid.tileSize / 2, closestTile.transform.position.y + grid.tileSize / 2, 0), Quaternion.identity);
                Logic.PurchaseMint();
            }
        }
    }
   void FindCursorTile()
    {
        nearestDistance = float.MaxValue;
        Tiles = GameObject.FindGameObjectsWithTag("Grid");

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(mousePosition), Tiles[i].transform.position);
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