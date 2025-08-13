using UnityEngine;

public class npcWaitingState : npcBaseState
{
    private npcJob myJob;
    private npcStats myStats;
    private GlobalStats stats;
    public override void EnterState(npcStateManager npc)
    {
        myStats = npc.GetComponent<npcStats>();
        myJob = npc.GetComponent<npcJob>();
        stats = GameObject.FindGameObjectWithTag("Logic").GetComponent<GlobalStats>();
    }
    public override void UpdateState(npcStateManager npc)
    {
        if (Vector2.Distance(npc.transform.position, myJob.waitingRoomNode.transform.position) > .01f)
        {
            npc.animator.SetBool(name: "StandingStill", false);
            npc.transform.position = Vector3.MoveTowards(npc.transform.position,
            myJob.waitingRoomNode.transform.position, stats.npcSpeed * Time.deltaTime);
        }
        else
        {
            npc.animator.SetBool(name: "StandingStill", true);
        }
    }
}
