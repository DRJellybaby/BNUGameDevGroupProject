using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBrain : BossControler
{
    public float attackRange;
    public float damageScale;
    public float attackTime;
    public float minimumStamina;
    public float distanceToPlayer;

    private enermyAttack damage;
    //float slashCooldown = 0, spinCooldown = 0, overheadCooldown = 0;
    bool slashOnCooldown, spinOnCooldown, overheadOnCooldown;
    bool attacking;

    [HideInInspector] public bool wait;
    [HideInInspector] public float waitTime = 3;

    [HideInInspector] public bool chasing;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public Animator animator;

    [HideInInspector] public bool returning;
    [HideInInspector] public Quaternion startRotation;

    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip attackSound;

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
        if(gameObject.name == "Demon GreatSword") { attackRange = 25; damageScale = 0.7f; }
        else if(gameObject.name == "Demon_Boss_Rig") { attackRange = 60; damageScale = 1f; }

        damage = gameObject.transform.GetChild(0).GetComponent<enermyAttack>();
        distanceToPlayer = sight.getDistance();
    }

    // Update is called once per frame
    void Update()
    {
        sight.CanSeeTarget();
        distanceToPlayer = sight.getDistance();
        BossStateMachine.CurrentState.Act();

        if (BossStateMachine.CurrentState.GetName() == "BossMove" )
        {
            m_AudioSource.Play();
            m_AudioSource.loop = true;
        }
        else
        {
            m_AudioSource.Stop();
            m_AudioSource.loop = false;
        }
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
        if(BossStateMachine.CurrentState.GetName() == "BossMove") { thinkInterval = 0; }
        else { thinkInterval = 0.4f; }
        yield return new WaitForSeconds(thinkInterval);

        BossStateMachine.Check();
        StartCoroutine(Think());
    }

    public void startTimer() { StartCoroutine(waitTimer()); }

    public void attack()
    {
        StartCoroutine(attackRate());
    }

    protected IEnumerator waitTimer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }

    protected IEnumerator spinCooldown()
    {
        yield return new WaitForSeconds(20);
        spinOnCooldown = false;
    }
    protected IEnumerator overheadCooldown()
    {
        yield return new WaitForSeconds(12);
        overheadOnCooldown = false;
    }
    protected IEnumerator slashCooldown()
    {
        yield return new WaitForSeconds(2);
        slashOnCooldown = false;
    }

    protected IEnumerator attackRate()
    {
        attacking = true;
        //face player
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotationAngle = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationAngle;

        //checks if each move are on cooldown, and in order of priority exacutes them. also sets the attack time to the relivent duration of the animation so that they do not overlap
        if(!spinOnCooldown)
        {
            damage.damageValue = 50 * damageScale;
            animator.SetTrigger("SpinAttack");
            m_AudioSource.PlayOneShot(attackSound, 1);
            attackTime = 5f;
            spinOnCooldown = true;
            StartCoroutine(spinCooldown());
        }
        else if (!overheadOnCooldown)
        {
            damage.damageValue = 75;
            animator.SetTrigger("OverheadAttack");
            m_AudioSource.PlayOneShot(attackSound, 1);
            attackTime = 5.5f;
            overheadOnCooldown = true;
            StartCoroutine(overheadCooldown());
        }
        else if (!slashOnCooldown)
        {
            damage.damageValue = 25;
            m_AudioSource.PlayOneShot(attackSound, 1);
            animator.SetTrigger("SlashAttack");
            attackTime = 2f;
            slashOnCooldown = true;
            StartCoroutine(slashCooldown());
        }
        
        yield return new WaitForSeconds(attackTime);
        attacking = false;
        yield return new WaitForSeconds(1);
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
    public bool GuardBossIdleToBossAttack(State<BossAIStates> currentState) { return (distanceToPlayer <= attackRange); }
    //Guards (Out of move)
    public bool GuardBossMoveToBossIdle(State<BossAIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardBossMoveToBossDie(State<BossAIStates> currentState) { return (bossHealth <= 0); }
    public bool GuardBossMoveToBossAttack(State<BossAIStates> currentState) { return (distanceToPlayer <= attackRange); }
    //Guards (out of Attack)
    public bool GuardBossAttackToBossIdle(State<BossAIStates> currentState) { return (!sight.CanSeeTarget()); }
    public bool GuardBossAttackToBossDie(State<BossAIStates> currentState) { return (bossHealth <= 0); }
    public bool GuardBossAttackToBossMove(State<BossAIStates> currentState) { return ((distanceToPlayer >= attackRange) && !attacking); }
}