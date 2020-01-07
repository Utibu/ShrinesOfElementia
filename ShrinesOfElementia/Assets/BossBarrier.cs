//Author: Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    public enum BARRIERTYPES
    {
        Fire,
        Water,
        Earth,
        Wind
    };

    private Collider barrierCollider;
    private string element;
    [SerializeField] private BARRIERTYPES barrierTypes;

    private void Start()
    {
        barrierCollider = GetComponent<BoxCollider>();
        EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
        switch (barrierTypes)
        {
            case BARRIERTYPES.Fire:
                element = "Fire";
                break;
            case BARRIERTYPES.Water:
                element = "Water";
                break;
            case BARRIERTYPES.Earth:
                element = "Earth";
                break;
            case BARRIERTYPES.Wind:
                element = "Wind";
                break;
        }
    }

    private void OnBossDeath(BossDeathEvent eve)
    {
        if(eve.ElementType == element)
        {
            DisableBarrier();
        }
    }

    private void DisableBarrier()
    {
        barrierCollider.enabled = false;
    }
}
