using UnityEngine;

public class npcInQueueState : npcBaseState
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
        if (Vector2.Distance(myJob.transform.position, myJob.queueNode.transform.position) > .03f)
        {
            npc.animator.SetBool(name: "StandingStill", false);
            npc.transform.position = Vector3.MoveTowards(npc.transform.position,
            myJob.queueNode.transform.position, stats.npcSpeed * Time.deltaTime);
        }
        else
        {
            npc.animator.SetBool(name: "StandingStill", true);
        }
    }
}
