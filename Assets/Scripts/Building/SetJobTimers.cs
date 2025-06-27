using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetJobTimers : MonoBehaviour
{
    public float jobDuration;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            jobNode.GetComponent<JobScript>().jobLength = jobDuration;
        }
    }
}