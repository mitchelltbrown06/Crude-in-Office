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
        randomOffset = new Vector3(Random.Range(-grid.tileSize / 2, grid.tileSize / 2), Random.Range(-grid.tileSize / 2, grid.tileSize / 2), 0);
        logic = GameObject.FindObjectOfType<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAtTargetPosition();
        pathfinding.FindClosestBuilding();
        ApproachPathfinder();
    }

    void ApproachPathfinder()
    {
        rb.velocity = (targetPosition - transform.position).normalized * speed;
    }
    public void UpdateTargetPosition()
    {
        targetPosition = new Vector3(NPC.nextNode.transform.position.x + randomOffset.x, NPC.nextNode.transform.position.y + randomOffset.y, NPC.nextNode.transform.position.z);
    }
    void CheckIfAtTargetPosition()
    {
        /*
        if(NPC.nextNode != null)
        {
            if(Vector2.Distance(NPC.transform.position, NPC.nextNode.transform.position) < .1f
                && Vector2.Distance(transform.position, targetPosition) < .1f)
            {
                NPC.GoToNextTile();
            }
            else if(Vector2.Distance(NPC.transform.position, NPC.entrance.transform.position) < .1f)
            {
                NPC.GoToNextTile();
            }
        }
        else if(NPC.currentNode != null && Vector2.Distance(targetPosition.transform.position, transform.position) < .1f)
        {
            NPC.GoToNextTile();
        }
        */
        if(targetPosition == null)
        {
            NPC.GoToNextTile();
            targetPosition = transform.position;
        }
        if(Vector2.Distance(transform.position, targetPosition) < .1f)
        {
            NPC.GoToNextTile();
        }
    }
}