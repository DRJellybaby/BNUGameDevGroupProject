using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBrain : BossControler
{
    //[HideInInspector] private float healthLevel;
    //[HideInInspector] private float staminaLevel;
    public float attackRange;
    public float attackTime;
    public float minimumStamina;


    [HideInInspector] public bool wait;
    [HideInInspector] public float waitTime = 3;

    [HideInInspector] public bool chasing;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public Animator animator;

    [HideInInspector] public bool returning;
    [HideInInspector] public Quaternion startRotation;

    public enum BossAIStates { BossIdle, BossMove, BossDie, BossAttack};
    public FSM<BossAIStates> BossStateMachine;
    protected float thinkInterval = 0.4f;

    void Start()
    {
        base.Start();
        StartCoroutine(Think());
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        startRotation = GetComponent<Transform>().rotation;
        attackRange = 30;
        attackTime = (2.625f * 2.5f); //annimatiion length * (speed variable + 0.5)
    }

    void Fixedupdate()
    {
        Debug.Log(distanceToTarget);
    }

    // Update is called once per frame
    void Update()
    {
        BossStateMachine.CurrentState.Act();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        BossStateMachine.CurrentState.OnStateTriggerEnter(collider);
    }

    protected IEnumerator Think()
    {
        yield return new WaitForSeconds(thinkInterval);

        BossStateMachine.Check();
        StartCoroutine(Think());
    }

    public void startTimer() { StartCoroutine(waitTimer()); }

    public void attack() { StartCoroutine(attackRate()); }

    protected IEnumerator waitTimer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }

    protected IEnumerator attackRate()
    {
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotationAngle = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationAngle;
        animator.SetTrigger("SlashAttack");
        yield return new WaitForSeconds(attackTime);
        if (BossStateMachine.CurrentState.GetName() == "BossAttack") { StartCoroutine(attackRate()); }
    }


    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //Finite State Machine
        BossStateMachine = new FSM<BossAIStates>();
        BossStateMachine.AddState(new BossIdle<BossAIStates>(BossAIStates.BossIdle, this, 0f));
        BossStateMachine.AddState(new BossMove<BossAIStates>(BossAIStates.BossMove, this, 0f));
        BossStateMachine.AddState(new BossDie<BossAIStates>(BossAIStates.BossDie, this, 0f));
        BossStateMachine.AddState(new BossAttack<BossAIStates>(BossAIStates.BossAttack, this, 0f));
        //Idle Transitions
        BossStateMachine.AddTransition(BossAIStates.BossIdle, BossAIStates.BossAttack);
        BossStateMachine.AddTransition(BossAIStates.BossIdle, BossAIStates.BossDie);
        BossStateMachine.AddTransition(BossAIStates.BossIdle, BossAIStates.BossMove);
        //Move Transistions
        BossStateMachine.AddTransition(BossAIStates.BossMove, BossAIStates.BossAttack);
        BossStateMachine.AddTransition(BossAIStates.BossMove, BossAIStates.BossDie);
        BossStateMachine.AddTransition(BossAIStates.BossMove, BossAIStates.BossIdle);
        //Attack Transitions
        BossStateMachine.AddTransition(BossAIStates.BossAttack, BossAIStates.BossMove);
        BossStateMachine.AddTransition(BossAIStates.BossAttack, BossAIStates.BossDie);
        BossStateMachine.AddTransition(BossAIStates.BossAttack, BossAIStates.BossIdle);

        BossStateMachine.SetInitialState(BossAIStates.BossIdle);
    }
    //Guards (Out of idle)
    public bool GuardBossIdleToBossMove(State<BossAIStates> currentState) { return (sight.CanSeeTarget()); }
    public bool GuardBossIdleToBossDie(State<BossAIStates> currentState) { return (bossHealth <= 0); }
    public bool GuardBossIdleToBossAttack(State<BossAIStates> currentState) { return (sight.distanceToTarget <= attackRange); }
    //Guards (Out of move)
    public bool GuardBossMoveToBossIdle(State<BossAIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardBossMoveToBossDie(State<BossAIStates> currentState) { return (bossHealth <= 0); }
    public bool GuardBossMoveToBossAttack(State<BossAIStates> currentState) { return (sight.distanceToTarget <= attackRange); }
    //Guards (out of Attack)
    public bool GuardBossAttackToBossIdle(State<BossAIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardBossAttackToBossDie(State<BossAIStates> currentState) { return (bossHealth <= 0); }
    public bool GuardBossAttackToBossMove(State<BossAIStates> currentState) { return !(sight.distanceToTarget <= attackRange); }
}