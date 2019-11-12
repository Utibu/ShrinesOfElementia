// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseThreeState")]
public class GiantPhaseThreeState : GiantPhaseTwoState
{
    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;
    }

    public override void HandleUpdate()
    {
        if (owner.LeapAvailable && owner.leapRange >= owner.DistanceToPlayer())
        {
            owner.LeapAvailable = false;
            owner.Transition<GiantLeapState>();
        }

        // Spawn
        // Spawn stuff

        // Stomp
        else if (owner.StompAvailable && owner.stompRange >= owner.DistanceToPlayer())
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
