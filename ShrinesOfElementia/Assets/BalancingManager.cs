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

    public int PlayerHealth;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }



    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
