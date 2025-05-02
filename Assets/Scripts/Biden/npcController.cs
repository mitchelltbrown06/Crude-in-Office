using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Node nextNode;
    public Node currentNode;
    public Node[] nodesInScene;
    public List<Node> path;

    public float speed = .5f;

    public GameObject exit;

    public GameObject entrance;

    public GridScript grid;

    public LogicScript logic;

    public float buildingCaptureDistance;

    public bool jobToDo = false;

    public BidenPathfinding npc;

    private Node jobNode;

    public JoeBidenScript npcFollowerMovement;

    void Start()
    {
        entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
        npc = GameObject.FindObjectOfType<BidenPathfinding>();
        npcFollowerMovement = GameObject.FindObjectOfType<JoeBidenScript>();

        buildingCaptureDistance = grid.tileSize * .9f;

        GoToNextTile();
    }
    void Update()
    {
        if(nodesInScene.Length == 0)
        {
            nodesInScene = FindObjectsOfType<Node>();
        }
        if(currentNode == null)
        {
            transform.position = FindClosestConnectedNode().transform.position;
            currentNode = FindClosestConnectedNode();
            jobToDo = false;
            
        }
        if(npc.closestBuilding != null && jobToDo == false)
        {
            FindJob(npc.closestBuilding.transform.Find("Door").gameObject);
        }
        if(exit != null && jobToDo == false)
        {
            CreatePath(exit.transform.position);
            FollowPath();
        }
        else if(exit != null && jobToDo == true && npc.closestBuilding != null)
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
    }
    void FollowPath()
    {
        if(path.Count > 0)
        {
            int x = 0;
            if(path[x] != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[x].transform.position,
                    speed * Time.deltaTime);
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
    public void GoToNextTile()
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
            }
        if(Vector2.Distance(transform.position, exit.transform.position) < grid.tileSize)
        {
            nextNode = currentNode;
        }
        npcFollowerMovement.UpdateTargetPosition();
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
}
