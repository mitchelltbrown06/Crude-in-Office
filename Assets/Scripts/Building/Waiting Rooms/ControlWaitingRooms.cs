using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWaitingRooms : MonoBehaviour
{
    private BuildingScript buildingScript;
    public bool waitingRoomsOpen;

    public List<GameObject> waitingRooms;
    public List<GameObject> jobNodes;
    public float timerDuration;
    Coroutine startTimer;
    Coroutine closeWaitingRoomsTimer;

    void Start()
    {
        buildingScript = GetComponent<BuildingScript>();
        waitingRoomsOpen = false;
        FindWaitingRooms();
        FindJobNodes();
    }
    public void CheckOccupancy()
    {
        //go through all the job nodes. If one of them isn't occupied, end the function
        //if all the job nodes are occupied, open up the waiting rooms.
        if (waitingRoomsOpen == false)
        {
            foreach (GameObject jobNode in jobNodes)
            {
                if (!jobNode.GetComponent<JobScript>().occupied)
                {
                    return;
                }
            }
            foreach (GameObject waitingRoom in waitingRooms)
            {
                if (waitingRoom.GetComponent<WaitingRoomScript>().customerWaiting == false)
                {
                    return;
                }
            }
            OpenWaitingRooms();
        }
    }
    void OpenWaitingRooms()
    {
        waitingRoomsOpen = true;
        startTimer = StartCoroutine(StartTimer());
        closeWaitingRoomsTimer = StartCoroutine(CloseWaitingRoomsTimer());
    }
    void CloseWaitingRooms()
    {
        waitingRoomsOpen = false;
        foreach (GameObject jobNode in jobNodes)
        {
            jobNode.GetComponent<JobScript>().EndJob();
            jobNode.GetComponentInParent<WaitingRoomScript>().employee = null;
        }
    }
    void FindWaitingRooms()
    {
        foreach (WaitingRoomScript waitingRoom in GetComponentsInChildren<WaitingRoomScript>())
        {
            waitingRooms.Add(waitingRoom.gameObject);
            waitingRoom.controller = this;
        }
    }
    void FindJobNodes()
    {
        foreach (JobScript jobNode in GetComponentsInChildren<JobScript>())
        {
            jobNodes.Add(jobNode.gameObject);
        }
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timerDuration);
        CloseWaitingRooms();
    }
    IEnumerator CloseWaitingRoomsTimer()
    {
        yield return new WaitForSeconds(.01f);
    }
}
