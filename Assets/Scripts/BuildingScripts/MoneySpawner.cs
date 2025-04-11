using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    public GameObject Coin;
    public float printSpeed;
    private float Timer;
    public float coinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCoin();
    }
    void SpawnCoin() 
    {
        Timer += Time.deltaTime;
        if (Timer > printSpeed)
        {
            GameObject TrumpCoin = Instantiate(Coin, transform.position, transform.rotation);
            TrumpCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)) * coinSpeed;

            Timer = 0;
        }
    }
}
