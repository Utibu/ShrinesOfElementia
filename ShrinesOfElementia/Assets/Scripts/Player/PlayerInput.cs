// Author: Bilal El Medkouri

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private MovementInput movementInput;

    private float lightAttackTimer = 0f; // Temporary fix
    private bool blockTrigger = false, isBlocking = false;
    


    private void Start()
    {
        player = Player.Instance;
        movementInput = player.GetComponent<MovementInput>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Deals 10 damage to the player
            player.Health.CurrentHealth -= 10;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // Heals the player for 10 hp
            player.Health.CurrentHealth += 10;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Animator.speed = movementInput.RunSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.Animator.speed = movementInput.DefaultSpeed;
        }


        // Mouse buttons, 0 - Primary Button, 1 - Secondary Button, 2 - Middle Click

        // Left click / Primary button
        if (Input.GetMouseButtonDown(0))
        {
            movementInput.FaceCameraDirection = true;
            // Temporary fix to stop animations from repeating
            if (lightAttackTimer <= 0f)
            {
                lightAttackTimer = 1.5f;
                player.Animator.SetTrigger("LightAttack1");
            }
        }

        lightAttackTimer -= Time.deltaTime; // Temporary fix

        // Holding down left click / Primary button
        if (Input.GetMouseButton(0))
        {
            // Heavy Attack
        }

        // Holding down right click / Secondary button

        isBlocking = Input.GetMouseButton(1);
        player.Animator.SetBool("IsBlocking", isBlocking);

        if (isBlocking == true) // Start blocking
        {

            if (blockTrigger == true)   // Makes it so that the trigger is only called on once per "action"
                return;

            player.Animator.SetTrigger("ShieldBlock");

            blockTrigger = true;
        }
        else if (blockTrigger == true && isBlocking == false) // Resets the trigger if the player isn't blocking
        {
            blockTrigger = false;
        }
    }
}
