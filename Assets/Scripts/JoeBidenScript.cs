using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeBidenScript : MonoBehaviour
{

    /*
    public GameObject[] Buildings;
    public GameObject closestBuilding;    
    float distance;
    float nearestBuildingDistance = 10000000;
    Vector3 direction;
    public OilRigScript OilRigScript;
    public GameObject Player;
    */

    public BidenPathfinding pathfinding;

    public Rigidbody2D rb;

    public float speed;

    public float attackDistance;
    public float attackSpeed;
    private float attackTimer;

    public npcController NPC;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pathfinding.FindClosestBuilding();

        attackTimer += Time.deltaTime;
        if(pathfinding.nearestBuildingDistance < attackDistance && attackTimer > attackSpeed)
        {
            AttackBuilding();
        }
        if(pathfinding.nearestBuildingDistance > attackDistance)
        {
            ApproachPathfinder();
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    void ApproachPathfinder()
    {
        rb.velocity = (NPC.transform.position - transform.position).normalized * speed;
    }
    void AttackBuilding()
    {
        pathfinding.OilRigScript.DamageFromJoe();
        Debug.Log("Attack");
        attackTimer = 0;
    }
}
