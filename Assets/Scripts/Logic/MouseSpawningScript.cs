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
        SpawnPreviews();

        //When the mouse is clicked down
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !Physics2D.OverlapBox(logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, new Vector2(.1f, .1f), 0, buildingLayer))
        {
            if (buttonManager.equiped == "Bulldozer")
            {
                Bulldoze();
            }
            if (buttonManager.entrancePlaced == false && buttonManager.equiped == "Entrance" && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath == false)
            {
                PlaceEntrance();
            }
            if (buttonManager.equiped == "Path" && Vector2.Distance(logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, logic.placedPaths[^1].transform.position) < grid.tileSize * 1.1 && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath == false)
            {
                PlacePath();
            }
            //one tile buildings
            if (buttonManager.equiped == "Arcade")
            {
                Place1TileBuilding(arcadeMachine, arcadePreviewInstance, buttonManager.arcadeMachineInstance);
            }
            if (buttonManager.equiped == "RollerRink")
            {
                Place4TileBuilding(rollerRink, rollerRinkPreviewInstance, buttonManager.rollerRinkInstance);
            }
            if (buttonManager.equiped == "LaserTag")
            {
                Place3TileBuilding(laserTag, laserTagPreviewInstance, buttonManager.laserTagInstance);
            }
            if (buttonManager.equiped == "Casino")
            {
                Place2TileBuilding(casino, casinoPreviewInstance, buttonManager.casinoInstance);
            }
            if (buttonManager.equiped == "Restaurant")
            {
                Place4TileBuilding(restaurant, restaurantPreviewInstance, buttonManager.restaurantInstance);
            }
            if (buttonManager.equiped == "Bathroom")
            {
                Place1TileBuilding(bathroom, bathroomPreviewInstance, buttonManager.bathroomInstance);
            }
        }
    }
    void SpawnPreviews()
    {
        if (buttonManager.equiped == "Bulldozer")
        {
            SpawnBulldozerPreview();
        }
        else
        {
            Destroy(bulldozerPreviewInstance);
        }
        if (buttonManager.equiped == "Entrance")
        {
            SpawnEntrancePreview();
        }
        else
        {
            Destroy(entrancePreviewInstance);
        }
        if (buttonManager.equiped == "Path")
        {
            SpawnPathPreview();
        }
        else
        {
            Destroy(pathPreviewInstance);
        }
        if (buttonManager.equiped == "Arcade")
        {
            SpawnArcadePreview();
        }
        else
        {
            Destroy(arcadePreviewInstance);
        }
        if (buttonManager.equiped == "RollerRink")
        {
            SpawnRollerRinkPreview();
        }
        else
        {
            Destroy(rollerRinkPreviewInstance);
        }
        if (buttonManager.equiped == "LaserTag")
        {
            SpawnLaserTagPreview();
        }
        else
        {
            Destroy(laserTagPreviewInstance);
        }
        if (buttonManager.equiped == "Casino")
        {
            SpawnCasinoPreview();
        }
        else
        {
            Destroy(casinoPreviewInstance);
        }
        if (buttonManager.equiped == "Restaurant")
        {
            SpawnRestaurantPreview();
        }
        else
        {
            Destroy(restaurantPreviewInstance);
        }
        if (buttonManager.equiped == "Bathroom")
        {
            SpawnBathroomPreview();
        }
        else
        {
            Destroy(bathroomPreviewInstance);
        }
    }
    void SpawnBulldozerPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (bulldozerPreviewInstance == null)
        {
            bulldozerPreviewInstance = Instantiate(bulldozerPreview, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display1TilePreview(bulldozerPreviewInstance, bulldozerPreviewInstance.transform.position);
        }
    }
    void SpawnEntrancePreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (entrancePreviewInstance == null)
        {
            entrancePreviewInstance = Instantiate(entrancePreview, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display1TilePreview(entrancePreviewInstance, entrancePreviewInstance.transform.position);
        }
    }
    void SpawnPathPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (pathPreviewInstance == null)
        {
            pathPreviewInstance = Instantiate(pathPreview, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            DisplayPathPreview(pathPreviewInstance);
        }
    }
    void SpawnArcadePreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (arcadePreviewInstance == null)
        {
            arcadePreviewInstance = Instantiate(arcadePreview, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display1TilePreview(arcadePreviewInstance, arcadePreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void SpawnRollerRinkPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (rollerRinkPreviewInstance == null)
        {
            rollerRinkPreviewInstance = Instantiate(rollerRinkPreview, FindClosestCrossection(), Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display4TilePreview(rollerRinkPreviewInstance, rollerRinkPreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void SpawnLaserTagPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (laserTagPreviewInstance == null)
        {
            laserTagPreviewInstance = Instantiate(laserTagPreview, FindClosestCrossection(), Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display3TilePreview(laserTagPreviewInstance, laserTagPreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void SpawnCasinoPreview()
{
        //if there's no preview currently spawned, spawn one in
        if (casinoPreviewInstance == null)
        {
            casinoPreviewInstance = Instantiate(casinoPreview, FindClosestCrossection(), Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display2TilePreview(casinoPreviewInstance, casinoPreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void SpawnRestaurantPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (restaurantPreviewInstance == null)
        {
            restaurantPreviewInstance = Instantiate(restaurantPreview, FindClosestCrossection(), Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display4TilePreview(restaurantPreviewInstance, restaurantPreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void SpawnBathroomPreview()
    {
        //if there's no preview currently spawned, spawn one in
        if (bathroomPreviewInstance == null)
        {
            bathroomPreviewInstance = Instantiate(bathroomPreview, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position, Quaternion.identity);
        }
        //everything you do if there is a preview
        else
        {
            Display1TilePreview(bathroomPreviewInstance, bathroomPreviewInstance.transform.GetChild(0).transform.position);
        }
    }
    void PlaceEntrance()
    {
        Instantiate(entrance, new Vector3(logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.x, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.y, 0), Quaternion.identity);
        logic.placedPaths.Add(Instantiate(path, new Vector3(logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.x, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.y, 0), Quaternion.identity));
        logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath = true;

        //For testing
        buttonManager.Disable(buttonManager.entranceInstance);
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
        if (buttonManager.paths >= 0
        && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onBuilding == false
        && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath == false)
        {
            buttonManager.paths -= 1;
            logic.placedPaths.Add(Instantiate(path, new Vector3(logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.x, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position.y, 0), Quaternion.identity));
            logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath = true;
        }
        if (buttonManager.paths <= 0 && buttonManager.pathInstance != null)
        {
            buttonManager.Purchase(buttonManager.pathInstance);
            buttonManager.equiped = "null";
        }
    }
    void Place1TileBuilding(GameObject prefab, GameObject preview, Button button)
    {
        if (buttonManager.entrancePlaced == true
        && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath == false
        && logic.FindClosestTile(logic.FindClosestPath(preview.transform.GetChild(0).transform.position).transform.position).GetComponent<Node>().onEntranceOrExit == false
        && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onBuilding == false
        && Vector2.Distance(logic.FindClosestPath(preview.transform.GetChild(0).transform.position).transform.position, preview.transform.GetChild(0).transform.position) < grid.tileSize * 0.6
        )
        {
            foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
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
        foreach (GameObject path in logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f))
        {
            if (logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
            || Vector2.Distance(path.transform.position, FindClosestCrossection()) < grid.tileSize * .9f)
            {
                return;
            }
        }
        foreach (Node node in logic.FindTilesInRange((preview.transform.Find("Door").transform.position + FindClosestCrossection()) / 2f, grid.tileSize * .51f))
        {
            if (node.onBuilding == true
            || node.onPath == true)
            {
                return;
            }
        }
        if (logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f).Count == 2)
        {
            foreach (Node node in logic.FindTilesInRange((preview.transform.Find("Door").transform.position + FindClosestCrossection()) / 2f, grid.tileSize * .51f))
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
        if (logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f).Count == 1
        && preview.transform.InverseTransformPoint(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position) == new Vector3(-.5f, -.5f, 0)
        && logic.FindClosestTile(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position).GetComponent<Node>().onEntranceOrExit == false
        )
        {
            foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                if (node.onBuilding == true)
                {
                    return;
                }
            }
            foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
            {
                if (node.onPath == false)
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
        foreach (GameObject path in logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f))
        {

            if (logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
            || Vector2.Distance(path.transform.position, FindClosestCrossection()) < grid.tileSize * .9f)
            {
                return;
            }
        }
        foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
        {
            if (node.onBuilding == true
            || node.onPath == true)
            {
                return;
            }
        }
        if (logic.FindPathsInRange(preview.transform.Find("Door").transform.position, grid.tileSize * .9f).Count == 2)
        {
            foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
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
        preview.transform.position = logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position;

        if (preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            //if the preview is on a tile that is spawnable, it's color values should be normal. If not, turn it red
            if (logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onPath == false
            && logic.FindClosestTile(logic.FindClosestPath(pathConnectionPoint).transform.position).GetComponent<Node>().onEntranceOrExit == false
            && logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Node>().onBuilding == false
            && Vector2.Distance(logic.FindClosestPath(pathConnectionPoint).transform.position, pathConnectionPoint) < grid.tileSize * 0.6
            )
            {
                //this finds all the sprite renderers for each child object
                foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = new Color(1f, 1f, 1f, 0.7f);
                }
                return;
            }
            foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display2TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = FindClosestCrossection();

        if (preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            foreach (GameObject path in logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f))
            {
                if (logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
                || Vector2.Distance(path.transform.position, FindClosestCrossection()) < grid.tileSize * .9f)
                {
                    foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            //if the surrounding tiles are on buildings, don't spawn
            foreach (Node node in logic.FindTilesInRange((pathConnectionPoint + FindClosestCrossection()) / 2f, grid.tileSize * .51f))
            {
                if (node.onBuilding == true)
                {
                    foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            if (logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f).Count == 2)
            {
                //this finds all the sprite renderers for each child object
                foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display3TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = FindClosestCrossection();

        if (preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }
            if (logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f).Count == 1
            && preview.transform.InverseTransformPoint(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position) == new Vector3(-.5f, -.5f, 0)
            && logic.FindClosestTile(logic.FindPathsInRange(preview.transform.position, grid.tileSize * .9f)[0].transform.position).GetComponent<Node>().onEntranceOrExit == false
            )
            {
                foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
                {
                    if (node.onBuilding == true)
                    {
                        foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                        {
                            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                        }
                        return;
                    }
                }
                foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    void Display4TilePreview(GameObject preview, Vector3 pathConnectionPoint)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = FindClosestCrossection();

        if (preview.CompareTag("BuildingPreview"))
        {
            //if you press r, the preview should rotate
            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.transform.Rotate(0, 0, -90);
            }

            foreach (GameObject path in logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f))
            {

                if (logic.FindClosestTile(path.transform.position).GetComponent<Node>().onEntranceOrExit == true
                || Vector2.Distance(path.transform.position, FindClosestCrossection()) < grid.tileSize * .9f)
                {
                    foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                    }
                    return;
                }
            }
            if (logic.FindPathsInRange(pathConnectionPoint, grid.tileSize * .9f).Count == 2)
            {
                //if the surrounding tiles are on buildings, don't spawn
                foreach (Node node in logic.FindTilesInRange(preview.transform.position, grid.tileSize))
                {
                    if (node.onBuilding == true)
                    {
                        foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                        {
                            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                        }
                        return;
                    }
                }
                //this finds all the sprite renderers for each child object
                foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = new Color(1f, 1f, 1f, .7f);
                }
                return;
            }
            foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            }
        }
    }
    // all the code for displaying previews
    void DisplayPathPreview(GameObject preview)
    {
        //update the preview position to be at the cursor tile
        preview.transform.position = logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position;

        if (Vector2.Distance(logic.placedPaths[^1].transform.position, preview.transform.position) < grid.tileSize * 1.1f
        && logic.FindClosestTile(preview.transform.position).GetComponent<Node>().onBuilding == false
        && logic.FindClosestTile(preview.transform.position).GetComponent<Node>().onPath == false
        )
        {
            foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 1f, 1f, 0.7f);
            }
            return;
        }
        foreach (SpriteRenderer sr in preview.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = new Color(1f, 0.5f, 0.5f, 0.7f);
        }
    }
    void Bulldoze()
    {
        //make arrays of all the buildings in the scene and all the entities in the scene
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        //go through each building and, if the closest tile to the building is the same as the cursor tile, destroy the building
        foreach (GameObject building in buildings)
        {
            if (Vector2.Distance(logic.FindClosestTile(building.transform.position).transform.position, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position) < .1f)
            {
                building.GetComponent<DemolishScript>().Demolish();
            }
        }

        //Go through every path in the list of paths that have been placed and if that path is at cursor tile, delete it and all of the following paths
        for (int i = 0; i < logic.placedPaths.Count; i++)
        {
            if (Vector2.Distance(logic.FindClosestTile(logic.placedPaths[i].transform.position).transform.position, logic.FindClosestTile(Camera.main.ScreenToWorldPoint(Input.mousePosition)).transform.position) < .1f && i > 1)
            {
                for (int j = logic.placedPaths.Count - 1; j > -1; j--)
                {
                    if (j > i)
                    {
                        //clear the connections between nodes
                        logic.FindClosestTile(logic.placedPaths[j].transform.position).GetComponent<Node>().connections.Clear();
                        logic.placedPaths[j].GetComponent<DemolishScript>().Demolish();

                        //set onpath and onentranceorexit to false for the tile at this path
                        logic.FindClosestTile(logic.placedPaths[j].transform.position).GetComponent<Node>().onPath = false;
                        logic.FindClosestTile(logic.placedPaths[j].transform.position).GetComponent<Node>().onEntranceOrExit = false;

                        //remove this path from the list of paths
                        logic.placedPaths.RemoveAt(j);
                    }
                }
                logic.FindClosestTile(logic.placedPaths[i].transform.position).GetComponent<Node>().connections.Clear();
                logic.placedPaths[i].GetComponent<DemolishScript>().Demolish();

                //set onpath and onentranceorexit to false for the tile at this path
                logic.FindClosestTile(logic.placedPaths[i].transform.position).GetComponent<Node>().onPath = false;
                logic.FindClosestTile(logic.placedPaths[i].transform.position).GetComponent<Node>().onEntranceOrExit = false;

                if (exit == null)
                {
                    exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
                }
                exit.transform.position = logic.placedPaths[i - 1].transform.position;
                logic.FindClosestTile(exit.transform.position).GetComponent<Node>().onEntranceOrExit = true;
                
                foreach (GameObject npc in logic.npcs)
                {
                    if (npc.GetComponent<npcStateManager>().currentState == npc.GetComponent<npcStateManager>().ExitingState
                    && !npc.GetComponent<npcStateManager>().ExitingState.path.Contains(logic.FindNearestNode(exit.transform.position)))
                    {
                        npc.GetComponent<npcStateManager>().ExitingState.updateNeeded = true;
                    }
                }

                logic.placedPaths.RemoveAt(i);

                //check if any buildings were cut off
                foreach (GameObject building in buildings)
                {
                    if (building.transform.Find("Door").GetComponent<Node>().connections[building.transform.Find("Door").GetComponent<Node>().connections.Count - 1].connections.Count == 0
                    || Vector2.Distance(building.transform.Find("Door").GetComponent<Node>().connections[building.transform.Find("Door").GetComponent<Node>().connections.Count - 1].transform.position, exit.transform.position) < .1f)
                    {
                        building.GetComponent<DemolishScript>().Demolish();
                    }
                }
                //go through all nodes and, if they have a connections, add them to a list.
                //go through all the connected nodes and check how many connections each of their connections has.
                //if one of their connections doesn't have a connection, remove it as a connection.
                List<Node> nodesWithConnections = new List<Node>();

                foreach (Node node in logic.nodesInScene)
                {
                    nodesWithConnections.Add(node);
                }
                foreach (Node node in nodesWithConnections)
                {
                    foreach (Node connectedNode in node.connections)
                    {
                        if (connectedNode.connections.Count == 0)
                        {
                            node.connections.Remove(connectedNode);
                            break;
                        }
                    }
                }
            }
        }
    }
    private Vector3 FindClosestCrossection()
    {
        return new Vector3(logic.FindClosestTile(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x
                                                                - grid.tileSize / 2, Camera.main.ScreenToWorldPoint(Input.mousePosition).y
                                                                - grid.tileSize / 2, 0)).transform.position.x + grid.tileSize / 2,
                                                                logic.FindClosestTile(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x
                                                                    - grid.tileSize / 2, Camera.main.ScreenToWorldPoint(Input.mousePosition).y
                                                                    - grid.tileSize / 2, 0)).transform.position.y + grid.tileSize / 2, 0);
    }
}