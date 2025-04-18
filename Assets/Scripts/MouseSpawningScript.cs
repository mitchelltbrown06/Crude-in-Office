using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSpawningScript : MonoBehaviour
{
    public GameObject[] Tiles;
    public PathScript[] paths;

    //Prefabs
    public GameObject entrance;
    public GameObject exit;
    public GameObject path;
    public GameObject arcadeMachine;

    //managers
    public ButtonManager buttonManager;
    public LogicScript Logic;
    public GridScript grid;

    //Vector3 spawnLocation;
    public GameObject closestTile;
    public PathScript closestPath;

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
        closestTile = FindClosestTile(Camera.main.ScreenToWorldPoint(mousePosition));
        FindClosestPath();
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !Physics2D.OverlapBox(closestTile.transform.position, new Vector2(.1f, .1f), 0, buildingLayer))
        {
            if(buttonManager.entrancePlaced == false && buttonManager.equiped == "Entrance" && closestTile.GetComponent<Node>().onPath == false)
            {
                Instantiate(entrance, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                buttonManager.Purchase(buttonManager.entranceInstance);
                buttonManager.entrancePlaced = true;
                buttonManager.SpawnPath();
                buttonManager.CheckSpawnPosition();
                buttonManager.SpawnArcadeMachine();
            }
            if(buttonManager.exitPlaced == false && buttonManager.equiped == "Exit" && Vector2.Distance(closestTile.transform.position, Logic.lastPath.transform.position) < grid.tileSize * 1.1 && closestTile.GetComponent<Node>().onPath == false)
            {
                Instantiate(exit, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                buttonManager.Purchase(buttonManager.exitInstance);
                buttonManager.exitPlaced = true;
            }
            if(buttonManager.equiped == "Path" && Vector2.Distance(closestTile.transform.position, Logic.lastPath.transform.position) < grid.tileSize * 1.1 && closestTile.GetComponent<Node>().onPath == false)
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
            if(buttonManager.entrancePlaced == true && buttonManager.equiped == "ArcadeMachine" && closestTile.GetComponent<Node>().onPath == false
                && FindClosestTile(closestPath.transform.position).GetComponent<Node>().connections.Count > 1
                && Vector2.Distance(closestPath.transform.position, closestTile.transform.position) < grid.tileSize * 1.1)
            {
                Instantiate(arcadeMachine, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
                buttonManager.Purchase(buttonManager.arcadeMachineInstance);
            }
        }
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
    void FindClosestPath()
    {
        float nearestDistance = float.MaxValue;
        paths = GameObject.FindObjectsOfType<PathScript>();

        if (paths.Length > 0)
        {
            for(int i = 0; i < paths.Length; i++)
            {
                float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(mousePosition), paths[i].transform.position);

                if(distance < nearestDistance && FindClosestTile(paths[i].transform.position).GetComponent<Node>().connections.Count > 1)
                {
                    closestPath = paths[i];
                    nearestDistance = distance;
                }
            }
        }
    }
}