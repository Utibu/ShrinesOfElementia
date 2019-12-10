// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Fire Giant/ChaseState")]
public class FireGiantChaseState : FireGiantBattleState
{
    // This is all duplicate code. Needs to be removed/taken care of asap!

    public override void Enter()
    {
        owner.Animator.SetBool("IsRunning", true);
        owner.Animator.SetTrigger("Run");

        base.Enter();
    }

    public override void HandleUpdate()
    {
        owner.Agent.SetDestination(Player.Instance.transform.position);

        if (owner.DistanceToPlayer() < owner.Agent.stoppingDistance)
        {
            owner.Transition<FireGiantPhaseOneState>();
        }

        base.HandleUpdate();
    }

    public override void Leave()
    {
        owner.Animator.SetBool("IsRunning", false);
    }
}
