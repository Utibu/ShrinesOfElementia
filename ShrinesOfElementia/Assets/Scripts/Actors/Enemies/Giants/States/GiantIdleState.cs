// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/IdleState")]
public class GiantIdleState : GiantBaseState
{
    public override void Enter()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter += OnBossAreaEnter;
    }

    private void OnBossAreaEnter()
    {
        owner.Transition<GiantPhaseOneState>();
    }

    private void OnDestroy()
    {
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }
}
