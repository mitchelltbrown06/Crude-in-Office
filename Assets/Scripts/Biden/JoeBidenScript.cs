using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeBidenScript : MonoBehaviour
{
    public BidenPathfinding pathfinding;

    public Rigidbody2D rb;

    public float speed;

    public float attackDistance;
    public float attackSpeed;
    private float attackTimer;

    public npcController NPC;

    private Vector3 randomOffset;
    private Vector3 targetPosition;

    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        randomOffset = new Vector3(Random.Range(-logic.npcOffsetRange, logic.npcOffsetRange), Random.Range(-logic.npcOffsetRange, logic.npcOffsetRange), Random.Range(-logic.npcOffsetRange, logic.npcOffsetRange));
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(NPC.transform.position.x + randomOffset.x, NPC.transform.position.y + randomOffset.y, NPC.transform.position.z + randomOffset.z);

        pathfinding.FindClosestBuilding();

        attackTimer += Time.deltaTime;
        ApproachPathfinder();
    }

    void ApproachPathfinder()
    {
        rb.velocity = (targetPosition - transform.position).normalized * speed;
    }
}
