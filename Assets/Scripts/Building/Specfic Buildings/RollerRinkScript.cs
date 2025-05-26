using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerRinkScript : MonoBehaviour
{
    public List<GameObject> boostedCustomers;

    void Update()
    {
        foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            if(jobNode.GetComponent<JobScript>().employee != null 
            && !boostedCustomers.Contains(jobNode.GetComponent<JobScript>().employee)
            && Vector2.Distance(transform.position, jobNode.GetComponent<JobScript>().employee.transform.position) < GameObject.FindObjectOfType<GridScript>().tileSize)
            {
                jobNode.transform.parent.GetComponent<Animator>().SetTrigger("StartAnimation");
                jobNode.GetComponent<JobScript>().employee.GetComponent<npcStats>().speed = jobNode.GetComponent<JobScript>().employee.GetComponent<npcStats>().speed * 1.6f;
                boostedCustomers.Add(jobNode.GetComponent<JobScript>().employee);
            }
            if(jobNode.GetComponent<JobScript>().occupied == false)
            {
                jobNode.transform.parent.GetComponent<Animator>().SetTrigger("StopAnimation");
                jobNode.transform.parent.transform.rotation = transform.rotation;
            }
        }
    }
}
