using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueScript : MonoBehaviour
{
    public bool queueOpen;
    public Queue<GameObject> customers;
    public int maxCustomers;
    public GameObject nodePrefab;
    public LogicScript logic;
    public GridScript grid;
    public GameObject employee;

    public float price;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindObjectOfType<GridScript>();
        logic = GameObject.FindObjectOfType<LogicScript>();
        price = transform.root.GetComponent<SetPrices>().jobPrice;
    }
    void Update()
    {
        if(customers.Count > maxCustomers)
        {
            queueOpen = false;
            Debug.Log("Queue is full");
        }
        else
        {
            //Find employees
        }
    }
    public void CheckIfFull()
    {
        foreach(GameObject jobNode in transform.root.GetComponent<BuildingScript>().jobNodes)
        {
            if(jobNode.GetComponent<JobScript>().occupied == false)
            {
                return;
            }
        }
        queueOpen = true;
    }
}
