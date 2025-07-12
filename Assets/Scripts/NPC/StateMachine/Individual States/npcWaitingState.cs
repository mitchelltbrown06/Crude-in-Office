using UnityEngine;

public class npcWaitingState : npcBaseState
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
        if(Vector2.Distance(npc.transform.position, myJob.waitingRoomNode.transform.position) > .01f)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, 
            myJob.waitingRoomNode.transform.position, myStats.speed * Time.deltaTime);
        }
    }
}
