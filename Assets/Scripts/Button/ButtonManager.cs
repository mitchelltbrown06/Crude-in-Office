using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public string equiped;

    public Canvas canvas;

    //entrance and exit info
    public GameObject entrance;
    public GameObject exit;
    public bool entrancePlaced = false;
    public bool exitPlaced = false;

    //button slot info
    public bool slot1Filled = false;
    public Vector3 slot1Position = new Vector3(0, 0, 0);
    public Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = slot1Position;
        Instantiate(entrance, spawnPosition, Quaternion.identity, canvas.transform);
    }
    void Update()
    {
        if (!slot1Filled)
        {
            spawnPosition = slot1Position;
        }
    }
    public void PurchaseEntrance()
    {
        entrancePlaced = true;
        entrance.GetComponent<ButtonDisable>().Disable();
        Instantiate(exit, spawnPosition, Quaternion.identity, canvas.transform);
    }
    public void PurchaseExit()
    {
        exitPlaced = true;
        exit.GetComponent<ButtonDisable>().Disable();
    }
    public void EquipEntrance()
    {
        equiped = "Entrance";
    }
    public void EquipExit()
    {
        equiped = "Exit";
    }
}
