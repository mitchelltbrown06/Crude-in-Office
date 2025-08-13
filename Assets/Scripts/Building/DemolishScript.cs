using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemolishScript : MonoBehaviour
{
    private LogicScript logic;
    private GridScript grid;

    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();
    }
    public void Demolish()
    {
        foreach(Node node in logic.FindTilesInRange(transform.position, grid.tileSize))
        {
            RaycastHit2D hit = Physics2D.Raycast(node.transform.position, Vector2.up, 1f, logic.buildingLayerMask);
            if (hit && hit.collider.gameObject == gameObject)
            {
                node.onBuilding = false;
            }
        }
        foreach (Node node in GetComponentsInChildren<Node>())
        {
            logic.nodesInScene.Remove(node);
        }
        Destroy(gameObject);
    }
}
