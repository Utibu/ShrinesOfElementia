// Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "InstantiateEnemyStates/BasicEnemyDie")]
public class Die_BasicEnemy : BasicEnemyBaseState
{

    private float dyingTime = 1.0f;
    private float timer;
    [SerializeField] private AnimationClip deathAnimation;


    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }


    public override void Enter()
    {
        base.Enter();
        timer = dyingTime;
        owner.Animator.SetTrigger("OnDeath");
        EventManager.Instance.FireEvent(new ExperienceEvent(experienceAmount + " xp gained", experienceAmount));
        owner.gameObject.GetComponent<Collider>().enabled = false;
        GameObject gameobject = owner.GetComponent<GameObject>();
        EventManager.Instance.FireEvent(new EnemyDeathEvent(gameobject, owner.SpawnArea, owner.Elite));
        DisableComponents();
        if (Random.Range(0f,100f) < orbDropChance )
        {
            GameObject.Instantiate(orb, owner.transform.position + owner.Player.transform.forward * 3f, new Quaternion(0, 0, 0, 0));
        }

        Destroy(owner.gameObject, deathAnimation.length + 1.0f);

    }

    private void DisableComponents()
    {
        owner.Agent.enabled = false;
        owner.Collider.enabled = false;
        owner.enabled = false;
        owner.GetComponent<HealthComponent>().Canvas.SetActive(false);
    }

    public override void HandleUpdate()
    {

        //base.HandleUpdate();
        //owner.transform.Rotate(new Vector3(1, 0, 0), 75);
    }


    public override void Leave()
    {
        //base.Leave();
    }
}
