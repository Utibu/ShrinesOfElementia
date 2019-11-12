// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseTwoState")]
public class GiantPhaseTwoState : GiantPhaseOneState
{
    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;

        if (owner.HealthComponent.CurrentHealth <= (owner.HealthComponent.MaxHealth / 3))
        {
            owner.Transition<GiantPhaseThreeState>();
        }
    }

    public override void HandleUpdate()
    {
        // Spawn
        // Spawn stuff

        // Stomp
        if (owner.StompAvailable && owner.stompRange >= owner.DistanceToPlayer())
        {
            owner.StompAvailable = false;
            owner.Transition<GiantStompState>();
        }

        // Sweep
        else if (owner.SweepAvailable && owner.sweepRange >= owner.DistanceToPlayer())
        {
            owner.SweepAvailable = false;
            owner.Transition<GiantSweepState>();
        }

        // Basic Attack
        else if (owner.basicAttackRange >= owner.DistanceToPlayer())
        {
            owner.Transition<GiantAttackState>();
        }

        // Chase
        else
        {
            owner.Transition<GiantChaseState>();
        }
    }
}
