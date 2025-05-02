using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{

    public Node lastPath;
    
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
    public GameObject FindClosestPath(Vector3 position)
    {
        float nearestDistance = float.MaxValue;
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        GameObject path = null;

        if (paths.Length > 0)
        {
            for(int i = 0; i < paths.Length; i++)
            {
                float distance = Vector3.Distance(position, paths[i].transform.position);

                if(distance < nearestDistance)
                {
                    path = paths[i];
                    nearestDistance = distance;
                }
            }
            return path;
        }
        return null;
    }
}