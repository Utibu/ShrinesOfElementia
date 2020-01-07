// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/IdleState")]
public class GiantIdleState : GiantBaseState
{
    public override void Enter()
    {
        //owner.Agent.SetDestination(owner.PatrolPoint.transform.position);
    }

    /*
    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (owner.DistanceToPlayer() < 10f)
        {
            owner.StartBattle();
        }
    }
    */
}
