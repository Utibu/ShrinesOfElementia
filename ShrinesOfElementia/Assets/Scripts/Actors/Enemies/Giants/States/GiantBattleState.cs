// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/BattleState")]
public class GiantBattleState : GiantBaseState
{
    public override void HandleUpdate()
    {
        if (owner.Phase3Active && owner.LeapAvailable && owner.leapRange >= owner.DistanceToPlayer())
        {
            owner.LeapAvailable = false;
            owner.Transition<GiantLeapState>();
        }

        // Spawn
        // Spawn stuff


        // Stomp
        else if (owner.Phase2Active && owner.StompAvailable && owner.stompRange >= owner.DistanceToPlayer())
        {
            owner.StompAvailable = false;
            owner.Transition<GiantStompState>();
        }

        //Fireball
        else if (owner.FireballAvaliable && owner.FireballMinRange < owner.DistanceToPlayer())
        {
            owner.Transition<GiantFireballState>();
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
