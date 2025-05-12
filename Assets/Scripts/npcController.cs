using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public GridScript grid;
    public LogicScript logic;

    public List<Node> path;
    public Node currentNode;
    public Node nextNode;
    public Node jobNode;
    private Node targetNode;

    private npcJob myJob;

    public Vector3 target;

    public GameObject closestBuilding;

    public GameObject exit;
    public GameObject entrance;

    public float speed;

    public float buildingCaptureDistance;
    
    public bool firstTargetSet = false;

    public float updateCooldown;
    public float updateTimer;

    void Start()
    {
        myJob = GetComponent<npcJob>();
        entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();

        buildingCaptureDistance = grid.tileSize * .9f;
    }
    void Update()
    {
        if(nextNode != null && nextNode.connections.Count == 0)
        {
            GetComponent<KillScript>().Kill(); 
        }
        if(nextNode == null && firstTargetSet == true)
        {
            GetComponent<KillScript>().Kill();
        }

        //increase the update timer that determines if the path should be cleared and regenerated
        updateTimer += Time.deltaTime;

        //If at the exit, die
        if(nextNode != null && Vector2.Distance(transform.position, target) < .1f && Vector2.Distance(nextNode.transform.position, exit.transform.position) < .1f)
        {
            Destroy(gameObject);
        }
        
        //find the closest building
        closestBuilding = FindClosestBuilding(transform.position);

        //if you don't have a current node, teleport to the closest node and set that to current node
        if(currentNode == null)
        {
            currentNode = FindClosestConnectedNode();
        }

        //If you don't have a job, look for one
        if(closestBuilding != null && myJob.jobToDo == false)
        {
            myJob.FindJob(closestBuilding.transform.Find("Door").gameObject);
        }

        //Create a path to either the exit or your job if you have one
        if(exit != null)
        {
            if(myJob.jobToDo == false)
            {
                CreatePath(exit.transform.position);
            }
            else if(jobNode != null)
            {
                CreatePath(jobNode.transform.position);
            }
            FollowPath();
        }
    }

    void CreatePath(Vector3 destination)
    {
        if(path.Count == 0 && currentNode.connections.Count > 0)
        { 
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(destination));
        }
        if(firstTargetSet == false)
        {
            target = path[0].transform.position;
            firstTargetSet = true;
        }
    }
    void FollowPath()
    {
        //make sure that there aren't any holes in the pathway. if there is, clear the path and set current node to nearest node
        CheckIncompletePath();
        if(path.Count > 0)
        {
            if(path[0] != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, target) < .1f)
                {
                    GoToNextTile();
                }
            }
        }
    }
    public Node FindNearestNode(Vector2 position)
    {
        Node foundNode = null;
        float minDistance = float.MaxValue;

        foreach(Node node in NodesInScene())
        {
            float currentDistance = Vector2.Distance(transform.position, node.transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                foundNode = node;
            }
        }
        return foundNode;
    }
    private Node[] NodesInScene()
    {
        return FindObjectsOfType<Node>();
    }

    void GoToNextTile()
    {
        int x = 0;
        if(path.Count > 0)
        {
            currentNode = path[x];
            path.RemoveAt(x);
        }
        if(path.Count > 0)
            {
                nextNode = path[x];

                //Don't delete. targetNode only exists to make sure that nextNode has actually changed since the last time we called GoToNextTile().
                if(targetNode == null || targetNode != nextNode)
                {
                    SetTarget();
                    targetNode = nextNode;
                }
            }
        //As long as it's been .1 seconds since you last wiped the path, wipe it now
        if (updateTimer > updateCooldown && path.Count > 0)
            {
                path.Clear();
                updateTimer = 0;
            }
    }
    private Node FindClosestConnectedNode()
    {
        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach(Node node in NodesInScene())
        {
            if(node.connections.Count > 0)
            {
                float currentDistance = Vector2.Distance(transform.position, node.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestNode = node;
                }
            }
            
        }
        return closestNode;
    }
    public GameObject FindClosestBuilding(Vector3 position)
    {
        float closestBuildingDistance = float.MaxValue;
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        if (buildings.Length > 0)
        {
            for(int i = 0; i < buildings.Length; i++)
            {
                float currentDistance = Vector3.Distance(this.transform.position, buildings[i].transform.position);

                if(currentDistance < closestBuildingDistance)
                {
                    closestBuilding = buildings[i];
                    closestBuildingDistance = currentDistance;
                }
            }
            return closestBuilding;
        }
        return null;
    }
    void CheckIncompletePath()
    {
        if(path.Count > 0)
        {
            foreach(Node node in path)
            {
                if(node == null)
                {
                    path.Clear();
                    currentNode = FindClosestConnectedNode();
                    myJob.jobToDo = false;
                    return;
                }
            }
        }
    }
    void SetTarget()
    {
        if(path[0].transform.CompareTag("InBuilding"))
        {
            target = path[0].transform.position;
        }
        else
        {
            target = new Vector3(path[0].transform.position.x + Random.Range(-.4f, .4f), 
            path[0].transform.position.y + Random.Range(-.4f, .4f), transform.position.z);
        }
    }
}
