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

    private npcJob myJob;
    private npcStats myStats;

    public GameObject exit;
    public GameObject entrance;

    public float buildingCaptureDistance;

    public float updateCooldown;
    public float updateTimer;

    public float positionOffset;

    void Start()
    {
        myStats = GetComponent<npcStats>();
        myJob = GetComponent<npcJob>();
        entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();

        buildingCaptureDistance = grid.tileSize * .9f;
    }
    void Update()
    {
        if(nextNode != null && nextNode.CompareTag("InBuilding") && positionOffset != 0)
        {
            positionOffset = 0;
        }
        else if(nextNode != null && !nextNode.CompareTag("InBuilding") && positionOffset == 0)
        {
            positionOffset = Random.Range(-.2f, .2f);
        }
        
        if(nextNode != null && nextNode.connections.Count == 0)
        {
            GetComponent<KillScript>().Kill(); 
        }
        if(nextNode == null && Vector2.Distance(transform.position, entrance.transform.position) > .1f)
        {
            GetComponent<KillScript>().Kill();
        }

        //increase the update timer that determines if the path should be cleared and regenerated
        updateTimer += Time.deltaTime;

        //If at the exit, die
        if(Vector2.Distance(logic.FindNearestNode(transform.position).transform.position, exit.transform.position) < .1f)
        {
            Destroy(gameObject);
        }
        
        //if you don't have a current node, set the closest node to the current node
        if(currentNode == null)
        {
            currentNode = FindClosestConnectedNode();
        }

        if(exit != null)
        {
            if(myJob.jobToDo == false)
            {
                CreatePath(exit.transform.position);
            }
            else if(jobNode != null)
            {
                if(path.Count > 0 && path[^1] != jobNode)
                {
                    GoToNextTile();
                }
                CreatePath(jobNode.transform.position);
            }
            FollowPath();
        }
    }

    public void CreatePath(Vector3 destination)
    {
        if(path.Count == 0 && currentNode.connections.Count > 0)
        { 
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(destination));
            nextNode = path[0];
        }
    }
    void FollowPath()
    {
        //make sure that there aren't any holes in the pathway. if there is, clear the path and set current node to nearest node
        CheckIncompletePath();
        if(nextNode != null)
        {
            /*
            if(targetNode != null && Vector2.Distance(target, targetNode.transform.position) > grid.tileSize * .5f)
            {
                SetTarget();
            }
            if(nextNode.CompareTag("InBuilding") || nextNode.onEntranceOrExit == true || target == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, myStats.speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, nextNode.transform.position) < .1f)
                {
                    GoToNextTile();
                } 
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, myStats.speed * Time.deltaTime);
                if(Vector2.Distance(transform.position, target) < .1f)
                {
                    GoToNextTile();
                }
            }
            */
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextNode.transform.position.x + positionOffset, nextNode.transform.position.y + positionOffset, 0), myStats.speed * Time.deltaTime);
            if(nextNode.CompareTag("InBuilding") && Vector2.Distance(transform.position, nextNode.transform.position) < .1f)
            {
                GoToNextTile();
            }
            else if(!nextNode.CompareTag("InBuilding") && Vector2.Distance(transform.position, nextNode.transform.position) < .3)
            {
                GoToNextTile();
            } 
        }
        else if(path.Count > 0)
        {
            nextNode = path[0];
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
        }
        //As long as it's been .1 seconds since you last wiped the path, wipe it now
        if (updateTimer > updateCooldown && path.Count > 0)
        {
            path.Clear();
            updateTimer = 0;
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
}
