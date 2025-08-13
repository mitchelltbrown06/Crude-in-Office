using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueNode : MonoBehaviour
{
    public GameObject employee;
    Coroutine move;
    public float moveRate;
    private Vector2 localScale;
    private Vector3 position;
    private LogicScript logic;
    void Start()
    {
        logic = GameObject.FindObjectOfType<LogicScript>();
        move = StartCoroutine(MoveNode());
        localScale = transform.parent.localScale;
        position = transform.parent.transform.position;
        moveRate = GetComponentInParent<QueueScript>().moveRate;
        transform.localPosition = new Vector3(Random.Range(transform.parent.localScale.x / 2, -transform.parent.localScale.x / 2),
                                        Random.Range(transform.parent.localScale.y / 2, -transform.parent.localScale.y / 2),
                                        0);
    }
    void Update()
    {
        if (employee == null || employee.GetComponent<npcStateManager>().currentState == employee.GetComponent<npcStateManager>().WorkingState)
        {
            DeleteNode();
        }
    }
    public void DeleteNode()
    {
        foreach (Node node in GetComponent<Node>().connections)
        {
            node.connections.Remove(GetComponent<Node>());
        }
        logic.nodesInScene.Remove(GetComponent<Node>());
        Destroy(gameObject);
    }
    IEnumerator MoveNode()
    {
        yield return new WaitForSeconds(moveRate);
        transform.localPosition = new Vector3(Random.Range(localScale.x / 2, -localScale.x / 2), Random.Range(localScale.y / 2, -localScale.y / 2), 0);
        moveRate = GetComponentInParent<QueueScript>().moveRate * Random.Range(.3f, 1.5f);
        move = StartCoroutine(MoveNode());
    }
}
