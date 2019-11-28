// Author: Bilal El Medkouri

public class GiantBattleState : GiantBaseState
{
    public override void HandleUpdate()
    {
        if (owner.Phase3Active && owner.LeapAvailable && owner.LeapRange >= owner.DistanceToPlayer())
        {
            owner.LeapAvailable = false;
            owner.Transition<GiantLeapState>();
        }

        // Spawn
        // Spawn stuff


        // Stomp
        else if (owner.Phase2Active && owner.StompAvailable && owner.StompRange >= owner.DistanceToPlayer())
        {
            owner.StompAvailable = false;
            owner.Transition<GiantStompState>();
        }

        // Sweep
        else if (owner.SweepAvailable && owner.SweepRange >= owner.DistanceToPlayer())
        {
            owner.SweepAvailable = false;
            owner.Transition<GiantSweepState>();
        }

        // Basic Attack
        else if (owner.BasicAttackRange >= owner.DistanceToPlayer())
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
