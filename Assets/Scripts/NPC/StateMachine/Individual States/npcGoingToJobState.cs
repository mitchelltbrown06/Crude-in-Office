using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcGoingToJobState : npcBaseState
{
    public LogicScript logic;

    public List<Node> path;
    public Node currentNode;
    public Node nextNode;

    private npcJob myJob;
    private npcStats myStats;

    public float updateCooldown;
    public float updateTimer;

    public float positionOffset;

    public override void EnterState(npcStateManager npc)
    {
        myStats = npc.GetComponent<npcStats>();
        myJob = npc.GetComponent<npcJob>();
        logic = npc.logic;

        //if you're not on a path (i.e. the path closest to you isn't at the same position as the tile closest to you) then set current node to the nearest connected node
        if (logic.FindClosestPath(npc.transform.position) == null || Vector2.Distance(logic.FindClosestPath(npc.transform.position).transform.position, logic.FindClosestTile(npc.transform.position).transform.position) > .1f)
        {
            currentNode = logic.FindClosestConnectedNode(npc.transform.position);
        }
        //otherwise, you are on a path, so you should set your current node the nearest path
        else
        {
            currentNode = logic.FindClosestConnectedNode(logic.FindClosestPath(npc.transform.position).transform.position);
        }

        nextNode = null;
        if (path != null && path.Count > 0)
        {
            path.Clear();
        }
        if (path == null || path.Count == 0 && currentNode.connections.Count > 0)
        {
            CreatePath(myJob.jobNode.GetComponent<Node>());
            if (nextNode != null && nextNode.CompareTag("InBuilding") && positionOffset != 0)
            {
                positionOffset = 0;
            }
            else if (nextNode != null && !nextNode.CompareTag("InBuilding") && positionOffset == 0)
            {
                positionOffset = Random.Range(-.2f, .2f);
            }
        }
    }
    public override void UpdateState(npcStateManager npc)
    {
        if (myJob.jobNode != null)
        {
            if (myJob.jobToDo == false)
            {
                npc.SwitchState(npc.ExitingState);
            }
            //if you don't have a current node, set the closest node to the current node
            if (currentNode == null)
            {
                currentNode = logic.FindClosestConnectedNode(npc.transform.position);
            }
            FollowPath(npc);
        }
        else
        {
            npc.SwitchState(npc.ExitingState);
        }
    }
    public void CreatePath(Node destination)
    {
        path = AStarManager.instance.GeneratePath(currentNode, destination);
        nextNode = path[0];
    }
    void FollowPath(npcStateManager npc)
    {
        if(nextNode != null)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, new Vector3(nextNode.transform.position.x + positionOffset, nextNode.transform.position.y + positionOffset, 0), npc.logic.GetComponent<GlobalStats>().npcSpeed * Time.deltaTime);

            if(nextNode.CompareTag("InBuilding") && Vector2.Distance(npc.transform.position, nextNode.transform.position) < .1f)
            {
                GoToNextTile();
            }
            else if(!nextNode.CompareTag("InBuilding") && Vector2.Distance(npc.transform.position, nextNode.transform.position) < .3)
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
        if (path.Count > 0)
        {
            currentNode = path[x];
            path.RemoveAt(x);
        }
        if (path.Count > 0)
        {
            nextNode = path[x];
        }
        //update position offset
        if (nextNode != null && nextNode.CompareTag("InBuilding") && positionOffset != 0)
        {
            positionOffset = 0;
        }
        else if (nextNode != null && !nextNode.CompareTag("InBuilding") && positionOffset == 0)
        {
            positionOffset = Random.Range(-.2f, .2f);
        }
    }
}

