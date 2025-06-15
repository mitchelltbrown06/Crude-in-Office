using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToWaitingRoom : MonoBehaviour
{
    private BuildingScript buildingScript;
    // Start is called before the first frame update
    void Start()
    {
        buildingScript = GetComponent<BuildingScript>();
        ConnectNodes();
    }
    void ConnectNodes()
    {
        foreach(GameObject waitingRoom in buildingScript.waitingRooms)
        {
            buildingScript.door.GetComponent<Node>().connections.Add(waitingRoom.GetComponent<Node>());
            waitingRoom.GetComponent<Node>().connections.Add(buildingScript.door.GetComponent<Node>());
        }
    }
}
