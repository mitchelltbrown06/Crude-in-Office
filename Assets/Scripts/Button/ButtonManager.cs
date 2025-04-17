using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string equiped;

    public Canvas canvas;

    //button slot info
    public bool slot1Filled = false;
    public Vector3 slot1Position;

    public bool slot2Filled = false;
    public Vector3 slot2Position;

    public bool slot3Filled = false;
    public Vector3 slot3Position;

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

    //button specifics
    public bool entrancePlaced = false;
    public bool exitPlaced = false;
    public int paths = 25;

    void Start()
    {
        slot1Position = new Vector3(55, 55, 0);
        slot2Position = new Vector3(160, 55, 0);
        slot3Position = new Vector3(265, 55, 0);

        spawnPosition = slot1Position;
        SpawnEntrance();
    }
    void Update()
    {
        CheckSpawnPosition();
    }

    public void Purchase(Button instance)
    {
        Disable(instance);
    }

    //Equips
    public void Equip(string equippable)
    {
        equiped = equippable;
    }

    //Button spawns
    public void SpawnEntrance()
    {
        entranceInstance = Instantiate(entrancePrefab, slot1Position, Quaternion.identity, canvas.transform);
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
