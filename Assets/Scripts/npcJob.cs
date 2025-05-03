using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcJob : MonoBehaviour
{
    private npcController myController;
    private npcStats myStats;

    public bool jobToDo = false;

    void Start()
    {
        myStats = GetComponent<npcStats>();
        myController = GetComponent<npcController>();
    }

    public void FindJob(GameObject door)
    {
        if(myController.nextNode != null)
        {
            if(Vector2.Distance(door.transform.position, myController.nextNode.transform.position) < myController.buildingCaptureDistance
            && door.GetComponent<DoorScript>().openJob != null
            && !door.GetComponent<DoorScript>().rejectionList.Contains(gameObject)
            && door.GetComponent<DoorScript>().openJob.GetComponent<JobScript>().price < myStats.money
            )
            {
                //clear your path so you can make a new one
                myController.path.Clear();
                //set jobNode to the open job that you found at the building
                myController.jobNode = door.GetComponent<DoorScript>().openJob;
                //JobToDo is now true because you have a job
                jobToDo = true;
                //Set jobfilled on your jobNode to true so that other npcs don't take your job.
                door.GetComponent<DoorScript>().openJob.gameObject.GetComponent<JobScript>().JobFilled();
                //Set employee on jobNode to this gameObject
                myController.jobNode.gameObject.GetComponent<JobScript>().employeeCandidates.Add(gameObject);
            }
        }
    }
    public void JobComplete(float price)
    {
        jobToDo = false;
        myStats.SpendMoney(price);
    }
}
