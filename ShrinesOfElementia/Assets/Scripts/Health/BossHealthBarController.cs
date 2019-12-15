//"author" Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarController : HealthBarController
{
    public static  HealthBarController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
