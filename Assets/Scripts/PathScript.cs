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
        
        if(logic.placedPaths.Count > 1)
        {
            logic.FindNearestNode(logic.placedPaths[^1].transform.position).connections.Add(logic.FindNearestNode(logic.placedPaths[^2].transform.position));
            logic.FindNearestNode(logic.placedPaths[^2].transform.position).connections.Add(logic.FindNearestNode(logic.placedPaths[^1].transform.position));
            //if last path is at the entrance, spawn in an exit on this path
            if(Vector2.Distance(entrance.transform.position, logic.placedPaths[^2].transform.position) < .1f)
            {
                Instantiate(exit, transform.position, Quaternion.identity);
            }
            //if last path is not at the entrance, move the exit to this path
            else
            {
                FindObjectOfType<ExitScript>().GoToPath(transform.position);
            }
        }
    }
}
