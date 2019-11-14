// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseTwoState")]
public class GiantPhaseTwoState : GiantPhaseOneState
{
    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;
        owner.Phase2Active = true;

        if (owner.HealthComponent.CurrentHealth <= (owner.HealthComponent.MaxHealth / 3))
        {
            owner.Transition<GiantPhaseThreeState>();
        }
    }
}
