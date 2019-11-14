// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected Player owner;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Player)owner;
    }

    public override void HandleUpdate()
    {
        HandlePlayerMovementInput();

        HandlePlayerInput();
    }

    protected void HandlePlayerMovementInput()
    {
        owner.MovementInput.UpdateMovementInput();
    }

    protected void HandlePlayerInput()
    {
        owner.PlayerInput.UpdatePlayerInput();
    }
}
