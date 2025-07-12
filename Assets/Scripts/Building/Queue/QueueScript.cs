using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class QueueScript : MonoBehaviour
{
    private Vector3 boxCastCenter;
    public bool queueOpen;
    public Queue<GameObject> customers;
    public int maxCustomers;
    public GameObject nodePrefab;
    public LogicScript logic;
    public GridScript grid;
    public GameObject employee;
    public BuildingScript building;
    public List<Node> nodeConnections;
    public float price;
    public GameObject door;
    public float moveRate;

    // Start is called before the first frame update
    void Start()
    {
        door = transform.root.transform.Find("Door").gameObject;
        boxCastCenter = door.GetComponent<Node>().connections[^1].transform.position;
        customers = new Queue<GameObject>();
        building = transform.root.GetComponent<BuildingScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
        logic = GameObject.FindObjectOfType<LogicScript>();
    }
    void Update()
    {
        //check if this queue is full. if so, close the queue
        if (queueOpen == true
        && customers != null
        && customers.Count >= maxCustomers)
        {
            queueOpen = false;
        }
        else if (queueOpen == true)
        {
            //Find employees
            FindEmployee();
        }
    }
    public void CheckIfFull()
    {
        foreach (GameObject jobNode in building.jobNodes)
        {
            if (jobNode.GetComponent<JobScript>().occupied == false)
            {
                queueOpen = false;
                return;
            }
        }
        queueOpen = true;
    }
    void FindEmployee()
    {
        foreach (Node connection in door.GetComponent<DoorScript>().pathConnections)
            {
                boxCastCenter = connection.transform.position;
                GameObject candidate;
                RaycastHit2D[] collisions = Physics2D.BoxCastAll(boxCastCenter, new Vector2(grid.tileSize, grid.tileSize), 0f, Vector2.up, 0f, logic.entityLayerMask);
                foreach (RaycastHit2D collision in collisions)
                {
                    if (collision.collider != null)
                    {
                        candidate = collision.collider.gameObject;
                    if (!door.GetComponent<DoorScript>().rejectionList.Contains(candidate)
                        && price < candidate.GetComponent<npcStats>().money
                        && candidate.GetComponent<npcJob>().jobToDo == false
                        && door.GetComponent<Node>().connections.Contains(logic.FindClosestTile(candidate.transform.position).GetComponent<Node>())
                        && building.CandidateCheck(candidate) == true
                        )
                    {
                        employee = candidate;
                        customers.Enqueue(employee);
                        employee.GetComponent<npcJob>().jobToDo = true;
                        //create a node and assign that node to the employee's "queue node" slot.
                        Node queueNode = Instantiate(nodePrefab, new Vector3(Random.Range(transform.localScale.x / 2, -transform.localScale.x / 2) + transform.position.x, Random.Range(transform.localScale.y / 2, -transform.localScale.y / 2)
                                                                                                + transform.position.y, 0), transform.rotation, transform).GetComponent<Node>();
                        queueNode.GetComponent<QueueNode>().employee = employee;
                        employee.GetComponent<npcJob>().queueNode = queueNode;
                        employee.GetComponent<npcStateManager>().SwitchState(employee.GetComponent<npcStateManager>().GoingToQueueState);
                        foreach (Node node in nodeConnections)
                        {
                            //add each node that leads to a job node to the list of connections on the new queue node
                            queueNode.connections.Add(node);
                        }
                        //add the queue node to the list of connections for the door
                        door.GetComponent<Node>().connections.Add(queueNode);
                        return;
                    }
                }
            }
        }
    }
}
