using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDrivenBrain : EnermyControler
{
    //[HideInInspector] private float healthLevel;
    //[HideInInspector] private float staminaLevel;
    public float attackRange;
    public float attackTime;
    public float minimumStamina;

    [HideInInspector] public Transform Player;

    [HideInInspector] public bool wait;
    [HideInInspector] public float waitTime = 3;

    [HideInInspector] public bool chasing;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public Animator animator;

    [HideInInspector] public bool returning;
    [HideInInspector] public Quaternion startRotation;

    public enum AIStates { Idle, Chase, Attack, Hold, SearchArea, Return, Death };
    public FSM<AIStates> stateMachine;
    protected float thinkInterval = 0f;

    void Start()
    {
        base.Start();
        StartCoroutine(Think());
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        startRotation = GetComponent<Transform>().rotation;

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrentState.Act();
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        stateMachine.CurrentState.OnStateTriggerEnter(collider);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    protected IEnumerator Think()
    {
        sight.CanSeeTarget();
        yield return new WaitForSeconds(thinkInterval);
        
        stateMachine.Check();
        StartCoroutine(Think());
    }

    public void startTimer() {StartCoroutine(waitTimer()); }

    public void attack() { StartCoroutine(attackRate()); }

    protected IEnumerator waitTimer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }

    protected IEnumerator attackRate()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotationAngle = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationAngle;
        if (stateMachine.CurrentState.GetName() == "Attack") { StartCoroutine(attackRate()); }
    }

    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
    //guards
    public bool GuardIdleToChase(State<AIStates> currentState) { return (sight.CanSeeTarget()); }
    public bool GuardIdleToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
    public bool GuardChaseToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardChaseToAttack(State<AIStates> currentState) { return (sight.distanceToTarget <= attackRange); }
    public bool GuardChaseToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
    public bool GuardAttackToHold(State<AIStates> currentState) { return ((sight.distanceToTarget <= attackRange) && (minimumStamina >= enermyStamina)); }
    public bool GuardAttackToChase(State<AIStates> currentState) { return !(sight.distanceToTarget <= attackRange); }
    public bool GuardAttackToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardAttackToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
    public bool GuardSearchAreaToChase(State<AIStates> currentState) { return (sight.CanSeeTarget()); }
    public bool GuardSearchAreaToHold(State<AIStates> currentState) { return ((sight.CanSeeTarget()) && (minimumStamina >= enermyStamina)); }
    public bool GuardSearchAreaToReturn(State<AIStates> currentState) { return (!sight.CanSeeTarget() && !wait); }//add wait timer
    public bool GuardSearchAreaToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
    public bool GuardHoldToSearchArea(State<AIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardHoldToChase(State<AIStates> currentState) { return (minimumStamina >= enermyStamina); }
    public bool GuardHoldToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
    public bool GuardReturnToIdle(State<AIStates> currentState) { return (!sight.CanSeeTarget() && !returning); }
    public bool GuardReturnToDeath(State<AIStates> currentState) { return (enermyHealth <= 0); }
}