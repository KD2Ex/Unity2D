using AI.Btree;
using UnityEngine;
using Sequence = AI.Btree.Sequence;

public class RangedEnemyBehavior : EnemyBehavior
{
    [SerializeField] private Sensor attackSensor;
    [SerializeField] private Sensor dashSensor;
    
    [SerializeField] private AbilityStrategy ranged;
    //[SerializeField] private AbilityStrategy dash;
    [SerializeField] private DashNavMesh dash;

    private GameEventListener listener;
    
    private float wanderingCooldown;
    
    private bool rangedReady = true;
    private bool dashReady = true;
    private bool wanderingReady = true;

    private bool damagedRecently;
    
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }

    public void Damaged()
    {
        // stun management
        damagedRecently = true;
    }
    
    private void Start()
    {
        
        var rangedSelector = new PrioritySelector("Ranged Behavior");


        var stunSequence = new Sequence("stun", 30);
        stunSequence.AddChild(new Leaf("damaged Recently", new Condition(() => damagedRecently)));
        stunSequence.AddChild(new Leaf("relax", new RelaxStrategy(2f)));
        stunSequence.AddChild(new Leaf("reset", new ActionStrategy(() => damagedRecently = false)));
        
        var dashSequence = new Sequence("Dash away", 20);
        dashSequence.AddChild(new Leaf("Is Player close", new Condition(() => dashSensor.IsTargetInRange)));
        dashSequence.AddChild(new Leaf("IsReady", new Condition(() => dashReady)));

        var executeDashSequence = new Sequence("Execute dash");
        executeDashSequence.AddChild(new Leaf("Dash", new DashStrategy(dash, transform, dashSensor)));
        executeDashSequence.AddChild(new Leaf("Start cooldown", new ActionStrategy(() =>
        {
            StartCoroutine(AbilityCooldown(
                dash.Cooldown.Value,
                () => dashReady = false,
                () => dashReady = true
            ));
        })));
        dashSequence.AddChild(executeDashSequence);
        
        var wanderingSequence = new Sequence("Wandering Seq", 5);
        
        wanderingSequence.AddChild(new Leaf("IsRested", new Condition(() => wanderingReady)));
        wanderingSequence.AddChild(new Leaf("Not in danger", new Condition(() => !attackSensor.IsTargetInRange)));
        wanderingSequence.AddChild(new Leaf("Wandering", new Wandering(agent, 0f)));
        wanderingSequence.AddChild(new Leaf("Cooldown", new ActionStrategy(() =>
        {
            StartCoroutine(
                AbilityCooldown(wanderingCooldown, () => wanderingReady = false, () => wanderingReady = true)
                );
        })));


        var attackSequence = new Sequence("Attack", 10);
        attackSequence.AddChild(new Leaf("Is Player in range", new Condition(() => rangedReady && attackSensor.IsTargetInRange)));
        //attackSequence.AddChild(new Leaf("IsRead", new Condition(() => rangedReady)));
        attackSequence.AddChild(new Leaf("Shoot", new ActionStrategy(() =>
        {
            if (!attackSensor.TargetTransform) return;
            var direction = (attackSensor.TargetTransform.position - transform.position).normalized;
            agent.ResetPath();
            ranged
                .WithOffset(direction + direction)
                .WithDirection(direction)
                .Execute(transform);

            StartCoroutine(AbilityCooldown(ranged.Cooldown, () => rangedReady = false, () => rangedReady = true));
        })));
        attackSequence.AddChild(new Leaf("Strafe", new Wandering(agent, 0f)));

        var attackPrior = new PrioritySelector("Engage");
        attackPrior.AddChild(attackSequence);
        attackPrior.AddChild(wanderingSequence);
        
        rangedSelector.AddChild(attackPrior);
        rangedSelector.AddChild(stunSequence);
        //rangedSelector.AddChild(dashSequence);
        rangedSelector.AddChild(wanderingSequence);
        
        tree.AddChild(rangedSelector);
    }
    
    private void Update()
    {
        tree.Process();
    }
}
