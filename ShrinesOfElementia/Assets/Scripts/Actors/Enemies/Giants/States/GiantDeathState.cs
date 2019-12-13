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
        Destroy(owner.GetComponent<HealthComponent>());
        Destroy(owner);
        

        
    }
    
}
