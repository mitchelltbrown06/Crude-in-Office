using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcGoingToQueueState : npcBaseState
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
        if (logic.FindClosestPath(myJob.transform.position) == null || Vector2.Distance(logic.FindClosestPath(myJob.transform.position).transform.position, logic.FindClosestTile(npc.transform.position).transform.position) > .1f)
        {
            currentNode = logic.FindClosestConnectedNode(myJob.transform.position);
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
        //otherwise, create a path to the job node and follow it
        
    }
    public override void UpdateState(npcStateManager npc)
    {
        //make sure that there aren't any holes in the pathway. if there is, clear the path and set current node to nearest node
        //CheckIncompletePath(npc);
        if (myJob.queueNode != null)
        {
            if (path == null || path.Count == 0 && currentNode.connections.Count > 0)
            {
                CreatePath(myJob.queueNode.GetComponent<Node>());
                if (nextNode != null && nextNode.CompareTag("InBuilding") && positionOffset != 0)
                {
                    positionOffset = 0;
                }
                else if (nextNode != null && !nextNode.CompareTag("InBuilding") && positionOffset == 0)
                {
                    positionOffset = Random.Range(-.2f, .2f);
                }
            }
            //if you don't have a current node, set the closest node to the current node
            if (currentNode == null)
            {
                currentNode = logic.FindClosestConnectedNode(npc.transform.position);
            }
            //if you're standing on the job node, switch to working state
            if (nextNode == myJob.queueNode)
            {
                npc.SwitchState(npc.InQueueState);
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
        if (path != null && path.Count > 0)
        {
            nextNode = path[0];
        }
    }
    void FollowPath(npcStateManager npc)
    {
        if(nextNode != null)
        {
            myJob.transform.position = Vector3.MoveTowards(myJob.transform.position, new Vector3(nextNode.transform.position.x + positionOffset, nextNode.transform.position.y + positionOffset, 0), myStats.speed * Time.deltaTime);

            if(nextNode.CompareTag("InBuilding") && Vector2.Distance(myJob.transform.position, nextNode.transform.position) < .1f)
            {
                GoToNextTile(npc);
            }
            else if(!nextNode.CompareTag("InBuilding") && Vector2.Distance(myJob.transform.position, nextNode.transform.position) < .3)
            {
                GoToNextTile(npc);
            } 
        }
        else if(path != null && path.Count > 0)
        {
            nextNode = path[0];
        }
    }
    void GoToNextTile(npcStateManager npc)
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
            positionOffset = Random.Range(-.2f, .2f);
        }
    }
    void CheckIncompletePath(npcStateManager npc)
    {
        if(path != null && path.Count > 0)
        {
            foreach(Node node in path)
            {
                if(node == null || node.connections.Count == 0)
                {

                    npc.SwitchState(npc.ExitingState);
                }
            }
        }
    }
}
