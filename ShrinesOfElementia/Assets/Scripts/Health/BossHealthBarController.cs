//"author" Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarController : HealthBarController
{
    public static  BossHealthBarController Instance { get; private set; }
    public GameObject bossHealthBar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
    }

    public void OnBossFightTrigger(int MaxHP)
    {
        InitiateHealthValues(MaxHP);
        bossHealthBar.SetActive(true);
        
    }

    private void OnBossDeath(BossDeathEvent ev)
    {
        DeactivateBossHealthBar();
    }

    public void DeactivateBossHealthBar()
    {
        bossHealthBar.SetActive(false);
    }
}
