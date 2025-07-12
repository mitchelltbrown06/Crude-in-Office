using UnityEngine;

public class npcWorkingState : npcBaseState
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
        if(Vector2.Distance(npc.transform.position, myJob.jobNode.transform.position) > .01f)
        {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, 
            myJob.jobNode.transform.position, myStats.speed * Time.deltaTime);
        }
        if(myJob.jobToDo == false)
        {
            {
                npc.SwitchState(npc.ExitingState);
            }
        }
    }
}
