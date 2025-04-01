using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeBidenScript : MonoBehaviour
{
    public GameObject[] Buildings;
    public GameObject closestBuilding;
    public GameObject Player;
    float distance;
    float nearestDistance = 10000000;
    Vector3 direction;
    public OilRigScript OilRigScript;

    public Rigidbody2D rb;

    public float speed;

    public float attackDistance;
    public float attackSpeed;
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBuilding();

        attackTimer += Time.deltaTime;
        if(nearestDistance < attackDistance && attackTimer > attackSpeed)
        {
            AttackBuilding();
        }
        if(nearestDistance > attackDistance)
        {
        ApproachBuilding();
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }
    void FindClosestBuilding()
    {
        nearestDistance = 10000000;
        Buildings = GameObject.FindGameObjectsWithTag("Building");

        if (Buildings.Length > 0)
        {
            for(int i = 0; i < Buildings.Length; i++)
            {
                //Debug.Log("checking i: " + i.ToString());
                distance = Vector3.Distance(this.transform.position, Buildings[i].transform.position);
                //Debug.Log("distance: " + distance.ToString());

                if(distance < nearestDistance)
                {
                    closestBuilding = Buildings[i];
                    nearestDistance = distance;
                }
            }
        }
        else 
        {
        closestBuilding = Player;
        }

        OilRigScript = closestBuilding.GetComponent<OilRigScript>();
    }
    void ApproachBuilding()
    {
        direction = (closestBuilding.transform.position - this.transform.position).normalized;
        rb.velocity = direction * speed;
    }
    void AttackBuilding()
    {
        OilRigScript.DamageFromJoe();
        Debug.Log("Attack");
        attackTimer = 0;
    }
}
