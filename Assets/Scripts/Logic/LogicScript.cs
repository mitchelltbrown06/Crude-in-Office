using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LogicScript : MonoBehaviour
{
    public List<GameObject> placedPaths;
    public List<GameObject> laserTagPlayers;
    public GridScript grid;
    public int entityLayerMask = (1 << 7);
    public int gridLayerMask = (1 << 6);
    public int pathlayerMask = (1 << 8);
    public int buildingLayerMask = (1 << 3);
    public List<Node> nodesInScene;
    public List<GameObject> npcs;
    public Dictionary<(Node start, Node end), List<Node>> generatedPaths;
    void Start()
    {
        grid = GameObject.FindObjectOfType<GridScript>();
        generatedPaths = new Dictionary<(Node start, Node end), List<Node>>();
    }
    public Node FindNearestNode(Vector2 position)
    {
        Node foundNode = null;
        float searchDistance = grid.tileSize / 2;
        while (foundNode == null)
        {
            RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(searchDistance, searchDistance), 0f, Vector2.up, 0f);
            float minDistance = float.MaxValue;

            foreach (RaycastHit2D collision in collisions)
            {
                if (collision.collider.GetComponent<Node>())
                {
                    float currentDistance = Vector2.Distance(position, collision.transform.position);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        foundNode = collision.collider.GetComponent<Node>();
                    }
                }
            }
            searchDistance = searchDistance * 2;
        }
        return foundNode;
    }
    public GameObject FindClosestTile(Vector3 position)
    {
        GameObject closeTile = null;
        float searchDistance = grid.tileSize / 2;
        while (closeTile == null)
        {
            RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(searchDistance, searchDistance), 0f, Vector2.up, 0f, gridLayerMask);
            float nearestDistance = float.MaxValue;
            foreach (RaycastHit2D collision in collisions)
            {
                float distance = Vector3.Distance(position, collision.transform.position);
                if (distance < nearestDistance)
                {
                    closeTile = collision.collider.gameObject;
                    nearestDistance = distance;
                }
            }
            searchDistance = searchDistance * 2;
        }
        return closeTile;
    }
    public GameObject FindClosestPath(Vector3 position)
    {
        float searchDistance = grid.tileSize / 2;
        GameObject closestPath = null;
        while (closestPath == null)
        {
            RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(searchDistance, searchDistance), 0f, Vector2.up, 0f, pathlayerMask);
            float nearestDistance = float.MaxValue;
            foreach (RaycastHit2D collision in collisions)
            {
                float distance = Vector3.Distance(position, collision.transform.position);
                if (distance < nearestDistance)
                {
                    closestPath = collision.collider.gameObject;
                    nearestDistance = distance;
                }
            }
            searchDistance = searchDistance * 2;
        }
        return closestPath;
    }
    public Node FindClosestConnectedNode(Vector3 position)
    {
        Node closestNode = null;
        float searchDistance = grid.tileSize / 2;
        while (closestNode == null)
        {
            RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(searchDistance, searchDistance), 0f, Vector2.up, 0f);
            float minDistance = float.MaxValue;

            foreach (RaycastHit2D collision in collisions)
            {
                if (collision.collider.GetComponent<Node>() && collision.collider.GetComponent<Node>().connections.Count > 0)
                {
                    float currentDistance = Vector2.Distance(position, collision.transform.position);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        closestNode = collision.collider.GetComponent<Node>();
                    }
                }
            }
            searchDistance = searchDistance * 2;
        }
        return closestNode;
    }
    public List<GameObject> FindPathsInRange(Vector3 position, float minDistance)
    {
        List<GameObject> pathsInRange = new List<GameObject>();
        RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(minDistance / 2, minDistance / 2), 0f, Vector2.up, 0f, pathlayerMask);
        foreach (RaycastHit2D collision in collisions)
        {
            pathsInRange.Add(collision.collider.gameObject);
        }
        return pathsInRange;
    }
    public List<Node> FindTilesInRange(Vector3 position, float minDistance)
    {
        List<Node> tilesInRange = new List<Node>();
        RaycastHit2D[] collisions = Physics2D.BoxCastAll(position, new Vector2(minDistance / 2, minDistance / 2), 0f, Vector2.up, 0f);
        foreach (RaycastHit2D collision in collisions)
        {
            if (collision.collider.GetComponent<Node>())
            {
                tilesInRange.Add(collision.collider.GetComponent<Node>());   
            }
        }
        return tilesInRange;
    }
    public bool WeightedCoinToss(float weight)
    {
        //you set a weight for this function between 0 and 1. Then, you generate a random float between 0 and 1. 
        // If the randomly generated value is less than the weight value, return true. Otherwise, return false
        //Essentially, your weight represents the percent chance that you will return true.
        float randomValue = Random.Range(0f,1f);
        if(randomValue < weight)
        {
            return true;
        }
        return false;
    }
}