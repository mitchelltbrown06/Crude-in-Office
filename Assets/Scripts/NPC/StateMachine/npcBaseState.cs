using UnityEngine;

public abstract class npcBaseState
{
    public abstract void EnterState(npcStateManager npc);
    public abstract void UpdateState(npcStateManager npc);
}
