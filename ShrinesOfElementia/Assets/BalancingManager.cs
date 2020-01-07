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

public enum BalancingVariables { PlayerHealth, PlayerRegen, EnemyHealth, HealthDrops, EnemyAttack, PlayerLives };

public class BalancingManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static BalancingManager Instance { get; private set; }

    public List<BalancingInput> inputs;

    public float PlayerHealth;
    public float PlayerRegen;
    public float PlayerLives;
    public float EnemyHealth;
    public float EnemyAttack;
    public float Healthdrops;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }



    }

    void Start()
    {
        
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
