using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDrivenBrain : EnermyControler
{
    private float healthLevel;
    private float staminaLevel;
    public float attackRange;
    public float minimumStamina;

    public bool wait;
    public float waitTime = 3;

    public enum AIStates { Idle, Chase, Attack, Hold, SearchArea, Return, Death };
    public FSM<AIStates> stateMachine;
    protected float thinkInterval = 0.4f;

    void Start()
    {
        base.Start();
        Debug.Log("SDB running");
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrentState.Act();
    }

    protected IEnumerator Think()
    {
        yield return new WaitForSeconds(thinkInterval);
        healthLevel = enermyHealth;
        staminaLevel = enermyStamina;
        stateMachine.Check();
        Debug.Log("think running");
        StartCoroutine(Think());
    }

    protected void Awake()
    {
        //Finite State Machine
        stateMachine = new FSM<AIStates>();
        stateMachine.AddState(new Idle<AIStates>(AIStates.Idle, this, 0f));
        stateMachine.AddState(new Chase<AIStates>(AIStates.Chase, this, 0f));
        stateMachine.AddState(new Attack<AIStates>(AIStates.Attack, this, 0f));
        stateMachine.AddState(new Hold<AIStates>(AIStates.Hold, this, 0f));
        stateMachine.AddState(new SearchArea<AIStates>(AIStates.SearchArea, this, 0f));
        stateMachine.AddState(new Return<AIStates>(AIStates.Return, this, 0f));
        stateMachine.AddState(new Death<AIStates>(AIStates.Death, this, 0f));
        //transitions out of the Idle state
        stateMachine.AddTransition(AIStates.Idle, AIStates.Chase);
        stateMachine.AddTransition(AIStates.Idle, AIStates.Death);
        //transitions out of the Chase state
        stateMachine.AddTransition(AIStates.Chase, AIStates.SearchArea);
        stateMachine.AddTransition(AIStates.Chase, AIStates.Attack);
        stateMachine.AddTransition(AIStates.Chase, AIStates.Death);
        //transitions out of the attack state
        stateMachine.AddTransition(AIStates.Attack, AIStates.Hold);
        stateMachine.AddTransition(AIStates.Attack, AIStates.Chase);
        stateMachine.AddTransition(AIStates.Attack, AIStates.SearchArea);
        stateMachine.AddTransition(AIStates.Attack, AIStates.Death);
        //transitions out of the Search state
        stateMachine.AddTransition(AIStates.SearchArea, AIStates.Chase);
        stateMachine.AddTransition(AIStates.SearchArea, AIStates.Hold);
        stateMachine.AddTransition(AIStates.SearchArea, AIStates.Return);
        stateMachine.AddTransition(AIStates.SearchArea, AIStates.Death);
        //transitions out of the Hold state
        stateMachine.AddTransition(AIStates.Hold, AIStates.SearchArea);
        stateMachine.AddTransition(AIStates.Hold, AIStates.Chase);
        stateMachine.AddTransition(AIStates.Hold, AIStates.Death);
        //transitions out of the Return state
        stateMachine.AddTransition(AIStates.Return, AIStates.Idle);
        stateMachine.AddTransition(AIStates.Return, AIStates.Death);

        stateMachine.SetInitialState(AIStates.Idle);
    }
    public bool GuardIdleToChase(State<AIStates> currentState) { return (sight.CanSeeTarget()); }
    public bool GuardIdleToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }
    public bool GuardChaseToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardChaseToAttack(State<AIStates> currentState) { return (sight.distanceToTarget <= attackRange); }
    public bool GuardChaseToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }
    public bool GuardAttackToHold(State<AIStates> currentState) { return ((sight.distanceToTarget <= attackRange) && (minimumStamina >= staminaLevel)); }
    public bool GuardAttackToChase(State<AIStates> currentState) { return !(sight.distanceToTarget <= attackRange); }
    public bool GuardAttackToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardAttackToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }
    public bool GuardSearchAreaToChase(State<AIStates> currentState) { return (sight.CanSeeTarget()); }
    public bool GuardSearchAreaToHold(State<AIStates> currentState) { return ((sight.CanSeeTarget()) && (minimumStamina >= staminaLevel)); }
    public bool GuardSearchToReturn(State<AIStates> currentState) { return (!senses.CanSeeTarget() && !wait); }//add wait timer
    public bool GuardSearchAreaToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }
    public bool GuardHoldToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardHoldToChase(State<AIStates> currentState) { return (minimumStamina >= staminaLevel); }
    public bool GuardHoldToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }
    public bool GuardRerturnToIdle(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardReturnToDeath(State<AIStates> currentState) { return (healthLevel <= 0); }


}
