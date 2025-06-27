using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWaitingRooms : MonoBehaviour
{
    private BuildingScript buildingScript;
    public bool waitingRoomsOpen;

    public List<GameObject> waitingRooms;
    public List<GameObject> jobNodes;

    public bool timerStarted;
    public float timer;
    public float timerDuration;

    void Start()
    {
        buildingScript = GetComponent<BuildingScript>();
        waitingRoomsOpen = false;
        FindWaitingRooms();
        FindJobNodes();
    }
    void Update()
    {
        CheckOccupancy();
        IncreaseTimer();
    }
    void CheckOccupancy()
    {
        //go through all the job nodes. If one of them isn't occupied, end the function
        //if all the job nodes are occupied, open up the waiting rooms.
        if(waitingRoomsOpen == false)
        {
            foreach(GameObject jobNode in jobNodes)
            {
                if(!jobNode.GetComponent<JobScript>().occupied)
                {
                    return;
                }
            }
            foreach(GameObject waitingRoom in waitingRooms)
            {
                if(waitingRoom.GetComponent<WaitingRoomScript>().customerWaiting == false)
                {
                    return;
                }
            }
            OpenWaitingRooms();
        }
        //go through all the job nodes. If one of them is occupied, end the function
        //if all the job nodes are unoccupied, close the waiting rooms.
        else
        {
            foreach(GameObject jobNode in jobNodes)
            {
                if(jobNode.GetComponent<JobScript>().jobTimer == 0)
                {
                    return;
                }
            }
            CloseWaitingRooms();
        }
    }
    void OpenWaitingRooms()
    {
        waitingRoomsOpen = true;
        timerStarted = true;
    }
    void CloseWaitingRooms()
    {
        waitingRoomsOpen = false;
    }
    void FindWaitingRooms()
    {
        foreach(WaitingRoomScript waitingRoom in GetComponentsInChildren<WaitingRoomScript>())
        {
            waitingRooms.Add(waitingRoom.gameObject);
            waitingRoom.controller = this;
        }
    }
    void FindJobNodes()
    {
        foreach(JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNodes.Add(jobNode.gameObject);
        }
    }
    void IncreaseTimer()
    {
        if(timerStarted == true)
        {
            timer += Time.deltaTime;
            if(timer > timerDuration)
            {
                foreach(GameObject jobNode in jobNodes)
                {
                    jobNode.GetComponent<JobScript>().jobTimer = jobNode.GetComponent<JobScript>().jobLength;
                }
                timerStarted = false;
                timer = 0;
            }
        }
    }
}
