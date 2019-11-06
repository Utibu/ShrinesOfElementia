﻿// Author: Bilal El Medkouri

using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public MovementInput MovementInput { get; private set; }
    public HealthComponent Health { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        Health = GetComponent<HealthComponent>();
        Animator = GetComponent<Animator>();
        MovementInput = GetComponent<MovementInput>();
    }
}
