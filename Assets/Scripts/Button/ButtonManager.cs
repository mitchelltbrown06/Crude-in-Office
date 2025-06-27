using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string equiped;

    public Canvas canvas;

    //button list
    public List<Button> buttonsSpawned;
    //button slot info
    public bool slotZFilled = false;
    public Vector3 slotZPosition;

    public bool slot1Filled = false;
    public Vector3 slot1Position;

    public bool slot2Filled = false;
    public Vector3 slot2Position;

    public bool slot3Filled = false;
    public Vector3 slot3Position;

    public bool slot4Filled = false;
    public Vector3 slot4Position;

    public bool slot5Filled = false;
    public Vector3 slot5Position;

    public bool slot6Filled = false;
    public Vector3 slot6Position;

    public bool slot7Filled = false;
    public Vector3 slot7Position;

    public Vector3 spawnPosition;

    //buttons
    public Button entrancePrefab;
    public Button entranceInstance;

    public Button exitPrefab;
    public Button exitInstance;
    
    public Button pathPrefab;
    public Button pathInstance;

    public Button arcadeMachinePrefab;
    public Button arcadeMachineInstance;

    public Button rollerRinkPrefab;
    public Button rollerRinkInstance;

    public Button laserTagPrefab;
    public Button laserTagInstance;

    public Button casinoPrefab;
    public Button casinoInstance;

    public Button restaurantPrefab;
    public Button restaurantInstance;

    public Button bathroomPrefab;
    public Button bathroomInstance;

    //button specifics
    public bool entrancePlaced = false;
    public bool exitPlaced = false;
    public int paths = 25;

    void Start()
    {
        slot1Position = new Vector3(55, 55, 0);
        slot2Position = new Vector3(160, 55, 0);
        slot3Position = new Vector3(265, 55, 0);
        slot4Position = new Vector3(370, 55, 0);
        slot5Position = new Vector3(475, 55, 0);
        slot6Position = new Vector3(580, 55, 0);
        slot7Position = new Vector3(685, 55, 0);

        slotZPosition = new Vector3(784, 55, 0);

        spawnPosition = slot1Position;
        
        SpawnEntrance();
        equiped = "null";
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(equiped == "Bulldozer")
            {
                equiped = "null";
            }
            else
            {
                equiped = "Bulldozer";
            }
        }
        if(equiped == "Bulldozer")
        {
            canvas.GetComponent<Image>().enabled = true;
        }
        else
        {
            canvas.GetComponent<Image>().enabled = false;
        }
    }

    public void Purchase(Button instance)
    {
        Disable(instance);
    }

    public void Equip(string equippable)
    {
        equiped = equippable;
    }

    //Button spawns
    public void SpawnEntrance()
    {
        entranceInstance = Instantiate(entrancePrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(entranceInstance);
        entranceInstance.onClick.AddListener(EntranceOnClick);
    }
    public void SpawnExit()
    {
        exitInstance = Instantiate(exitPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(exitInstance);
        exitInstance.onClick.AddListener(ExitOnClick);
    }
    public void SpawnPath()
    {
        pathInstance = Instantiate(pathPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(pathInstance);
        pathInstance.onClick.AddListener(PathOnClick);
    }
    public void SpawnArcadeMachine()
    {
        arcadeMachineInstance = Instantiate(arcadeMachinePrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(arcadeMachineInstance);
        arcadeMachineInstance.onClick.AddListener(ArcadeMachineOnClick);
    }
    public void SpawnRollerRink()
    {
        rollerRinkInstance = Instantiate(rollerRinkPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(rollerRinkInstance);
        rollerRinkInstance.onClick.AddListener(RollerRinkOnClick);
    }
    public void SpawnLaserTag()
    {
        laserTagInstance = Instantiate(laserTagPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(laserTagInstance);
        laserTagInstance.onClick.AddListener(LaserTagOnClick);
    }
    public void SpawnCasino()
    {
        casinoInstance = Instantiate(casinoPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(casinoInstance);
        casinoInstance.onClick.AddListener(CasinoOnClick);
    }
    public void SpawnRestaurant()
    {
        restaurantInstance = Instantiate(restaurantPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(restaurantInstance);
        restaurantInstance.onClick.AddListener(RestaurantOnClick);
    }
    public void SpawnBathroom()
    {
        bathroomInstance = Instantiate(bathroomPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        Enable(bathroomInstance);
        bathroomInstance.onClick.AddListener(BathroomOnClick);
    }

    //OnClicks
    void EntranceOnClick()
    {
        Equip("Entrance");
    }
    void ExitOnClick()
    {
        Equip("Exit");
    }
    void PathOnClick()
    {
        Equip("Path");
    }
    void ArcadeMachineOnClick()
    {
        Equip("ArcadeMachine");
    }
    void RollerRinkOnClick()
    {
        Equip("RollerRink");
    }
    void LaserTagOnClick()
    {
        Equip("LaserTag");
    }
    void CasinoOnClick()
    {
        Equip("Casino");
    }
    void RestaurantOnClick()
    {
        Equip("Restaurant");
    }
    void BathroomOnClick()
    {
        Equip("Bathroom");
    }
    public void CheckSpawnPosition()
    {
        if (!slot1Filled)
            {
                spawnPosition = slot1Position;
            }
            else if(!slot2Filled)
            {
                spawnPosition = slot2Position;
            }
            else if(!slot3Filled)
            {
                spawnPosition = slot3Position;
            }
            else if(!slot4Filled)
            {
                spawnPosition = slot4Position;
            }
            else if(!slot5Filled)
            {
                spawnPosition = slot5Position;
            }
            else if(!slot6Filled)
            {
                spawnPosition = slot6Position;
            }
    }
    void Disable(Button instance)
    {
        instance.GetComponent<ButtonDisable>().Disable();
    }
    void Enable(Button instance)
    {
        instance.GetComponent<ButtonEnable>().Enable();
    }

}
