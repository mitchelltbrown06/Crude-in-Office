using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSpawningScript : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject oilrig;
    public GameObject mint;
    public GameObject wall;
    public GameObject vance;
    public GameObject entrance;
    public GameObject exit;
    public GameObject path;
    public ButtonManager buttonManager;
    public LogicScript Logic;
    public GridScript grid;
    //Vector3 spawnLocation;
    private float nearestDistance;
    float distance;
    public GameObject closestTile;
    private Vector3 mousePosition;
    public LayerMask buildingLayer;
    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindObjectOfType<LogicScript>();
        buildingLayer = LayerMask.GetMask("Building");
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        FindCursorTile();
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !Physics2D.OverlapBox(closestTile.transform.position, new Vector2(.1f, .1f), 0, buildingLayer))
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
            if(Logic.Coins >= Logic.WallPrice && Logic.Equiped == "Wall")
            {
                Instantiate(wall, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Logic.PurchaseWall();
            }
            if(Logic.Coins >= Logic.VancePrice && Logic.Equiped == "Vance")
            {
                Instantiate(vance, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Logic.PurchaseVance();
            }
            if(buttonManager.entrancePlaced == false && buttonManager.equiped == "Entrance")
            {
                Instantiate(entrance, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                buttonManager.Purchase(buttonManager.entranceInstance);
                buttonManager.entrancePlaced = true;
                buttonManager.SpawnPath();
            }
            if(buttonManager.exitPlaced == false && buttonManager.equiped == "Exit" && Vector2.Distance(closestTile.transform.position, Logic.lastPath.transform.position) < grid.tileSize * 1.1)
            {
                Instantiate(exit, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                buttonManager.Purchase(buttonManager.exitInstance);
                buttonManager.exitPlaced = true;
            }
            if(buttonManager.equiped == "Path" && Vector2.Distance(closestTile.transform.position, Logic.lastPath.transform.position) < grid.tileSize * 1.1)
            {
                buttonManager.paths -= 1;
                if(buttonManager.paths >= 0)
                {   
                    Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                }
                if(buttonManager.paths <= 0 && buttonManager.pathInstance != null)
                {
                    buttonManager.Purchase(buttonManager.pathInstance);
                    buttonManager.SpawnExit();
                }
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