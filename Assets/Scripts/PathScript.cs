using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public Node[] nodesInScene;
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        nodesInScene = FindObjectsOfType<Node>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if(logic.lastPath == null)
        {
            Debug.Log("setting last path");
            logic.lastPath = FindNearestNode(transform.position);
        }
        else
        {
            Debug.Log("lastpath isn't null!");
            FindNearestNode(transform.position).connections.Add(logic.lastPath);
            logic.lastPath.connections.Add(FindNearestNode(transform.position));
        }
        //FindNeighbors();
        logic.lastPath = FindNearestNode(transform.position);
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
}
