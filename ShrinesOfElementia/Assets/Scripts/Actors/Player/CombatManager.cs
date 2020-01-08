//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool InCombat { get; }
    [SerializeField] private float combatDuration = 3.0f;
    GameObject timer;

    private void Start()
    {
        EventManager.Instance.RegisterListener<CombatEvent>(OnCombatEvent);
    }

    private void OnCombatEvent(CombatEvent eve)
    {
        EnterCombat();
        if(timer != null)
        {
            Destroy(timer);
        }
        timer = TimerManager.Instance.SetNewTimer(gameObject, combatDuration, LeaveCombat);
    }

    private void EnterCombat()
    {
        Player.Instance.Animator.SetBool("InCombat", true);
    }

    private void LeaveCombat()
    {
        Player.Instance.Animator.SetBool("InCombat", false);
    }
}
