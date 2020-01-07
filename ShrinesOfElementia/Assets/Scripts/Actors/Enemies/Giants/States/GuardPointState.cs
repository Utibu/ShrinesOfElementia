using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Giant States/GuardPointState")]
public class GuardPointState : GiantBaseState
{

    HealthComponent HP;

    public override void Enter()
    {
        owner.Animator.SetBool("IsRunning", true);
        owner.Animator.SetTrigger("Run");
        owner.Agent.SetDestination(owner.PatrolPoint.transform.position);

        HP = owner.GetComponent<HealthComponent>();

        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.DistanceToPatrol() < 4f)
        {
            owner.Animator.SetBool("IsRunning", false);
            owner.Agent.SetDestination(owner.gameObject.transform.position);
        }

        
        if (owner.DistanceToPlayer() < owner.Agent.stoppingDistance)
        {
            owner.Transition<GiantPhaseOneState>();
        }

        // BOSS REGEN
        HP.CurrentHealth += 5;

        base.HandleUpdate();
    }

    public override void Leave()
    {
        owner.Animator.SetBool("IsRunning", false);

    }
}
