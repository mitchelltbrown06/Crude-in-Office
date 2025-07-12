using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class JobScript : MonoBehaviour
{
    public List<GameObject> employeeCandidates;

    public float price;
    public GameObject employee;
    public float jobLength;
    public Coroutine timer;
    public bool timerStarted;
    public bool occupied = false;
    public DoorScript door;
    public GridScript grid;
    public LogicScript logic;
    public BuildingScript building;
    public Vector3 boxCastCenter;
    public bool animationStarted;

    void Start()
    {
        occupied = false;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridScript>();
        building = transform.root.GetComponent<BuildingScript>();
        door = transform.root.transform.Find("Door").GetComponent<DoorScript>();
        jobLength = jobLength * Random.Range(.9f, 1.1f);
    }
    void Update()
    {
        //if you don't have an employee, look for one and select the closest one
        if (employee == null)
        {
            SelectClosestEmployee();
        }
        else if (Vector2.Distance(transform.position, employee.transform.position) < .1f
        && employee.GetComponent<npcStateManager>().currentState == employee.GetComponent<npcStateManager>().GoingToJobState
        && employee.GetComponent<npcStateManager>().GoingToJobState.nextNode == employee.GetComponent<npcJob>().jobNode)
        {
            employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().WorkingState);
            timer = StartCoroutine(JobTimer());
        }

    }

    void SelectClosestEmployee()
    {
        if (transform.root.transform.GetComponentInChildren<QueueScript>()
        && transform.root.transform.GetComponentInChildren<QueueScript>().customers.Count > 0)
        {
            employee = transform.root.transform.GetComponentInChildren<QueueScript>().customers.Dequeue();
        }
        else
        {
            foreach (Node connection in door.GetComponent<DoorScript>().pathConnections)
            {
                boxCastCenter = connection.transform.position;
                GameObject candidate;
                RaycastHit2D[] collisions = Physics2D.BoxCastAll(boxCastCenter, new Vector2(grid.tileSize, grid.tileSize), 0f, Vector2.up, 0f, logic.entityLayerMask);
                foreach (RaycastHit2D collision in collisions)
                {
                    if (collision.collider != null)
                    {
                        candidate = collision.collider.gameObject;
                        if (!door.rejectionList.Contains(candidate)
                            && price < candidate.GetComponent<npcStats>().money
                            && candidate.GetComponent<npcJob>().jobToDo == false
                            && door.GetComponent<Node>().connections.Contains(logic.FindClosestTile(candidate.transform.position).GetComponent<Node>())
                            && building.CandidateCheck(candidate) == true
                            )
                        {
                            employee = candidate;
                            break;
                        }
                    }
                }
                if (employee != null)
                {
                    break;
                }
            }
        }


        if (employee != null)
        {
            employee.GetComponent<npcJob>().jobToDo = true;
            employee.GetComponent<npcJob>().jobNode = GetComponent<Node>();
            employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().GoingToJobState);

            JobFilled();
            building.UpdateOpenJob();

            //just for debugging
            //employee.transform.localScale = new Vector3(.2f, .2f, .1f);
        }
    }
    void JobFilled()
    {
        occupied = true;
        UpdateQueue();
    }
    void UpdateQueue()
    {
        //if this building has a queue, tell it to check if the queue should open
        if (transform.root.GetComponentsInChildren<QueueScript>() != null)
        {
            foreach (QueueScript queue in transform.root.GetComponentsInChildren<QueueScript>())
            {
                queue.CheckIfFull();
            }
        }
    }
    IEnumerator JobTimer()
    {
        timerStarted = true;
        yield return new WaitForSeconds(jobLength);

        employee.GetComponent<npcJob>().JobComplete(price);
        door.rejectionList.Add(employee);

        employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().ExitingState);

        //just for debugging
        //employee.transform.localScale = new Vector3(.1f, .1f, .1f);

        employee = null;
        occupied = false;
        jobLength = jobLength * Random.Range(.9f, 1.1f);
        timerStarted = false;
    }
    public void EndJob()
    {
        if (timer != null)
        {
            StopCoroutine(timer);   
        }

        employee.GetComponent<npcJob>().JobComplete(price);
        door.rejectionList.Add(employee);

        employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().ExitingState);

        //just for debugging
        //employee.transform.localScale = new Vector3(.1f, .1f, .1f);

        employee = null;
        occupied = false;
        jobLength = jobLength * Random.Range(.9f, 1.1f);
        timerStarted = false;
    }
}
