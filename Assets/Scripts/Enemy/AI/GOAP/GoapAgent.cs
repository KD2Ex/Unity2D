using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class GoapAgent : MonoBehaviour
{
    
    [Header("Known locations")] 
    [SerializeField] private Transform restPos;

    [SerializeField] private List<NamedTransform> knownLocations;

    [HideInInspector] public StunController _stunController;
    [HideInInspector] public NavMeshAgent _navMeshAgent;
    [HideInInspector] public AnimationController _animation;
    [HideInInspector] public Rigidbody2D _rb;

    private Transform _playerPosition;

    [Header("Stats")] 
    public float health = 100;
    public float stamina = 100;
    
    private GameObject _target;
    private Vector3 _destination;

    private AgentGoal _lastGoal;

    // public for test
    public AgentGoal currentGoal;
    public ActionPlan actionPlan;
    public AgentAction currentAction;

    public Dictionary<string, AgentBelief> beliefs;
    public HashSet<AgentAction> actions;
    public HashSet<AgentGoal> goals;
    public BeliefFactory beliefFactory;

    //[SerializeField] private GoapBehavior _goapBehaviorSO;

    /*public HashSet<AgentAction> Actions => _goapBehaviorSO.actions;
    public Dictionary<string, AgentBelief> Beliefs => _goapBehaviorSO.beliefs;
    public HashSet<AgentGoal> Goals => _goapBehaviorSO.goals;*/
    
    private IGoapPlanner gPlanner;
    
    private bool InRangeOf(Vector3 pos, float range) => Vector2.Distance(transform.position, pos) < range;

    protected List<CountDownTimer> _timers = new();
    
    /*public void SetKnownLocations(List<Transform> locations)
    {
        knownLocations = locations;
    }*/
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<AnimationController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stunController = GetComponent<StunController>();
        
        

        /*
        if (_goapBehaviorSO)
        {
            _goapBehaviorSO.Initialize(this);
        }
        */
        
        //_rb.freezeRotation = true;

        if (GameObject.FindGameObjectsWithTag("KnownPos").Length > 0)
        {
            restPos = GameObject.FindGameObjectsWithTag("KnownPos")[0].transform;
        }

        gPlanner = new GoapPlanner();
    }

    void Start()
    {
        SetupTimers();
        SetupBeliefs();
        SetupActions();
        SetupGoals();
    }

    protected virtual void SetupGoals()
    {
        goals = new HashSet<AgentGoal>();
    }
    
    protected virtual void SetupBeliefs()
    {
        beliefs = new Dictionary<string, AgentBelief>();
        beliefFactory = new BeliefFactory(this, beliefs);
    }

    protected virtual void SetupActions()
    {
        actions = new HashSet<AgentAction>();
    }
    
    protected virtual void SetupTimers()
    {
        foreach (var timer in _timers)
        {
            timer.Start();
        }
        
        /*StartCoroutine(Timer());
        
        IEnumerator Timer()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(2f);
                UpdateStats();
            }
        }*/
    }
    
    

    void UpdateStats()
    {
//        stamina += InRangeOf(restPos.position, 3f) ? 20 : -10;
        stamina = Mathf.Clamp(stamina, 0, 100);
    }

    protected void HandleTargetChange(Transform transform)
    {
        // Force the re-evaluation of the plan
        
        currentAction = null;
        currentGoal = null;

        if (!transform) return;
        _playerPosition = transform;

    }

    void Update()
    {

        foreach (var timer in _timers)
        {
            timer.Tick(Time.deltaTime);
        }
        
        _animation.SetSpeed(_navMeshAgent.velocity.magnitude, _navMeshAgent.speed);
        /*_animator.SetFloat("X", _movementDirection.x);
        _animator.SetFloat("Y", _movementDirection.y);
        _animator.SetFloat("LastX", _lastMoveDirection.x);
        _animator.SetFloat("LastY", _lastMoveDirection.y);*/
        
        _animation.SetDirection(_navMeshAgent.velocity.normalized);

        if (_navMeshAgent.velocity.magnitude > .1f)
        {
            _animation.SetLastDirection(_navMeshAgent.velocity.normalized);
        }
        
        if (currentAction == null)
        {
            CalculatePlan();

            if (actionPlan != null && actionPlan.Actions.Count > 0)
            {
                _navMeshAgent.ResetPath();

                currentGoal = actionPlan.AgentGoal;
                Debug.Log($"Goal: {currentGoal.Name} with {actionPlan.Actions.Count} actions in plan");
                currentAction = actionPlan.Actions.Pop();
                Debug.Log($"Popped action: {currentAction.Name}");
                if (currentAction.Preconditions.All(b => b.Evaluate()))
                {
                    currentAction.Start();
                }
                else
                {
                    Debug.Log("Preconditions aren't met");
                    currentAction = null;
                    currentGoal = null;
                }
            } 
        }

        if (actionPlan != null && currentAction != null)
        {
            /*Debug.Log(currentAction.Name);
            Debug.Log(currentAction.Complete);*/
            currentAction.Update(Time.deltaTime);

            if (currentAction.Complete)
            {
                Debug.Log($"{currentAction.Name} complete");
                currentAction.Stop();
                currentAction = null;

                if (actionPlan.Actions.Count == 0)
                {
                    Debug.Log("Plan complete");
                    _lastGoal = currentGoal;
                    currentGoal = null;
                }
            }
        }
    }

    void CalculatePlan()
    {
        var priorityLevel = currentGoal?.Priority ?? 0;

        HashSet<AgentGoal> goalsToCheck = goals;

        if (currentGoal != null)
        {
            Debug.Log("Goal exists");
            goalsToCheck = new HashSet<AgentGoal>(goals.Where(g => g.Priority > priorityLevel));
        }

        var potentialPlan = gPlanner.Plan(this, goalsToCheck, _lastGoal);
        if (potentialPlan != null)
        {
            /*foreach (var goal in goalsToCheck)
            {
                Debug.Log(goal.Name);
            }
            */
            actionPlan = potentialPlan;
        }
    }
}
