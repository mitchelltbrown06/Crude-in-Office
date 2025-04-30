using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSpawningScript : MonoBehaviour
{

    //Prefabs
    public GameObject entrance;
    public GameObject path;
    public GameObject arcadeMachine;

    //Previews
    public GameObject arcadePreview;
    public GameObject arcadePreviewInstance;

    //managers
    public ButtonManager buttonManager;
    public LogicScript Logic;
    public GridScript grid;

    //Vector3 spawnLocation;
    public GameObject closestTile;
    public GameObject closestPath;

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
        closestPath = FindClosestPath(Camera.main.ScreenToWorldPoint(mousePosition));
        //displaying previews
        if(buttonManager.equiped == "ArcadeMachine")
        {
            //if there's no preview currently spawned, spawn one in
            if(arcadePreviewInstance == null)
            {
                arcadePreviewInstance = Instantiate(arcadePreview, closestTile.transform.position, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                //if you press r, the preview should rotate
                if(Input.GetKeyDown(KeyCode.R))
                {
                    arcadePreviewInstance.transform.Rotate(0, 0, -90);
                }
                //update the preview position to be at the cursor tile
                arcadePreviewInstance.transform.position = closestTile.transform.position;

                //if the preview is on a tile that is spawnable, it's color values should be normal. If not, turn it red
                if(closestTile.GetComponent<Node>().onPath == false
                && FindClosestTile(FindClosestPath(arcadePreviewInstance.transform.GetChild(0).transform.position).transform.position).GetComponent<Node>().onEntranceOrExit == false
                && Vector2.Distance(FindClosestPath(arcadePreviewInstance.transform.GetChild(0).transform.position).transform.position, arcadePreviewInstance.transform.GetChild(0).transform.position) < grid.tileSize * 0.6
                )
                {
                    //this finds all the sprite renderers for each child object
                    foreach(SpriteRenderer sr in arcadePreviewInstance.GetComponentsInChildren<SpriteRenderer>()) 
                    {
                        sr.color = new Color(1f, 1f, 1f, 0.7f);
                    }
                }
                else
                {
                    foreach(SpriteRenderer sr in arcadePreviewInstance.GetComponentsInChildren<SpriteRenderer>()) 
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                }
            }
        }
        
        //When the mouse is clicked down
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
                }
            }
            if(buttonManager.entrancePlaced == true && buttonManager.equiped == "ArcadeMachine" && closestTile.GetComponent<Node>().onPath == false
                && FindClosestTile(FindClosestPath(arcadePreviewInstance.transform.GetChild(0).transform.position).transform.position).GetComponent<Node>().onEntranceOrExit == false
                && Vector2.Distance(FindClosestPath(arcadePreviewInstance.transform.GetChild(0).transform.position).transform.position, arcadePreviewInstance.transform.GetChild(0).transform.position) < grid.tileSize * 0.6
                )
            {
                Instantiate(arcadeMachine, arcadePreviewInstance.transform.position, arcadePreviewInstance.transform.rotation);
                //get ride of the arcade button
                buttonManager.Purchase(buttonManager.arcadeMachineInstance);

                buttonManager.equiped = "null";
                //get ride of the arcade preview
                Destroy(arcadePreviewInstance);
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
}