﻿// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseOneState")]
public class GiantPhaseOneState : GiantBaseState
{
    [SerializeField] protected float movementSpeed;

    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;

        if (owner.HealthComponent.CurrentHealth <= ((owner.HealthComponent.MaxHealth * 2) / 3))
        {
            owner.Transition<GiantPhaseTwoState>();
        }
    }

    public override void HandleUpdate()
    {
        // Sweep
        if (owner.SweepAvailable && owner.sweepRange >= owner.DistanceToPlayer())
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
