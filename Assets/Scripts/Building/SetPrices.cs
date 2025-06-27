using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrices : MonoBehaviour
{
    public float jobPrice;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject jobNode in GetComponent<BuildingScript>().jobNodes)
        {
            jobNode.GetComponent<JobScript>().price = jobPrice;
        }
    }
}
