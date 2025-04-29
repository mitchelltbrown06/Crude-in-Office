using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeBidenScript : MonoBehaviour
{
    public BidenPathfinding pathfinding;

    public Rigidbody2D rb;

    public float speed;

    public npcController NPC;

    public Vector3 randomOffset;

    public Vector3 targetPosition;

    public LogicScript logic;

    public GridScript grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindObjectOfType<GridScript>();
        logic = GameObject.FindObjectOfType<LogicScript>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAtTargetPosition();
        pathfinding.FindClosestBuilding();
        ApproachPathfinder();
        CheckIfAtExit();
    }

    void ApproachPathfinder()
    {
        rb.velocity = (targetPosition - transform.position).normalized * speed;
    }
    public void UpdateTargetPosition()
    {
        if(NPC.jobToDo == true)
        {
            randomOffset = new Vector3(Random.Range(-grid.tileSize * .05f, grid.tileSize * .05f), Random.Range(-grid.tileSize * .05f, grid.tileSize * .05f), 0);
        }
        else
        {
            randomOffset = new Vector3(Random.Range(-grid.tileSize * .3f, grid.tileSize * .3f), Random.Range(-grid.tileSize * .3f, grid.tileSize * .3f), 0);
        }
        if(NPC.nextNode != null)
        {
            targetPosition = new Vector3(NPC.nextNode.transform.position.x + randomOffset.x, NPC.nextNode.transform.position.y + randomOffset.y, NPC.nextNode.transform.position.z);
        }
    }
    void CheckIfAtTargetPosition()
    {
        if(NPC.jobToDo == true)
        {
            if(Vector2.Distance(transform.position, targetPosition) < .05f)
            {
                NPC.GoToNextTile();
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, targetPosition) < .3f)
            {
                NPC.GoToNextTile();
            }
        }
    }
    void CheckIfAtExit()
    {
        if(Vector2.Distance(NPC.transform.position, NPC.exit.transform.position) < .1
            && Vector2.Distance(transform.position, targetPosition) < .3)
            {
                Destroy(gameObject);
            }
    }
}