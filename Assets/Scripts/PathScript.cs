using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public Node[] nodesInScene;
    public LogicScript logic;
    public EntranceScript entrance;
    public GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        nodesInScene = FindObjectsOfType<Node>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        entrance = GameObject.FindObjectOfType<EntranceScript>();
        
        if(logic.lastPath != null)
        {
            FindNearestNode(transform.position).connections.Add(logic.lastPath);
            logic.lastPath.connections.Add(FindNearestNode(transform.position));
            //if last path is at the entrance, spawn in an exit on this path
            if(Vector2.Distance(entrance.transform.position, logic.lastPath.transform.position) < .1f)
            {
                Instantiate(exit, transform.position, Quaternion.identity);
                logic.lastPath = FindNearestNode(transform.position);
            }
            //if last path is not at the entrance, move the exit to this path
            else
            {
                FindObjectOfType<ExitScript>().GoToPath(transform.position);
                logic.lastPath = FindNearestNode(transform.position);
            }
        }
        else
        {
            logic.lastPath = FindNearestNode(transform.position);
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
}
