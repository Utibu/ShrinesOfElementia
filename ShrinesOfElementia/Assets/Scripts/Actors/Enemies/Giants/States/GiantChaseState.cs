// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/ChaseState")]
public class GiantChaseState : GiantBaseState
{
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
            owner.Transition<GiantPhaseOneState>();
        }

        base.HandleUpdate();
    }

    public override void Leave()
    {
        owner.Animator.SetBool("IsRunning", false);
    }
}
