using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class BalancingInput
{
    public BalancingVariables name;
    public TMP_InputField inputField;
}

public enum BalancingVariables { PlayerHealth, PlayerRegen, EnemyHealth, HealthDrops, EnemyAttack, PlayerAttack };

public class BalancingManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static BalancingManager Instance { get; private set; }
    public float PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float PlayerRegen { get => playerRegen; set => playerRegen = value; }
    public float EnemyHealthModifier { get => enemyHealthModifier; set => enemyHealthModifier = value; }
    public float EnemyAttackModifier { get => enemyAttackModifier; set => enemyAttackModifier = value; }
    public float Healthdrops { get => healthdrops; set => healthdrops = value; }
    public float PlayerAttack { get => playerAttack; set => playerAttack = value; }
    public bool HasSetupHealthOrbs { get => hasSetupHealthOrbs; set => hasSetupHealthOrbs = value; }

    public List<BalancingInput> inputs;

    private float playerHealth;
    private float playerRegen;
    private float enemyHealthModifier;
    private float enemyAttackModifier;
    private float healthdrops;
    private float playerAttack;
    private bool hasSetupHealthOrbs = false;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }



    }

    void Start()
    {
        EnemyAttackModifier = 1;
    }

    public void SetPlayerHealth(float val)
    {
        PlayerHealth = val;
        Player.Instance.Health.MaxHealth = (int)PlayerHealth;
        Player.Instance.Health.CurrentHealth = (int)PlayerHealth;
    }

    public void SetPlayerRegen(float val)
    {
        PlayerRegen = val;
        Player.Instance.GetComponent<AbilityManager>().regenerationInterval = PlayerRegen;
    }

    public void SetPlayerAttack(float val)
    {
        PlayerAttack = val;
        Player.Instance.GetComponent<WeaponController>().sword.GetComponent<Sword>().SetDamage(PlayerAttack);
    }

    public void SetHealthOrbsValue(float val)
    {
        Healthdrops = val;
        Player.Instance.healthDropsAmount = (int)Healthdrops;
    }

    public void SetEnemyAttackValue(float val)
    {
        EnemyAttackModifier = val;
    }

    public void UpdateInputs()
    {
        foreach(BalancingInput b in inputs)
        {
            switch(b.name)
            {
                case BalancingVariables.PlayerHealth:
                    b.inputField.text = PlayerHealth.ToString();
                    break;
                case BalancingVariables.PlayerRegen:
                    b.inputField.text = PlayerRegen.ToString();
                    break;
                case BalancingVariables.HealthDrops:
                    b.inputField.text = Healthdrops.ToString();
                    break;
                case BalancingVariables.PlayerAttack:
                    b.inputField.text = PlayerAttack.ToString();
                    break;
                case BalancingVariables.EnemyAttack:
                    b.inputField.text = EnemyAttackModifier.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
