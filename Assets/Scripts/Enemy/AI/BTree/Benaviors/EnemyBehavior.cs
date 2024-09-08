using System;
using System.Collections;
using AI.Btree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    protected Enemy owner;
    protected BehaviorTree tree = new("Tree");
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;

    private void Awake()
    {
        owner = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected IEnumerator AbilityCooldown(float cooldown, Action OnStart, Action OnStop)
    {
        OnStart();
        yield return new WaitForSeconds(cooldown);
        OnStop();
    }



}
