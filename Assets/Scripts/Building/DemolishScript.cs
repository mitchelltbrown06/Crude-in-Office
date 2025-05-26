using System.Collections;
using System.Collections.Generic;
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
            node.onBuilding = false;
        }
        Destroy(gameObject);
    }
}
