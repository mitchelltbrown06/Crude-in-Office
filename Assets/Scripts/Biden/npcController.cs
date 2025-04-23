using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Node currentNode;
    public Node[] nodesInScene;
    public List<Node> path;

    public float speed = .5f;

    private bool onStartTile = false;

    public float UpdateCooldown = 1;
    public float UpdateTimer = 0;

    public GameObject exit;

    public GridScript grid;

    public LogicScript logic;

    public float buildingCaptureDistance;

    public bool jobToDo = false;

    public BidenPathfinding npc;

    public GameObject target;

    void Start()
    {
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
        npc = GameObject.FindObjectOfType<BidenPathfinding>();
        buildingCaptureDistance = grid.tileSize / 2;
        /*
        //need to find closest node and then teleport to it and set it to the current node
        if (!onStartTile)
        {
            transform.position = new Vector3(FindNearestNode(transform.position).transform.position.x, FindNearestNode(transform.position).transform.position.y, transform.position.z);
            currentNode = FindNearestNode(transform.position);
            onStartTile = true;
        }
        */
    }
    void Update()
    {
        if(Vector2.Distance(exit.transform.position, this.transform.parent.gameObject.transform.position) < logic.npcOffsetRange * 1.5)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        if(nodesInScene.Length == 0)
        {
            nodesInScene = FindObjectsOfType<Node>();
        }
        else
        {
            if (!onStartTile)
            {
                transform.position = new Vector3(FindNearestNode(transform.position).transform.position.x, FindNearestNode(transform.position).transform.position.y, transform.position.z);
                currentNode = FindNearestNode(transform.position);
                onStartTile = true;
            }
        }
        UpdateTimer += Time.deltaTime;
        if(onStartTile)
        {
            if(npc.closestBuilding != null)
            {
                FindJob(npc.closestBuilding.transform.Find("Door").gameObject);
            }
            if(exit != null && jobToDo == false)
            {
                CreatePath(exit.transform.position);
                target = exit;
                FollowPath();
            }
            else if(exit != null && jobToDo == true)
            {
                if(target != npc.closestBuilding)
                {
                    path.Clear();
                }
                CreatePathWithNode(npc.closestBuilding.transform.Find("Door").GetComponent<DoorScript>().openJob);
                target = npc.closestBuilding;
                FollowPath();
            }
        }
    }
    void CreatePath(Vector3 destination)
    {
        if(path.Count == 0)
        { 
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(destination));
        }
    }
    void CreatePathWithNode(Node node)
    {
        Debug.Log(node.transform.position);
        Debug.Log(currentNode != null);
        if(path.Count == 0)
        { 
            path = AStarManager.instance.GeneratePath(currentNode, node);
        }
    }
    void FollowPath()
    {
        if(path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, transform.position.z),
                speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform.position) < .1f && Vector2.Distance(transform.position, transform.parent.gameObject.transform.position) < .5f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
                if(UpdateTimer > UpdateCooldown)
                {
                    path.Clear();
                    UpdateTimer = 0;
                }
            }
        }
    }
    private Node FindNearestNode(Vector2 position)
    {
        Node foundNode = null;
        float minDistance = float.MaxValue;

        foreach(Node node in nodesInScene)
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

        if(Vector2.Distance(door.transform.position, npc.transform.position) < buildingCaptureDistance)
        {
            jobToDo = true;
        }
    }
}
