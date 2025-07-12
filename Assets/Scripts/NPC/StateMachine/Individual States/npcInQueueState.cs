using UnityEngine;

public class npcInQueueState : npcBaseState
{
    private npcJob myJob;
    private npcStats myStats;
    public override void EnterState(npcStateManager npc)
    {
        myStats = npc.GetComponent<npcStats>();
        myJob = npc.GetComponent<npcJob>();
    }
    public override void UpdateState(npcStateManager npc)
    {
        if (Vector2.Distance(myJob.transform.position, myJob.queueNode.transform.position) > .03f)
        {
            Debug.Log("MOVING");
            npc.transform.position = Vector3.MoveTowards(npc.transform.position,
            myJob.queueNode.transform.position, myStats.speed * Time.deltaTime);
        }
    }
}
