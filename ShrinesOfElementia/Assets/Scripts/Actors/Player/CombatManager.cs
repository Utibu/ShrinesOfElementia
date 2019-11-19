//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private MovementInput movementInput;
    public bool InCombat { get { return inCombat; } }
    private bool inCombat;
    private int enemiesAttacking;


    private void Start()
    {
        movementInput = GetComponent<MovementInput>();
        EventManager.Current.RegisterListener<CombatEvent>(OnCombatEvent);
        enemiesAttacking = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(enemiesAttacking);
        }
    }

    private void OnCombatEvent(CombatEvent eve)
    {
        if(inCombat == false)
        {
            EnterCombat();
        }
        if (eve.EnteredCombat)
        {
            enemiesAttacking++;
        }
        else
        {
            enemiesAttacking--;
        }

        if(enemiesAttacking <= 0)
        {
            LeaveCombat();
        }
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
