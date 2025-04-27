using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public GameObject exit;
    public GameObject npc;

    public bool exitFound = false;

    public float spawnCooldown;
    public float Timer;

    void Start()
    {
        FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
    }
    void Update()
    {
        if(FindObjectOfType<ExitScript>() && exitFound == false)
        {
            exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
            exitFound = true;
        }
        Timer += Time.deltaTime;
        if(Timer > spawnCooldown && exit != null && AStarManager.instance.GeneratePath(
            AStarManager.instance.FindNearestNode(transform.position), 
            AStarManager.instance.FindNearestNode(exit.transform.position)) != null)
        {
            Timer = 0;
            Instantiate(npc, transform.position, Quaternion.identity);
        }
    }
    public GameObject FindClosestTile(Vector3 position)
    {
        float nearestDistance = float.MaxValue;
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Grid");
        GameObject closeTile = null;

        if (Tiles.Length > 0)
        {
            for(int i = 0; i < Tiles.Length; i++)
            {
                float distance = Vector3.Distance(position, Tiles[i].transform.position);

                if(distance < nearestDistance)
                {
                    closeTile = Tiles[i];
                    nearestDistance = distance;
                }
            }
            if(closeTile != null)
            {
                return closeTile;
            }
        }
        return null;
    }
}