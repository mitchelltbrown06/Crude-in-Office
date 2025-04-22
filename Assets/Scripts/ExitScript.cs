using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
