using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Node currentNode;
    public Node[] nodesInScene;
    public List<Node> path;

    public BidenPathfinding joeBiden;
    public float speed = .5f;

    private bool onStartTile = false;

    public float UpdateCooldown = 1;
    public float UpdateTimer = 0;

    void Start()
    {
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
            if(joeBiden.nodeAtBuilding != null)
            {
                Engage();
                CreatePath();
            }
        }
    }
    void Engage()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(joeBiden.nodeAtBuilding.transform.position));
            if(path.Count == 0)
            {
                path = AStarManager.instance.GenerateWallPath(currentNode, AStarManager.instance.FindNearestNode(joeBiden.nodeAtBuilding.transform.position));
            }
        }
    }
    void CreatePath()
    {
        if(path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, transform.position.z),
                speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform.position) < .1f && Vector2.Distance(transform.position, joeBiden.transform.position) < .5f)
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
}
