using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWaitingRooms : MonoBehaviour
{
    private BuildingScript buildingScript;
    public bool waitingRoomsOpen;

    void Start()
    {
        buildingScript = GetComponent<BuildingScript>();
        waitingRoomsOpen = false;
    }
    void Update()
    {
        CheckOccupancy();
    }
    void CheckOccupancy()
    {
        //go through all the job nodes. If one of them isn't occupied, end the function
        //if all the job nodes are occupied, open up the waiting rooms.
        if(waitingRoomsOpen == false)
        {
            foreach(GameObject jobNode in buildingScript.jobNodes)
            {
                if(!jobNode.GetComponent<JobScript>().occupied)
                {
                    return;
                }
            }
            foreach(GameObject waitingRoom in buildingScript.waitingRooms)
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
            foreach(GameObject jobNode in buildingScript.jobNodes)
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
    }
    void CloseWaitingRooms()
    {
        waitingRoomsOpen = false;
    }
}
