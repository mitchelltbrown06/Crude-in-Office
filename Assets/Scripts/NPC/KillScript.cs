using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    public void Kill()
    {
        if(GetComponent<npcStats>().laserTag == true)
        {
            GameObject.FindObjectOfType<LogicScript>().laserTagPlayers.Remove(gameObject);
        }
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
