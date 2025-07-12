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
        logic.FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
        FindObjectOfType<EntranceScript>().exit = gameObject;
    }
    //This will make the exit path go to the last placed path every time thsi function is triggered (which is whenever a path is placed)
    public void GoToPath(Vector3 position)
    {
        logic.FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = false;
        transform.position = position;
        logic.FindClosestTile(transform.position).GetComponent<Node>().onEntranceOrExit = true;
    }
}
