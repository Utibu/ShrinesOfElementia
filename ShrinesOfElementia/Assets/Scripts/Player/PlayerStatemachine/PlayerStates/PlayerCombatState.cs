using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerBaseState
{

    public override void Enter()
    {
        Player.Instance.GetComponent<MovementInput>().FaceCameraDirection = true;
        Player.Instance.Animator.SetBool("InCombat", true);
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            owner.Transition<PlayerRunState>();
        }
    }

    public override void Leave()
    {
        Player.Instance.GetComponent<MovementInput>().FaceCameraDirection = false;
        Player.Instance.Animator.SetBool("InCombat", false);
    }
}
