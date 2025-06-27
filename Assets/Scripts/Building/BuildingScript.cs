using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject door;

    public List<GameObject> jobNodes;
    public List<GameObject> waitingRooms;

    public bool adultsOnly;
    public bool hungryOnly;
    public bool bathroomOnly;

    void Start()
    {
        FindJobNodes();
        FindWaitingRooms();
        SetRestrictions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpenJob();
        SetDoor();
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
    void FindJobNodes()
    {
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNodes.Add(jobNode.gameObject);
        }
    }
    void FindWaitingRooms()
    {
        foreach(WaitingRoomScript waitingRoom in GetComponentsInChildren<WaitingRoomScript>())
        {
            waitingRooms.Add(waitingRoom.gameObject);
        }
    }
    void SetDoor()
    {
        foreach(GameObject jobNode in jobNodes)
        {
            jobNode.GetComponent<JobScript>().door = door.GetComponent<DoorScript>();
        }
    }
    void SetRestrictions()
    {
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNode.adultsOnly = adultsOnly;
        }
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNode.hungryOnly = hungryOnly;
        }
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNode.bathroomOnly = bathroomOnly;
        }
    }
}
