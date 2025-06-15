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
    public bool occupied = false;
    public DoorScript door;

    public LogicScript logic;
    public GridScript grid;

    void Start()
    {
        occupied = false;
        jobTimer = 0;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
        door = transform.root.transform.Find("Door").GetComponent<DoorScript>();
    }
    void Update()
    {
        //if you don't have an employee, look for one and select the closest one
        if(employee == null)
        {
            SelectClosestEmployee();
        }
        //if you have an employee and it's at the job position, start its job
        else if(Vector2.Distance(transform.position, employee.transform.position) < .1f)
        {
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
            }
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
            && candidate.GetComponent<npcJob>().jobToDo == false)
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
                employee.GetComponent<npcController>().currentNode = logic.FindNearestNode(employee.transform.position);
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
    }
}
