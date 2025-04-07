using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private float nearestDistance;
    private float distance;
    public Node closestNode;
    public Node[] nodes;
    public bool foundClosestNode = false;

    public BidenPathfinding joeBiden;


    private void Update()
    {
        if (!foundClosestNode)
        {
            FindClosestNode();
            currentNode = closestNode;
            foundClosestNode = true;
        }
        CreatePath();
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, transform.position.z), 3 * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform.position) < 0.1f && Vector2.Distance(joeBiden.transform.position, transform.position) < .3f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
        else
        {
            nodes = FindObjectsOfType<Node>();
            while(path == null || path.Count == 0)
            {
                path = AStarManager.instance.GeneratePath(currentNode, joeBiden.nodeAtBuilding);
            }
        }
    }
    void FindClosestNode()
    {
        nodes = FindObjectsOfType<Node>();
        nearestDistance = float.MaxValue;

        if (nodes.Length > 0)
        {
            for(int i = 0; i < nodes.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector2.Distance(transform.position, nodes[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(distance < nearestDistance)
                {
                    closestNode = nodes[i];
                    nearestDistance = distance;
                }
            }
        }
    }
}
