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

    public bool foundTile = false;
    // Start is called before the first frame update
    void Start()
    {
        foundTile = false;
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
            ApproachBuilding();
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    void ApproachBuilding()
    {
        //rb.velocity = pathfinding.nextDirection * speed;
        if(!foundTile)
        {
            rb.velocity = (pathfinding.closestTile.transform.position - transform.position).normalized * speed;
            if(pathfinding.nearestGridDistance > .02)
            {
                foundTile = true;
            }
        }
        else
        {
            //rb.velocity = (pathfinding.nextTile.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log("Highlighted Tile: " + pathfinding.highlightedTile.transform.position);
            Debug.Log("next tile: " + pathfinding.nextTile.transform.position);
        }
    }
    void AttackBuilding()
    {
        pathfinding.OilRigScript.DamageFromJoe();
        Debug.Log("Attack");
        attackTimer = 0;
    }
}
