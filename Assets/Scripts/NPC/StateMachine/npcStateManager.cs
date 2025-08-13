using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcStateManager : MonoBehaviour
{

    public npcBaseState currentState;
    public string currentStateStr;
    public npcWorkingState WorkingState = new npcWorkingState();
    public npcExitingScript ExitingState = new npcExitingScript();
    public npcGoingToJobState GoingToJobState = new npcGoingToJobState();
    public npcWaitingState WaitingState = new npcWaitingState();
    public npcGoingToQueueState GoingToQueueState = new npcGoingToQueueState();
    public npcInQueueState InQueueState = new npcInQueueState();

    public GameObject entrance;
    public GameObject exit;
    public LogicScript logic;
    public GridScript grid;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        entrance = GameObject.FindObjectOfType<EntranceScript>().gameObject;
        exit = GameObject.FindObjectOfType<ExitScript>().gameObject;
        logic = GameObject.FindObjectOfType<LogicScript>();
        grid = GameObject.FindObjectOfType<GridScript>();

        currentState = ExitingState;

        currentState.EnterState(this);

        currentStateStr = ExitingState.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(npcBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
        currentStateStr = currentState.ToString();
        if (state == ExitingState || state == GoingToJobState || state == GoingToQueueState)
        {
            animator.SetBool(name: "StandingStill", false);
        }
        else
        {
            animator.SetBool(name: "StandingStill", true);
        }
    }
}
