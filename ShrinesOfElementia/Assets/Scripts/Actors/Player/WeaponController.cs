//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public GameObject sword;
    [SerializeField] private GameObject shield;

    private Sword swordComponent;
    private Collider shieldCollider;

    [SerializeField] private int levelDamageIncrease;

    private void Start()
    {
        EventManager.Instance.RegisterListener<LevelUpEvent>(OnLevelUp);
        swordComponent = sword.GetComponent<Sword>();
        shieldCollider = shield.GetComponent<Collider>();
        EventManager.Instance.RegisterListener<StaggerEvent>(OnStagger);
    }

    public void SetSwordActive()
    {
        swordComponent.SetActive();
    }

    public void SetSwordDisabled()
    {
        swordComponent.SetDisabled();
    }

    public void SetShieldActive()
    {
        shieldCollider.enabled = true;
    }

    public void SetShieldDisabled()
    {
        shieldCollider.enabled = false;
    }

    private void OnLevelUp(LevelUpEvent eve)
    {
        sword.GetComponent<Sword>().SetDamage(levelDamageIncrease);
        print(sword.GetComponent<Sword>().getDamage + " " + Player.Instance.Health.MaxHealth);
    }
    
    private void OnStagger(StaggerEvent eve)
    {
        SetSwordDisabled();
        SetShieldDisabled();
    }
}
