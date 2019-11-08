// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/PhaseOneState")]
public class GiantPhaseOneState : GiantBaseState
{
    [SerializeField] private float movementSpeed;

    [Header("Phase One Abilities")]
    [SerializeField] private float attackRange;
    [SerializeField] private float sweepCooldown;
    private float sweepTimer;

    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;

        sweepTimer = sweepCooldown;
    }

    public override void HandleUpdate()
    {
        if (Vector3.Distance(owner.gameObject.transform.position, Player.Instance.transform.position) > attackRange)
        {
            owner.Transition<GiantChaseState>();
        }
        else
        {
            UseAbility();
        }

        CountdownCooldowns();

        base.HandleUpdate();
    }

    protected virtual void UseAbility()
    {
        if (sweepTimer <= 0f)
        {
            owner.Transition<GiantSweepState>();
        }
        else
        {
            owner.Transition<GiantAttackState>();
        }
    }

    protected virtual void CountdownCooldowns()
    {
        if (sweepTimer > 0f)
        {
            sweepTimer -= Time.deltaTime;
        }
    }

}
