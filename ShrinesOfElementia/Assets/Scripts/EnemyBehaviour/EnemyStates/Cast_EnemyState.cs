//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/Cast_EnemyState")]
public class Cast_EnemyState : BasicEnemyBaseState
{

    private float castTime;
    private float countdown;
   

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        //get nessecary variables
        EnemySpellManager spelldata = owner.GetComponent<EnemySpellManager>();
        castTime = spelldata.CastTime;
    }

    public override void Enter()
    {
        base.Enter();
        //stop and rotate / aim to player
        owner.Agent.destination = owner.transform.position;
        

        //set timer
        countdown = 0.5f;

        
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.transform.LookAt(owner.Player.transform);
        owner.transform.eulerAngles = new Vector3(0, owner.transform.eulerAngles.y, 0);

        //tick down spell channel
        countdown -= Time.deltaTime;

        //spellcast:
        if (countdown <= 0)
        {
            //get vector to use as aim when casting, is needed.
            Vector3 aim = owner.Player.transform.position - owner.transform.position;
            owner.GetComponent<EnemySpellManager>().SetAim(aim);

            owner.Animator.SetTrigger("ShouldCast");
            countdown = castTime;
        }
        else if (distanceToPlayer > castRange * 1.1)
        {
            owner.Transition<Chase_BasicEnemy>();
        }
        else if(distanceToPlayer <= 1.5f)
        {
            
            //Evade player when too close to cast
            owner.Transition<Flee_EnemyState>();
        }
    }

    public override void Leave()
    {
        base.Leave();
        owner.Agent.isStopped = false;
    }

    

}
