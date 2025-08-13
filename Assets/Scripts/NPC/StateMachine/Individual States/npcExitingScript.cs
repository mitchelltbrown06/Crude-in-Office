using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class npcExitingScript : npcBaseState
{
    public GridScript grid;
    public LogicScript logic;
    public GlobalStats stats;

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

    public bool updateNeeded;

    public override void EnterState(npcStateManager npc)
    {
        myStats = npc.GetComponent<npcStats>();
        myJob = npc.GetComponent<npcJob>();
        entrance = npc.entrance;
        exit = npc.exit;
        logic = npc.logic;
        grid = npc.grid;
        stats = npc.logic.GetComponent<GlobalStats>();

        //if you're not on a path (i.e. the path closest to you isn't at the same position as the tile closest to you) then set current node to the nearest connected node
        if (logic.FindClosestTile(npc.transform.position).GetComponent<Node>().onBuilding == true)
        {
            float minDistance = float.MaxValue;
            Node nearestNode = null;
            foreach (Node node in myJob.jobNode.transform.root.GetComponentsInChildren<Node>())
            {
                if (Vector2.Distance(npc.transform.position, node.transform.position) < minDistance)
                {
                    nearestNode = node;
                    minDistance = Vector2.Distance(npc.transform.position, node.transform.position);
                }
            }
            currentNode = nearestNode;
        }
        //otherwise, you are on a path, so you should set your current node the nearest path
        else
        {
            currentNode = logic.FindClosestConnectedNode(logic.FindClosestPath(npc.transform.position).transform.position);
        }

        nextNode = null;
        jobNode = null;
        myJob.jobNode = null;
        if (path != null && path.Count > 0)
        {
            path.Clear();
        }
        if (path == null || path.Count == 0 && currentNode.connections.Count > 0)
        {
            CreatePath(logic.FindNearestNode(exit.transform.position));
        }
    }
    public override void UpdateState(npcStateManager npc)
    {
        //if you don't have a current node, set the closest node to the current node
        if (currentNode == null)
        {
            currentNode = logic.FindClosestConnectedNode(npc.transform.position);
        }
        if (updateNeeded == true && path == null || path.Count == 0 && currentNode.connections.Count > 0)
        {
            CreatePath(logic.FindNearestNode(exit.transform.position));
            updateNeeded = false;
        }
        if (path != null && path.Count > 0)
        {
            FollowPath(npc);
        }
        //make sure that there aren't any holes in the pathway. if there is, clear the path and set current node to nearest node
        CheckIncompletePath(npc); 
    }
    public void CreatePath(Node destination)
    {
        if (logic.generatedPaths.TryGetValue((currentNode, destination), out List<Node> cachedPath))
        {
            path = new List<Node>(cachedPath);
        }
        else
        {
            path = AStarManager.instance.GeneratePath(currentNode, destination);
            List<Node> pathToCache = new List<Node>(path);
            logic.generatedPaths[(currentNode, destination)] = pathToCache;
        }
        nextNode = path[0];
        if (nextNode.CompareTag("InBuilding") && positionOffset != 0)
        {
            positionOffset = 0;
        }
        else if (!nextNode.CompareTag("InBuilding") && positionOffset == 0)
        {
            positionOffset = Random.Range(-.2f, .2f);
        }
    }
    void FollowPath(npcStateManager npc)
    {
        if (nextNode != null)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, new Vector3(nextNode.transform.position.x + positionOffset, nextNode.transform.position.y + positionOffset, 0), stats.npcSpeed * Time.deltaTime);

            if (nextNode.CompareTag("InBuilding") && Vector2.Distance(npc.transform.position, nextNode.transform.position) < .01f)
            {
                GoToNextTile();
            }
            else if (!nextNode.CompareTag("InBuilding") && Vector2.Distance(npc.transform.position, nextNode.transform.position) < .3)
            {
                GoToNextTile();
            }
        }
        else if (path.Count > 0)
        {
            nextNode = path[0];
        }
    }
    void GoToNextTile()
    {
        int x = 0;
        if (path.Count > 0)
        {
            currentNode = path[x];
            path.RemoveAt(x);
        }
        if (path.Count > 0)
        {
            nextNode = path[x];
        }
        if (nextNode != null && nextNode.CompareTag("InBuilding") && positionOffset != 0)
        {
            positionOffset = 0;
        }
        else if (nextNode != null && !nextNode.CompareTag("InBuilding") && positionOffset == 0)
        {
            positionOffset = UnityEngine.Random.Range(-.2f, .2f);
        }
    }
    void CheckIncompletePath(npcStateManager npc)
    {
        if(path.Count > 0)
        {
            foreach(Node node in path)
            {
                if(node == null || node.connections.Count == 0)
                {

                    currentNode = logic.FindClosestConnectedNode(npc.transform.position);
                    myJob.jobToDo = false;
                    path.Remove(node);
                    return;
                }
            }
        }
    }
}
