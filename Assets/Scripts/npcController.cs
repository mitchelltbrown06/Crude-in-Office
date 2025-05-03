using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public GridScript grid;
    public LogicScript logic;

    public List<Node> path;
    public Node[] nodesInScene;
    public Node currentNode;
    public Node nextNode;
    private Node jobNode;
    private Node targetNode;

    public Vector3 target;

    public GameObject closestBuilding;

    public GameObject exit;
    public GameObject entrance;

    public float speed;

    public float buildingCaptureDistance;
    
    public bool jobToDo = false;
    public bool firstTargetSet = false;

    public float updateCooldown;
    public float updateTimer;

    void Start()
    {
        entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();

        buildingCaptureDistance = grid.tileSize * .9f;
    }
    void Update()
    {
        //make sure that there aren't any holes in the pathway. if there is, clear the path and set current node to nearest node
        CheckIncompletePath();

        //increase the update timer that determines if the path should be cleared and regenerated
        updateTimer += Time.deltaTime;

        //If at the exit, die
        if(Vector2.Distance(transform.position, target) < .1f && Vector2.Distance(nextNode.transform.position, exit.transform.position) < .1f)
        {
            Destroy(gameObject);
        }
        
        //find the closest building
        closestBuilding = FindClosestBuilding(transform.position);

        //if there aren't any nodes recognized, update the list of nodes
        if(nodesInScene.Length == 0)
        {
            nodesInScene = FindObjectsOfType<Node>();
        }

        //if you don't have a current node, teleport to the closest node and set that to current node
        if(currentNode == null)
        {
            transform.position = FindClosestConnectedNode().transform.position;
            currentNode = FindClosestConnectedNode();
            jobToDo = false;
        }

        //If you don't have a job, look for one
        if(closestBuilding != null && jobToDo == false)
        {
            FindJob(closestBuilding.transform.Find("Door").gameObject);
        }

        //if theres an exit and you don't have a job, make a path to the exit and follow it
        if(exit != null && jobToDo == false)
        {
            CreatePath(exit.transform.position);
            FollowPath();
        }

        //if you do have a job, create a path to the job and follow it
        else if(exit != null && jobToDo == true && closestBuilding != null)
        {
            if(path.Count == 0)
            { 
                path = AStarManager.instance.GeneratePath(nextNode, AStarManager.instance.FindNearestNode(jobNode.transform.position));
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

    void FindJob(GameObject door)
    {
        if(nextNode != null)
        {
            if(Vector2.Distance(door.transform.position, nextNode.transform.position) < buildingCaptureDistance
            && door.GetComponent<DoorScript>().openJob != null
            && !door.GetComponent<DoorScript>().rejectionList.Contains(gameObject))
            {
                //clear your path so you can make a new one
                path.Clear();
                //set jobNode to the open job that you found at the building
                jobNode = door.GetComponent<DoorScript>().openJob;
                //JobToDo is now true because you have a job
                jobToDo = true;
                //Set jobfilled on your jobNode to true so that other npcs don't take your job.
                door.GetComponent<DoorScript>().openJob.gameObject.GetComponent<JobScript>().JobFilled();
                //Set employee on jobNode to this gameObject
                jobNode.gameObject.GetComponent<JobScript>().employeeCandidates.Add(gameObject);
            }
        }
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
                //Debug.Log("checking i: " + i.ToString());
                float currentDistance = Vector3.Distance(this.transform.position, buildings[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

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
                    jobToDo = false;
                    return;
                }
            }
        }
    }
    void SetTarget()
    {
        Debug.Log("target");
        //target = path[0].transform.position;
        if(path[0].transform.CompareTag("InBuilding"))
        {
            target = path[0].transform.position;
        }
        else
        {
            target = new Vector3(path[0].transform.position.x + Random.Range(-.4f, .4f), path[0].transform.position.y + Random.Range(-.4f, .4f), transform.position.z);
        }
    }
}
