using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcStateManager : MonoBehaviour
{

    npcBaseState currentState;
    npcSearchingState SearchingState = new npcSearchingState();
    npcWorkingState WorkingState = new npcWorkingState();
    npcExitingScript ExitingState = new npcExitingScript();
    npcGoingToJobState GoingToJobState = new npcGoingToJobState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = ExitingState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
