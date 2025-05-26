using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject door;
    public GameObject secondDoor;

    public List<GameObject> jobNodes; 
    public List<GameObject> waitingRooms;

    void Start()
    {
        ConnectNodes();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpenJob();
    }

    void ConnectNodes()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<JobScript>())
            {
                jobNodes.Add(child.gameObject);
                child.GetComponent<JobScript>().door = door.GetComponent<DoorScript>();
            }
            foreach(Transform childOfChild in child.transform)
            {
                if(childOfChild.GetComponent<JobScript>())
                {
                    jobNodes.Add(childOfChild.gameObject);
                    childOfChild.GetComponent<JobScript>().door = door.GetComponent<DoorScript>();
                }
            }
        }
        foreach(GameObject jobNode in jobNodes)
        {
            door.GetComponent<Node>().connections.Add(jobNode.GetComponent<Node>());
            jobNode.GetComponent<Node>().connections.Add(door.GetComponent<Node>());
        }
    }
    void UpdateOpenJob()
    {
        foreach(GameObject jobNode in jobNodes)
        {
            if(jobNode.GetComponent<JobScript>().occupied == false)
            {
                door.GetComponent<DoorScript>().openJob = jobNode.GetComponent<Node>();
                return;
            }
        }
        door.GetComponent<DoorScript>().openJob = null;
    }
}
