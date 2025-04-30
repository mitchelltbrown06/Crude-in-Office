using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
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
    //This will make the exit path go to the last placed path every time thsi function is triggered (which is whenever a path is placed)
    public void GoToPath(Vector3 position)
    {
        FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = false;
        Debug.Log("going to " + position.ToString());
        transform.position = position;
        FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
    }
}
