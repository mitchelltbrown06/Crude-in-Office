using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantScript : MonoBehaviour
{
    public List<GameObject> fedCustomers;
    // Update is called once per frame
    void Update()
    {
        foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            if(jobNode.GetComponent<JobScript>().employee != null 
            && !fedCustomers.Contains(jobNode.GetComponent<JobScript>().employee)
            && jobNode.transform.parent.GetComponent<WaitingRoomScript>().controller.waitingRoomsOpen == true)
            {
                Feed(jobNode.GetComponent<JobScript>().employee);
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
