using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    public GameObject tombstone;
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
    }

    void Update()
    {
        if (logic.FindClosestTile(transform.position).GetComponent<Node>().onPath == false
        && logic.FindClosestTile(transform.position).GetComponent<Node>().onBuilding == false)
        {
            Kill();
        }

        //If at the exit, die
        if (Vector2.Distance(logic.FindNearestNode(transform.position).transform.position, GameObject.FindObjectOfType<ExitScript>().transform.position) < .1f)
        {
            Exit();
        }
    }
    public void Kill()
    {
        if (GetComponent<npcStats>().laserTag == true)
        {
            logic.laserTagPlayers.Remove(gameObject);
        }
        logic.npcs.Remove(gameObject);
        Instantiate(tombstone, transform.position, transform.rotation);
        Destroy(transform.root.gameObject);
    }
    void Exit()
    {
        logic.npcs.Remove(gameObject);
        Destroy(transform.root.gameObject);
    }
}
