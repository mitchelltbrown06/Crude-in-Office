using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path;

    public JoeBidenScript player;
    private float speed = 3;

    void Start()
    {
        //need to find closest node and then teleport to it and set it to the current node
    }
    void Update()
    {
        CreatePath();
    }
    void Engage()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }
    void CreatePath()
    {
        if(path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, transform.position.z),
                speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform.position) < .1f /* && Vector2.Distance(transform.position, player.transform.position) < .5f*/)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }

}
