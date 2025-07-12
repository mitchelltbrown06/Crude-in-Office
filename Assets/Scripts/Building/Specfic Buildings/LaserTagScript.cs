using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTagScript : MonoBehaviour
{
    public LogicScript logic;
    public bool animationsStarted;
    public List<JobScript> jobNodes;
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        foreach (GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            jobNodes.Add(jobNode.GetComponent<JobScript>());
        }
    }

    void Update()
    {
        if (GetComponent<ControlWaitingRooms>().waitingRoomsOpen == true
        && animationsStarted == false)
        {
            foreach (JobScript jobNode in jobNodes)
            {
                jobNode.GetComponent<Animator>().SetTrigger("StartAnimation");
                jobNode.employee.GetComponent<npcStats>().laserTag = true;
                logic.laserTagPlayers.Add(jobNode.employee);
            }
            animationsStarted = true;
        }
        else if (GetComponent<ControlWaitingRooms>().waitingRoomsOpen == false
        && animationsStarted == true)
        {
            foreach (JobScript jobNode in jobNodes)
            {
                jobNode.transform.position = transform.position + transform.right * .5f + transform.up * .1f;
                jobNode.GetComponent<Animator>().SetTrigger("StopAnimation");
            }
            animationsStarted = false;
        }
    }
}
