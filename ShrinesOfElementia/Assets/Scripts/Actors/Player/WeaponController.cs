//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject shield;
    [SerializeField] private int levelDamageIncrease;

    private void Start()
    {
        EventManager.Instance.RegisterListener<LevelUpEvent>(OnLevelUp);
    }

    public void SetSwordActive()
    {
        sword.GetComponent<Sword>().SetActive();
    }

    public void SetSwordDisabled()
    {
        sword.GetComponent<Sword>().SetDisabled();
    }

    public void SetShieldActive()
    {
        shield.GetComponent<Collider>().enabled = true;
    }

    public void SetShieldDisabled()
    {
        shield.GetComponent<Collider>().enabled = false;
    }

    private void OnLevelUp(LevelUpEvent eve)
    {
        sword.GetComponent<Sword>().SetDamage(levelDamageIncrease);
        print(sword.GetComponent<Sword>().getDamage + " " + Player.Instance.Health.MaxHealth);
    }

}
