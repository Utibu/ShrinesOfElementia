// Author: Bilal El Medkouri

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;

    private float lightAttackTimer = 0f; // Temporary fix

    private void Start()
    {
        player = Player.Instance;
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


        // Mouse buttons, 0 - Primary Button, 1 - Secondary Button, 2 - Middle Click

        // Left click / Primary button
        if (Input.GetMouseButtonDown(0))
        {
            // Temporary fix to stop animations from repeating
            if (lightAttackTimer <= 0f)
            {
                lightAttackTimer = 1.5f;
                player.Animator.SetTrigger("LightAttack1");
            }
        }

        lightAttackTimer -= Time.deltaTime;

        // Holding down left click / Primary button
        if (Input.GetMouseButton(0))
        {
            // Heavy Attack
        }

        // Holding down right click / Secondary button
        if (Input.GetMouseButton(1))
        {
            // Block
        }
    }
}
