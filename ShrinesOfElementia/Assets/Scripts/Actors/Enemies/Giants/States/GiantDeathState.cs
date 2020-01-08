//Author: Sofia Chyle Kauko
//Co-Author: Joakim Ljung
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Giant States/DeathState")]
public class GiantDeathState : GiantBaseState
{
    public override void Enter()
    {
        
        EventManager.Instance.FireEvent(new BossDeathEvent(owner.ElementalType, owner.gameObject, null, false));
        owner.Animator.SetTrigger("Die");
        owner.Agent.isStopped = true;
        foreach (ParticleSystem ps in owner.gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            ps.gameObject.SetActive(false);
        }
        Destroy(owner.GetComponent<HealthComponent>());

        Destroy(owner);

        Destroy(owner.gameObject.GetComponentInChildren<BossFightTriggerArea>());
        

        
    }
    
}
