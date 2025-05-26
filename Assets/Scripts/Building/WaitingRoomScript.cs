using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomScript : MonoBehaviour
{
    private LogicScript logic;
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(logic.FindClosestCustomer(transform.position).transform.position, transform.position) < .1f && logic.FindClosestCustomer(transform.position).GetComponent<npcJob>().jobToDo == true)
        {
            logic.FindClosestCustomer(transform.position).GetComponent<npcStats>().speed = 0;
        }
    }
}
