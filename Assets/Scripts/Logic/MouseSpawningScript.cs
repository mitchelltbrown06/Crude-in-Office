using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseSpawningScript : MonoBehaviour
{

    //Prefabs
    public GameObject exit;
    public GameObject entrance;
    public GameObject path;
    public GameObject arcadeMachine;
    public GameObject rollerRink;
    public GameObject laserTag;
    public GameObject casino;
    public GameObject restaurant;
    public GameObject bathroom;

    //Previews
    public GameObject bulldozerPreview;
    public GameObject bulldozerPreviewInstance;
    
    public GameObject entrancePreview;
    public GameObject entrancePreviewInstance;

    public GameObject pathPreview;
    public GameObject pathPreviewInstance;

    public GameObject arcadePreview;
    public GameObject arcadePreviewInstance;

    public GameObject rollerRinkPreview;
    public GameObject rollerRinkPreviewInstance;

    public GameObject laserTagPreview;
    public GameObject laserTagPreviewInstance;

    public GameObject casinoPreview;
    public GameObject casinoPreviewInstance;

    public GameObject restaurantPreview;
    public GameObject restaurantPreviewInstance;

    public GameObject bathroomPreview;
    public GameObject bathroomPreviewInstance;

    //managers
    public ButtonManager buttonManager;
    public LogicScript logic;
    public GridScript grid;

    //Vector3 spawnLocation;
    public GameObject closestTile;
    public GameObject closestPath;
    public Vector3 closestTileCrossection;

    private Vector3 mousePosition;
    public LayerMask buildingLayer;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        buildingLayer = LayerMask.GetMask("Building");
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        closestTile = FindClosestTile(Camera.main.ScreenToWorldPoint(mousePosition));
        
        closestTileCrossection = new Vector3(FindClosestTile(new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x 
                                                                - grid.tileSize / 2, Camera.main.ScreenToWorldPoint(mousePosition).y
                                                                - grid.tileSize / 2, 0)).transform.position.x + grid.tileSize / 2,
                                                                FindClosestTile(new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x 
                                                                    - grid.tileSize / 2, Camera.main.ScreenToWorldPoint(mousePosition).y 
                                                                    - grid.tileSize / 2, 0)).transform.position.y + grid.tileSize / 2, 0);

        closestPath = FindClosestPath(Camera.main.ScreenToWorldPoint(mousePosition));

        SpawnPreviews();

        //When the mouse is clicked down
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !Physics2D.OverlapBox(closestTile.transform.position, new Vector2(.1f, .1f), 0, buildingLayer))
        {
            if(buttonManager.equiped == "Bulldozer")
            {
                Bulldoze();
            }
            if(buttonManager.entrancePlaced == false && buttonManager.equiped == "Entrance" && closestTile.GetComponent<Node>().onPath == false)
            {
                PlaceEntrance();
            }
            if(buttonManager.equiped == "Path" && Vector2.Distance(closestTile.transform.position, logic.placedPaths[^1].transform.position) < grid.tileSize * 1.1 && closestTile.GetComponent<Node>().onPath == false)
            {
                PlacePath();
            }
            //one tile buildings
            if(buttonManager.equiped == "ArcadeMachine")
            {
                Place1TileBuilding(arcadeMachine, arcadePreviewInstance, buttonManager.arcadeMachineInstance);
            }
            if(buttonManager.equiped == "RollerRink")
            {
                Place4TileBuilding(rollerRink, rollerRinkPreviewInstance, buttonManager.rollerRinkInstance);
            }
            if(buttonManager.equiped == "LaserTag")
            {
                Place3TileBuilding(laserTag, laserTagPreviewInstance, buttonManager.laserTagInstance);
            }
            if(buttonManager.equiped == "Casino")
            {
                Place2TileBuilding(casino, casinoPreviewInstance, buttonManager.casinoInstance);
            }
            if(buttonManager.equiped == "Restaurant")
            {
                Place4TileBuilding(restaurant, restaurantPreviewInstance, buttonManager.restaurantInstance);
            }
            if(buttonManager.equiped == "Bathroom")
            {
                Place1TileBuilding(bathroom, bathroomPreviewInstance, buttonManager.bathroomInstance);
            }
        }
    }
    void SpawnPreviews()
    {
        SpawnBulldozerPreview();
        SpawnEntrancePreview();
        SpawnPathPreview();
        SpawnArcadePreview();
        SpawnRollerRinkPreview();
        SpawnLaserTagPreview();
        SpawnCasinoPreview();
        SpawnRestaurantPreview();
        SpawnBathroomPreview();
    }
    void SpawnBulldozerPreview()
    {
        if(buttonManager.equiped == "Bulldozer")
        {
            //if there's no preview currently spawned, spawn one in
            if(bulldozerPreviewInstance == null)
            {
                bulldozerPreviewInstance = Instantiate(bulldozerPreview, closestTile.transform.position, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display1TilePreview(bulldozerPreviewInstance, bulldozerPreviewInstance.transform.position);
            }
        }
        else if(bulldozerPreviewInstance != null)
        {
            Destroy(bulldozerPreviewInstance);
        }
    }
    void SpawnEntrancePreview()
    {
        if(buttonManager.equiped == "Entrance")
        {
            //if there's no preview currently spawned, spawn one in
            if(entrancePreviewInstance == null)
            {
                entrancePreviewInstance = Instantiate(entrancePreview, closestTile.transform.position, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display1TilePreview(entrancePreviewInstance, entrancePreviewInstance.transform.position);
            }
        }
        else if(entrancePreviewInstance != null)
        {
            Destroy(entrancePreviewInstance);
        }
    }
    void SpawnPathPreview()
    {
        if(buttonManager.equiped == "Path")
        {
            //if there's no preview currently spawned, spawn one in
            if(pathPreviewInstance == null)
            {
                pathPreviewInstance = Instantiate(pathPreview, closestTile.transform.position, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                DisplayPathPreview(pathPreviewInstance);
            }
        }
        else if(pathPreviewInstance != null)
        {
            Destroy(pathPreviewInstance);
        }
    }
    void SpawnArcadePreview()
    {
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
                Display1TilePreview(arcadePreviewInstance, arcadePreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(arcadePreviewInstance != null)
        {
            Destroy(arcadePreviewInstance);
        }
    }
    void SpawnRollerRinkPreview()
    {
        if(buttonManager.equiped == "RollerRink")
        {
            //if there's no preview currently spawned, spawn one in
            if(rollerRinkPreviewInstance == null)
            {
                rollerRinkPreviewInstance = Instantiate(rollerRinkPreview, closestTileCrossection, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display4TilePreview(rollerRinkPreviewInstance, rollerRinkPreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(rollerRinkPreviewInstance != null)
        {
            Destroy(rollerRinkPreviewInstance);
        }
    }
    void SpawnLaserTagPreview()
    {
        if(buttonManager.equiped == "LaserTag")
        {
            //if there's no preview currently spawned, spawn one in
            if(laserTagPreviewInstance == null)
            {
                laserTagPreviewInstance = Instantiate(laserTagPreview, closestTileCrossection, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display3TilePreview(laserTagPreviewInstance, laserTagPreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(laserTagPreviewInstance != null)
        {
            Destroy(laserTagPreviewInstance);
        }
    }
    void SpawnCasinoPreview()
    {
        if(buttonManager.equiped == "Casino")
        {
            //if there's no preview currently spawned, spawn one in
            if(casinoPreviewInstance == null)
            {
                casinoPreviewInstance = Instantiate(casinoPreview, closestTileCrossection, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display2TilePreview(casinoPreviewInstance, casinoPreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(casinoPreviewInstance != null)
        {
            Destroy(casinoPreviewInstance);
        }
    }
    void SpawnRestaurantPreview()
    {
        if(buttonManager.equiped == "Restaurant")
        {
            //if there's no preview currently spawned, spawn one in
            if(restaurantPreviewInstance == null)
            {
                restaurantPreviewInstance = Instantiate(restaurantPreview, closestTileCrossection, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display4TilePreview(restaurantPreviewInstance, restaurantPreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(restaurantPreviewInstance != null)
        {
            Destroy(restaurantPreviewInstance);
        }
    }
    void SpawnBathroomPreview()
    {
        if(buttonManager.equiped == "Bathroom")
        {
            //if there's no preview currently spawned, spawn one in
            if(bathroomPreviewInstance == null)
            {
                bathroomPreviewInstance = Instantiate(bathroomPreview, closestTile.transform.position, Quaternion.identity);
            }
            //everything you do if there is a preview
            else
            {
                Display1TilePreview(bathroomPreviewInstance, bathroomPreviewInstance.transform.GetChild(0).transform.position);
            }
        }
        else if(bathroomPreviewInstance != null)
        {
            Destroy(bathroomPreviewInstance);
        }
    }
    void PlaceEntrance()
    {
        Instantiate(entrance, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity);
        logic.placedPaths.Add(Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity));
        buttonManager.Purchase(buttonManager.entranceInstance);
        buttonManager.entrancePlaced = true;
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnPath();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnArcadeMachine();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnRollerRink();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnLaserTag();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnCasino();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnRestaurant();
        buttonManager.CheckSpawnPosition();
        buttonManager.SpawnBathroom();
        buttonManager.equiped = "null";
    }
    void PlacePath()
    {
        if(buttonManager.paths >= 0
        && closestTile.GetComponent<Node>().onBuilding == false
        && closestTile.GetComponent<Node>().onPath == false)
        {   
            buttonManager.paths -= 1;
            logic.placedPaths.Add(Instantiate(path, new Vector3(closestTile.transform.position.x, closestTile.transform.position.y, 0), Quaternion.identity));
        }
        if(buttonManager.paths <= 0 && buttonManager.pathInstance != null)
        {
            buttonManager.Purchase(buttonManager.pathInstance);
            buttonManager.equiped = "null";
        }
    }
    void Place1TileBuilding(GameObject prefab, GameObject preview, Button button)
    {
        if(buttonManager.entrancePlaced == true 
        && closestTile.GetComponent<Node>().onPath == false
        && FindClosestTile(FindClosestPath(preview.transform.GetChild(0).transform.position).transform.position).GetComponent<Node>().onEntranceOrExit == false
        && closestTile.GetComponent<Node>().onBuilding == false
        && Vector2.Distance(FindClosestPath(preview.transform.GetChild(0).transform.position).transform.position, preview.transform.GetChild(0).transform.position) < grid.tileSize * 0.6
        )
        {
            foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                node.onBuilding = true;
            }
            //PlaceArcade();
            Instantiate(prefab, preview.transform.position, preview.transform.rotation);
            //get ride of the arcade button
            buttonManager.Purchase(button);

            buttonManager.equiped = "null";
            //get ride of the arcade preview
            Destroy(preview);
        }
    }
    void Place2TileBuilding(GameObject prefab, GameObject preview, Button button)
    {
        foreach(GameObject path in logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f))
        {
            if(logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
            || Vector2.Distance(path.transform.position, closestTileCrossection) < grid.tileSize * .9f)
            {
                return;
            }
        }
        foreach(Node node in logic.FindTilesInRange((preview.transform.Find("Door").transform.position + closestTileCrossection) / 2f, grid.tileSize * .51f))
        {
            if(node.onBuilding == true
            || node.onPath == true)
            {
                return;
            }
        }
        if(logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f).Count == 2)
        {
            foreach(Node node in logic.FindTilesInRange((preview.transform.Find("Door").transform.position + closestTileCrossection) / 2f, grid.tileSize * .51f))
            {
                node.onBuilding = true;
            }
            //PlaceArcade();
            Instantiate(prefab, preview.transform.position, preview.transform.rotation);
            //get ride of the arcade button
            buttonManager.Purchase(button);

            buttonManager.equiped = "null";
            //get ride of the arcade preview
            Destroy(preview);
        }
    }
    void Place3TileBuilding(GameObject prefab, GameObject preview, Button button)
    {
        if(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f).Count == 1
        && preview.transform.InverseTransformPoint(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position) == new Vector3(-.5f, -.5f, 0)
        && logic.FindClosestTile(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position).GetComponent<Node>().onEntranceOrExit == false
        )
        {
            foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                if(node.onBuilding == true)
                {
                    return;
                }
            }
            foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                if(node.onPath == false)
                {
                    node.onBuilding = true;
                }
            }
            Instantiate(prefab, preview.transform.position, preview.transform.rotation);
            //get ride of the arcade button
            buttonManager.Purchase(button);

            buttonManager.equiped = "null";
            //get ride of the arcade preview
            Destroy(preview);
        }
        else
        {
            return;
        }
    }
    void Place4TileBuilding(GameObject prefab, GameObject preview, Button button)
    {
        foreach(GameObject path in logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f))
        {

            if(logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
            || Vector2.Distance(path.transform.position, closestTileCrossection) < grid.tileSize * .9f)
            {
                return;
            }
        }
        foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
        {
            if(node.onBuilding == true
            || node.onPath == true)
            {
                return;
            }
        }
        if(logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f).Count == 2)
        {
            foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                node.onBuilding = true;
            }
            //PlaceArcade();
            Instantiate(prefab, preview.transform.position, preview.transform.rotation);
            //get ride of the arcade button
            buttonManager.Purchase(button);

            buttonManager.equiped = "null";
            //get ride of the arcade preview
            Destroy(preview);
        }
    }
    // all the code for displaying previews
    void Display1TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = closestTile.transform.position;

        if(preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if(Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            //if the preview is on a tile that is spawnable, it's color values should be normal. If not, turn it red
            if(closestTile.GetComponent<Node>().onPath == false
            && FindClosestTile(FindClosestPath(pathConnectionPoint).transform.position).GetComponent<Node>().onEntranceOrExit == false
            && closestTile.GetComponent<Node>().onBuilding == false
            && Vector2.Distance(FindClosestPath(pathConnectionPoint).transform.position, pathConnectionPoint) < grid.tileSize * 0.6
            )
            {
                //this finds all the sprite renderers for each child object
                foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                {
                    sr.color = new Color(1f, 1f, 1f, 0.7f);
                }
                return;
            }
            foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display2TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = closestTileCrossection;

        if(preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if(Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            foreach(GameObject path in logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f))
            {
                if(logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true 
                || Vector2.Distance(path.transform.position, closestTileCrossection) < grid.tileSize * .9f)
                {
                    foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            //if the surrounding tiles are on buildings, don't spawn
            foreach(Node node in logic.FindTilesInRange((pathConnectionPoint + closestTileCrossection) / 2f, grid.tileSize * .51f))
            {
                if(node.onBuilding == true)
                {
                    foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            if(logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f).Count == 2)
            {
                //this finds all the sprite renderers for each child object
                foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display3TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = closestTileCrossection;

        if(preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if(Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }
            if(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f).Count == 1
            && preview.transform.InverseTransformPoint(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position) == new Vector3(-.5f, -.5f, 0)
            && logic.FindClosestTile(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position).GetComponent<Node>().onEntranceOrExit == false
            )
            {
                foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
                {
                    if(node.onBuilding == true)
                    {
                        foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                        {
                            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                        }
                        return;
                    }
                }
                foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display4TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = closestTileCrossection;

        if(preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if(Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            foreach(GameObject path in logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f))
            {

                if(logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true 
                || Vector2.Distance(path.transform.position, closestTileCrossection) < grid.tileSize * .9f)
                {
                    foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            if(logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f).Count == 2)
            {
                //if the surrounding tiles are on buildings, don't spawn
                foreach(Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
                {
                    if(node.onBuilding == true)
                    {
                        foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                        {
                            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                        }
                        return;
                    }
                }
                //this finds all the sprite renderers for each child object
                foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    // all the code for displaying previews
    void DisplayPathPreview(GameObject preview)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = closestTile.transform.position;

        if(Vector2.Distance(logic.placedPaths[^1].transform.position, preview.transform.position) < grid.tileSize * 1.1f
        && logic.FindClosestTile(preview.transform.position).GetComponent<Node>().onBuilding == false
        && logic.FindClosestTile(preview.transform.position).GetComponent<Node>().onPath == false
        )
        {
            foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
            {
                sr.color = new Color(1f, 1f, 1f, 0.7f);
            }
            return;
        }
        foreach(SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>()) 
        {
            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
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
    void Bulldoze()
    {
        //make arrays of all the buildings in the scene and all the entities in the scene
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        //go through each building and, if the closest tile to the building is the same as the cursor tile, destroy the building
        foreach(GameObject building in buildings)
        {
            if(Vector2.Distance(FindClosestTile(building.transform.position).transform.position, closestTile.transform.position) < .1f)
            {
                building.GetComponent<DemolishScript>().Demolish();
            }
        }

        //Go through every path in the list of paths that have been placed and if that path is at cursor tile, delete it and all of the following paths
        for(int i = 0; i < logic.placedPaths.Count; i++)
        {
            if(Vector2.Distance(FindClosestTile(logic.placedPaths[i].transform.position).transform.position, closestTile.transform.position) < .1f && i > 1)
            {
                for(int j = logic.placedPaths.Count - 1; j > -1; j--)
                {
                    if(j > i)
                    {
                        //clear the connections between nodes
                        FindClosestTile(logic.placedPaths[j].transform.position).GetComponent<Node>().connections.Clear();
                        logic.placedPaths[j].GetComponent<DemolishScript>().Demolish();

                        //remove this path from the list of paths
                        logic.placedPaths.RemoveAt(j);
                    }
                }
                FindClosestTile(logic.placedPaths[i].transform.position).GetComponent<Node>().connections.Clear();
                logic.placedPaths[i].GetComponent<DemolishScript>().Demolish();

                exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
                exit.transform.position = logic.placedPaths[i-1].transform.position;
                
                logic.placedPaths.RemoveAt(i);

                //check if any buildings were cut off
                foreach(GameObject building in buildings)
                {
                    if(building.transform.Find("Door").GetComponent<Node>().connections[building.transform.Find("Door").GetComponent<Node>().connections.Count -1].connections.Count == 0
                    || Vector2.Distance(building.transform.Find("Door").GetComponent<Node>().connections[building.transform.Find("Door").GetComponent<Node>().connections.Count -1].transform.position, exit.transform.position) < .1f)
                    {
                        building.GetComponent<DemolishScript>().Demolish();
                    }
                }
                //go through all nodes and, if they have a connections, add them to a list.
                //go through all the connected nodes and check how many connections each of their connections has.
                //if one of their connections doesn't have a connection, remove it as a connection.
                List<Node> nodesWithConnections = new List<Node>();

                foreach(Node node in logic.NodesInScene())
                {
                    nodesWithConnections.Add(node);
                }
                foreach(Node node in nodesWithConnections)
                {
                    foreach(Node connectedNode in node.connections)
                    {
                        if(connectedNode.connections.Count == 0)
                        {
                            node.connections.Remove(connectedNode);
                            break;
                        }
                    }
                }
            }
        }
    }
}