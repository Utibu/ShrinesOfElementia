// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/ChaseState")]
public class GiantChaseState : GiantPhaseOneState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        owner.Agent.SetDestination(Player.Instance.transform.position);

        if (Vector3.Distance(owner.gameObject.transform.position, Player.Instance.transform.position) < owner.Agent.stoppingDistance)
        {
            owner.Agent.acceleration = 60f;
            owner.Transition<GiantPhaseOneState>();
        }

        base.HandleUpdate();
    }
}
