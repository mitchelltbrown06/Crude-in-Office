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

    public LogicScript logic;

    void Start()
    {
        jobTimer = 0;
        logic = GameObject.FindObjectOfType<LogicScript>();
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
                transform.parent.Find("Door").GetComponent<DoorScript>().rejectionList.Add(employee);
                employee = null;
                occupied = false;
                jobTimer = 0;
            }
        }
    }

    void SelectClosestEmployee()
    {
        //if you have multiple people trying to go to the same job, do this
        if(employeeCandidates.Count > 0)
        {
            float minDistance = float.MaxValue;
            GameObject closestCandidate = null;

            //go through each employee candidate and determine which one is the closest to the job
            foreach(GameObject candidate in employeeCandidates)
            {
                float currentDistance = Vector2.Distance(transform.position, candidate.transform.position);
                if(currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestCandidate = candidate;
                }
            }
            //go through each employee candidate and, if it's not the closest candidate, tell it it doesn't have a job and that it can't apply to this building anymore
            //if it is the closest candidate, set it to the employee
            foreach(GameObject candidate in employeeCandidates)
            {
                if(candidate != closestCandidate)
                {
                    candidate.GetComponent<npcJob>().jobToDo = false;
                    transform.parent.Find("Door").GetComponent<DoorScript>().rejectionList.Add(candidate);
                }
                else
                {
                    candidate.GetComponent<npcJob>().jobToDo = true;
                    employee = candidate;
                }
            }
        }
        //clear out the list of candidates so that you don't continue to reference them
        employeeCandidates.Clear();
    }
    public void JobFilled()
    {
        occupied = true;
    }
}
