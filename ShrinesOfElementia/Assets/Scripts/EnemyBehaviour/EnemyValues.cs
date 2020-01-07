using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    [SerializeField] private string elementalType;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float atkCooldown;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float orbDropChance;
    [SerializeField] private GameObject orb;
    [SerializeField] private float experienceAmount;

    public float healthModifier = 1f;
    public float damageModifier = 1f;

    //If enemy is elite
    [SerializeField] private float castRange;



    public string ElementalType { get => ElementalType; set => ElementalType = value; }
    public float Health { get => health; set => health = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AtkCooldown { get => atkCooldown; set => atkCooldown = value; }
    public float SightRange { get => sightRange; set => sightRange = value; }
    public float AttackRange { get => attackRange; private set => attackRange = value; }
    public float CastRange { get => castRange; set => castRange = value; }
    public float OrbDropChance { get => orbDropChance; set => orbDropChance = value; }
    public GameObject Orb { get => orb; set => orb = value; }
    public float ExperienceAmount { get => experienceAmount; set => experienceAmount = value; }

    public bool GoBack;


    //called from atack animation, to specify when the enemy should approach before an attack, and when it should back away after hitting
    public void ToggleGoBack()
    {
        if(GoBack == false)
        {
            GoBack = true;
            return;
        }
        GoBack = false;
    }

    public void SetGoBackTrue()
    {
        GoBack = true;
    }
    public void SetGoBackFalse()
    {
        GoBack = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
}
