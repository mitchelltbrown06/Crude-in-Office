using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vance : MonoBehaviour
{
    public GameObject[] enemies;
    public float shootingRange = 3;

    public float shotCooldown = 1;
    public float shotTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ShootEnemy();
        shotTimer += Time.deltaTime;
    }
    void ShootEnemy()
    {
        foreach(GameObject enemy in enemies)
        {
            if(Vector2.Distance(enemy.transform.position, transform.position) < shootingRange && shotTimer > shotCooldown)
            {
                enemy.GetComponent<GetShot>().TakeDamage();
                shotTimer = 0;
            }
        }
    }
}
