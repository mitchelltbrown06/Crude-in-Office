using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeScript : MonoBehaviour
{
    public GameObject door;
    public GameObject node1;
    public GameObject node2;
    public GameObject node3;
    public GameObject node4;

    void Start()
    {
        ConnectNodes();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpenJob();
    }
    void ConnectNodes()
    {
        door.GetComponent<Node>().connections.Add(node1.GetComponent<Node>());
        door.GetComponent<Node>().connections.Add(node2.GetComponent<Node>());
        door.GetComponent<Node>().connections.Add(node3.GetComponent<Node>());
        door.GetComponent<Node>().connections.Add(node4.GetComponent<Node>());

        node1.GetComponent<Node>().connections.Add(door.GetComponent<Node>());
        node2.GetComponent<Node>().connections.Add(door.GetComponent<Node>());
        node3.GetComponent<Node>().connections.Add(door.GetComponent<Node>());
        node4.GetComponent<Node>().connections.Add(door.GetComponent<Node>());
    }
    void UpdateOpenJob()
    {
        if(node1.GetComponent<Node>().occupied == false)
        {
            door.GetComponent<DoorScript>().openJob = node1.GetComponent<Node>();
        }
        else if(node2.GetComponent<Node>().occupied == false)
        {
            door.GetComponent<DoorScript>().openJob = node2.GetComponent<Node>();
        }
        else if(node3.GetComponent<Node>().occupied == false)
        {
            door.GetComponent<DoorScript>().openJob = node3.GetComponent<Node>();
        }
        else if(node4.GetComponent<Node>().occupied == false)
        {
            door.GetComponent<DoorScript>().openJob = null;
        }
    }
}
