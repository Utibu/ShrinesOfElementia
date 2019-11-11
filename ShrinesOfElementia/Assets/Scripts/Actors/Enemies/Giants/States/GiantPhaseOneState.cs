// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseOneState")]
public class GiantPhaseOneState : GiantBaseState
{
    [SerializeField] private float movementSpeed;

    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;
    }

    public override void HandleUpdate()
    {
        if (Vector3.Distance(owner.gameObject.transform.position, Player.Instance.transform.position) > owner.Agent.stoppingDistance)
        {
            owner.Transition<GiantChaseState>();
        }
        else
        {
            UseAbility();
        }

        base.HandleUpdate();
    }

    protected virtual void UseAbility()
    {
        if (owner.SweepAvailable == true)
        {
            owner.SweepAvailable = false;
            owner.Transition<GiantSweepState>();
        }
        else
        {
            owner.Transition<GiantAttackState>();
        }
    }
}
