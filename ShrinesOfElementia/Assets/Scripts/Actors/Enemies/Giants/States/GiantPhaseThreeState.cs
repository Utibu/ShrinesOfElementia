// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseThreeState")]
public class GiantPhaseThreeState : GiantPhaseTwoState
{
    public override void Enter()
    {
        owner.Phase3Active = true;
        owner.Agent.speed = movementSpeed;
    }
}
