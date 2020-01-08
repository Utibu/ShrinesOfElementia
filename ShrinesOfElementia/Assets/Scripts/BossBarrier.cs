//Author: Joakim Ljung
//Co-Author: Sofia Chyle Kauko
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
    private ParticleSystem particles;
    private string element;
    [SerializeField] private BARRIERTYPES barrierTypes;

    private void Start()
    {
        barrierCollider = GetComponent<BoxCollider>();
        particles = GetComponentInChildren<ParticleSystem>();
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
        particles.Stop();
    }
}
