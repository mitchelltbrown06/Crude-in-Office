using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomScript : MonoBehaviour
{
    private LogicScript logic;
    public JobScript jobNode;
    public GameObject employee;

    public ControlWaitingRooms controller;
    public bool customerWaiting;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        jobNode = transform.GetChild(0).GetComponent<JobScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (employee == null || (employee != null && employee != jobNode.employee))
        {
            if (jobNode != null && jobNode.employee != null && controller != null)
            {
                employee = jobNode.employee;
                employee.GetComponent<npcJob>().waitingRoomNode = GetComponent<Node>();
            }
        }
        else if (employee.GetComponent<npcStateManager>().currentState != employee.GetComponent<npcStateManager>().WaitingState)
        {
            if (controller.waitingRoomsOpen == false
            && Vector2.Distance(employee.transform.position, transform.position) < .1f
            && employee.GetComponent<npcJob>().jobToDo == true
            && jobNode.timerStarted == false
            && employee.GetComponent<npcStateManager>().currentState == employee.GetComponent<npcStateManager>().GoingToJobState)
            {
                customerWaiting = true;
                controller.CheckOccupancy();
                employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().WaitingState);
            }
            else
            {
                customerWaiting = false;
            }
        }
        else if (controller.waitingRoomsOpen == true
        && employee.GetComponent<npcJob>().jobToDo == true
        && employee.GetComponent<npcStateManager>().currentState == employee.GetComponent<npcStateManager>().WaitingState)
        {
            employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().GoingToJobState);
        }
    }
}
