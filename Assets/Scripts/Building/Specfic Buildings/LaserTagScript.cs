using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTagScript : MonoBehaviour
{
    public bool timerStarted;
    public float timer;
    public float timerDuration;

    public LogicScript logic;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
    }

    void Update()
    {
        foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            if(jobNode.GetComponent<JobScript>().employee != null
            && Vector2.Distance(jobNode.transform.position, jobNode.GetComponent<JobScript>().employee.transform.position) < .1f
            && jobNode.GetComponent<JobScript>().jobTimer == 0)
            {
                jobNode.GetComponent<Animator>().SetTrigger("StartAnimation");
            }
            if(jobNode.GetComponent<JobScript>().occupied == false)
            {
                jobNode.transform.position = new Vector3(transform.position.x + .5f, transform.position.y + .1f, 0);
                jobNode.GetComponent<Animator>().SetTrigger("StopAnimation");
            }
        }
        if(GetComponent<ControlWaitingRooms>().waitingRoomsOpen == true)
        {
            timerStarted = true;
            foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
            {
                jobNode.GetComponent<JobScript>().employee.GetComponent<npcStats>().laserTag = true;
                logic.laserTagPlayers.Add(jobNode.GetComponent<JobScript>().employee);
            }
        }
        if(timerStarted)
        {
            timer += Time.deltaTime;
            if(timer > timerDuration)
            {
                /*
                foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
                {
                    jobNode.GetComponent<JobScript>().jobTimer = 100;
                }
                */
                timerStarted = false;
                timer = 0;
            }
        }
    }
}
