using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobScript : MonoBehaviour
{
    public List<GameObject> employeeCandidates;

    public float price;
    public GameObject employee;
    public float jobLength;
    public float jobTimer;
    public bool timerStarted;
    public bool occupied = false;
    public DoorScript door;

    public LogicScript logic;
    public GridScript grid;

    //all of the variables for checking if a candidate can work at this job
    public bool adultsOnly;
    public bool hungryOnly;
    public bool bathroomOnly;

    void Start()
    {
        occupied = false;
        jobTimer = 0;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
        door = transform.root.transform.Find("Door").GetComponent<DoorScript>();
        jobLength = jobLength * Random.Range(.9f, 1.1f);
    }
    void Update()
    {
        //if you don't have an employee, look for one and select the closest one
        if(employee == null)
        {
            SelectClosestEmployee();
        }
        else if(Vector2.Distance(transform.position, employee.transform.position) < .1f)
        {
            IncreaseTimer();
        }
        
    }

    void SelectClosestEmployee()
    {
        float minDistance = float.MaxValue;

        //go through each employee candidate and determine which one is the closest to the job
        foreach(GameObject candidate in GameObject.FindGameObjectsWithTag("Entity"))
        {
            if(!door.rejectionList.Contains(candidate)
            && price < candidate.GetComponent<npcStats>().money
            && candidate.GetComponent<npcJob>().jobToDo == false
            && door.GetComponent<Node>().connections.Contains(logic.FindClosestTile(candidate.transform.position).GetComponent<Node>())
            && CandidateCheck(candidate) == true
            )
            {
                float currentDistance = Vector2.Distance(door.transform.position, candidate.GetComponent<npcController>().transform.position);
                if(currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    employee = candidate;
                }
            }
        }

        if(employee != null)
        {
            employee.GetComponent<npcJob>().jobToDo = true;
            employee.GetComponent<npcController>().jobNode = GetComponent<Node>();
            if(Vector2.Distance(door.transform.position, logic.FindNearestNode(employee.transform.position).transform.position) < grid.tileSize * .6f)
            {
                employee.GetComponent<npcController>().path.Clear();
                employee.GetComponent<npcController>().currentNode = logic.FindNearestNode(logic.FindClosestPath(employee.transform.position).transform.position);
                employee.GetComponent<npcController>().CreatePath(logic.FindNearestNode(transform.position));
            }

            JobFilled();

            //just for debugging
            //employee.transform.localScale = new Vector3(.2f, .2f, .1f);
        }
    }
    public void JobFilled()
    {
        occupied = true;
        UpdateQueue();
    }
    public bool CandidateCheck(GameObject candidate)
    {
        if(adultsOnly == true)
        {
            if(candidate.GetComponent<npcStats>().adult == false)
            {
                return false;
            }
        }
        if(hungryOnly == true)
        {
            if(candidate.GetComponent<npcStats>().hunger < logic.hungryCutoff)
            {
                return false;
            }
        }
        else
        {
            if(candidate.GetComponent<npcStats>().hunger > logic.starvation)
            {
                return false;
            }
        }
        if(bathroomOnly == true)
        {
            if(candidate.GetComponent<npcStats>().bathroom < logic.bathroomCutoff)
            {
                return false;
            }
        }
        return true;
    }
    public void IncreaseTimer()
    {
        //if you have an employee and it's at the job position, start its job
        if(transform.parent.GetComponent<WaitingRoomScript>() && transform.parent.GetComponent<WaitingRoomScript>().controller.waitingRoomsOpen == false && timerStarted == false)
        {
            return;
        }
        else
        {
            timerStarted = true;
            jobTimer += Time.deltaTime;
            //if its job is done, send it away.
            if(jobTimer > jobLength)
            {
                employee.GetComponent<npcJob>().JobComplete(price);
                door.rejectionList.Add(employee);

                //just for debugging
                //employee.transform.localScale = new Vector3(.1f, .1f, .1f);

                employee = null;
                occupied = false;
                jobTimer = 0;
                jobLength = jobLength * Random.Range(.9f, 1.1f);
                timerStarted = false;
            }
        }
    }
    void UpdateQueue()
    {
        //if this building has a queue, tell it to check if the queue should open
        if(transform.root.GetComponentsInChildren<QueueScript>() != null)
        {
            foreach(QueueScript queue in transform.root.GetComponentsInChildren<QueueScript>())
            {
                queue.CheckIfFull();
            }
        }
    }
}
