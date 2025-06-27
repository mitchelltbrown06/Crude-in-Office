using UnityEngine;

public class npcExitingScript : npcBaseState
{
    public override void EnterState(npcStateManager npc)
    {
        Debug.Log("hello from the exiting state");
    }
    public override void UpdateState(npcStateManager npc)
    {
        Debug.Log("updating from the exiting state");
    }
}
