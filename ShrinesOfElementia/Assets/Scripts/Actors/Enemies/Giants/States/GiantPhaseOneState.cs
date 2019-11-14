// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseOneState")]
public class GiantPhaseOneState : GiantBattleState
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
}
