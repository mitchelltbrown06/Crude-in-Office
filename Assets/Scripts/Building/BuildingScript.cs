using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject door;

    public List<GameObject> jobNodes;
    public List<GameObject> waitingRooms;

    public LogicScript logic;
    public GlobalStats stats;

    public bool adultsOnly;
    public bool hungryOnly;
    public bool bathroomOnly;

    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Logic").GetComponent<GlobalStats>();
        logic = GameObject.FindObjectOfType<LogicScript>();
        FindJobNodes();
        FindWaitingRooms();
        UpdateOpenJob();
        SetDoor();
        foreach (Node node in GetComponentsInChildren<Node>())
        {
            node.gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public void UpdateOpenJob()
    {
        foreach (GameObject jobNode in jobNodes)
        {
            if (jobNode.GetComponent<JobScript>().occupied == false)
            {
                door.GetComponent<DoorScript>().openJob = jobNode.GetComponent<Node>();
                return;
            }
        }
        door.GetComponent<DoorScript>().openJob = null;
    }
    void FindJobNodes()
    {
        foreach (JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNodes.Add(jobNode.gameObject);
        }
    }
    void FindWaitingRooms()
    {
        foreach (WaitingRoomScript waitingRoom in GetComponentsInChildren<WaitingRoomScript>())
        {
            waitingRooms.Add(waitingRoom.gameObject);
        }
    }
    void SetDoor()
    {
        foreach (GameObject jobNode in jobNodes)
        {
            jobNode.GetComponent<JobScript>().door = door.GetComponent<DoorScript>();
        }
    }
    public bool CandidateCheck(GameObject candidate)
    {
        if (adultsOnly == true)
        {
            if (candidate.GetComponent<npcStats>().adult == false)
            {
                return false;
            }
        }
        if(hungryOnly == true)
        {
            if(candidate.GetComponent<npcStats>().hunger < stats.hungryCutoff)
            {
                return false;
            }
        }
        else
        {
            if(candidate.GetComponent<npcStats>().hunger > stats.starvation)
            {
                return false;
            }
        }
        if(bathroomOnly == true)
        {
            if(candidate.GetComponent<npcStats>().bathroom < stats.bathroomCutoff)
            {
                return false;
            }
        }
        return true;
    }
}
