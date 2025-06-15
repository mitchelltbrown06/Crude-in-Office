using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToJobNode : MonoBehaviour
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
        foreach(GameObject jobNode in buildingScript.jobNodes)
        {
            buildingScript.door.GetComponent<Node>().connections.Add(jobNode.GetComponent<Node>());
            jobNode.GetComponent<Node>().connections.Add(buildingScript.door.GetComponent<Node>());
        }
    }
}
