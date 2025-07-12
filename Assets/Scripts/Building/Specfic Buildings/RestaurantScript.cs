using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class RestaurantScript : MonoBehaviour
{
    public List<GameObject> fedCustomers;
    private ControlWaitingRooms[] controllers;
    public List<ControlWaitingRooms> tablesToFeed;
    // Update is called once per frame
    void Start()
    {
        controllers = GetComponentsInChildren<ControlWaitingRooms>();
        foreach (ControlWaitingRooms controller in controllers)
        {
            tablesToFeed.Add(controller);
        }
    }
    void Update()
    {
        foreach (ControlWaitingRooms controller in controllers)
        {
            if (controller.waitingRoomsOpen == true && tablesToFeed.Contains(controller))
            {
                JobScript[] jobNodes = GetComponentsInChildren<JobScript>();
                foreach (JobScript jobNode in jobNodes)
                {
                    if (jobNode.GetComponent<JobScript>().employee != null
                    && !fedCustomers.Contains(jobNode.GetComponent<JobScript>().employee))
                    {
                        Feed(jobNode.GetComponent<JobScript>().employee);
                    }
                }
                tablesToFeed.Remove(controller);
            }
            else if (controller.waitingRoomsOpen = false && !tablesToFeed.Contains(controller))
            {
                tablesToFeed.Add(controller);
            }
        }
    }
    public void Feed(GameObject employee)
    {
        employee.GetComponent<npcStats>().hunger = 0;
        employee.GetComponent<npcStats>().bathroom += 10;
        fedCustomers.Add(employee);
    }
}
