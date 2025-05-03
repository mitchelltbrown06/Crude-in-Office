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
    public Node FindClosestConnectedNode(Vector3 position)
    {
        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach(Node node in NodesInScene())
        {
            if(node.connections.Count > 0)
            {
                float currentDistance = Vector2.Distance(position, node.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestNode = node;
                }
            }
            
        }
        return closestNode;
    }
    public GameObject FindClosestBuilding(Vector3 position)
    {
        float closestBuildingDistance = float.MaxValue;
        GameObject closestBuilding = null;
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        if (buildings.Length > 0)
        {
            for(int i = 0; i < buildings.Length; i++)
            {
                float currentDistance = Vector3.Distance(this.transform.position, buildings[i].transform.position);

                if(currentDistance < closestBuildingDistance)
                {
                    closestBuilding = buildings[i];
                    closestBuildingDistance = currentDistance;
                }
            }
            return closestBuilding;
        }
        return null;
    }
    public Node[] NodesInScene()
    {
        return FindObjectsOfType<Node>();
    }
}