using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomScript : MonoBehaviour
{
    private LogicScript logic;
    public JobScript jobNode;
    public GameObject employee;

    private ControlWaitingRooms controller;
    public bool customerWaiting;

    private float previousSpeed;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        controller = transform.root.GetComponent<ControlWaitingRooms>();
        jobNode = transform.GetChild(0).GetComponent<JobScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if(jobNode != null && jobNode.employee != null && controller != null)
        {
            employee = jobNode.employee;

            //if a customer is within range and the waiting rooms are open, stop the customer that is within range;
            if(Vector2.Distance(employee.transform.position, transform.position) < .1f 
            && employee.GetComponent<npcJob>().jobToDo == true)
            {
                customerWaiting = true;
            }
            else
            {
                customerWaiting = false;
            }
            if(controller.waitingRoomsOpen == false 
            && Vector2.Distance(employee.transform.position, transform.position) < .1f 
            && employee.GetComponent<npcJob>().jobToDo == true
            && jobNode.jobTimer == 0)
            {
                if(employee.GetComponent<npcStats>().speed != 0)
                {
                    previousSpeed = employee.GetComponent<npcStats>().speed;
                }
                employee.GetComponent<npcStats>().speed = 0;
            }
            else if(controller.waitingRoomsOpen == true  
            && employee.GetComponent<npcJob>().jobToDo == true)
            {
                employee.GetComponent<npcStats>().speed = previousSpeed;
            }
        }
    }
}
