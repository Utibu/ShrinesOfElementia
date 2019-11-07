// Author: Bilal El Medkouri

using System;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    public static BossEvents Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public event Action OnBossFightAreaTriggerEnter;
    public void BossFightAreaTriggerEnter()
    {
        if (OnBossFightAreaTriggerEnter != null)
        {
            OnBossFightAreaTriggerEnter();
        }
    }
}
