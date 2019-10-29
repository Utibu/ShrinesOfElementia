﻿// Author: Bilal El Medkouri

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private MovementInput movementInput;

    private bool blockTrigger = false, isBlocking = false;

    [Header("Temporary Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed;
    [SerializeField] private GameObject fireballSpawnLocation;

    [Header("Temporary Geyser attributes")]
    [SerializeField] private GameObject geyserPrefab;
    [SerializeField] private GameObject geyserSpawnLocation;

    private float resetTimer = 0;
    private float lightAttackTimer = 0.9f; // Temporary fix
    private string[] lightAttacks;
    private int attackIndex = 0;

    private void Start()
    {
        player = Player.Instance;
        movementInput = player.GetComponent<MovementInput>();
        lightAttacks = new string[] { "LightAttack1", "LightAttack2" };
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


        // Fireball
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation.transform.position, Quaternion.identity);
            fireball.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);
        }

        // Geyser
        if (Input.GetKeyDown(KeyCode.R))
        {
            /*GameObject geyser = */
            Instantiate(geyserPrefab, geyserSpawnLocation.transform.position, Quaternion.identity);
        }

        // Temporary water walking. Layer 9 is the player, and layer 4 is water

        //Enable water walking
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Physics.IgnoreLayerCollision(9, 4, false);
        }

        //Disable water walking
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Physics.IgnoreLayerCollision(9, 4, true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Animator.SetBool("InCombat", false);
            player.Animator.SetTrigger("IsSprinting");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Entire Body.Sprint"))
        {
            print("sprint key up");
            player.Animator.SetTrigger("ToNeutral");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !movementInput.IsDodging && !isBlocking && player.Animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Entire Body.Combat"))
        {
            movementInput.OnDodge();
        }


        // Mouse buttons, 0 - Primary Button, 1 - Secondary Button, 2 - Middle Click

        // Left click / Primary button
        if (Input.GetMouseButtonDown(0) && attackIndex < lightAttacks.Length)
        {
            LightAttack();
        }

        if (attackIndex > 0)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > lightAttackTimer)
            {
                player.Animator.SetTrigger("ResetAttack");
                attackIndex = 0;
            }
        }

        //lightAttackTimer -= Time.deltaTime; // Temporary fix

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
            player.Animator.SetBool("InCombat", true);


            if (blockTrigger == true)   // Makes it so that the trigger is only called on once per "action"
                return;

            player.Animator.SetTrigger("ShieldBlock");

            blockTrigger = true;
        }
        else if (blockTrigger == true && isBlocking == false) // Resets the trigger if the player isn't blocking
        {
            blockTrigger = false;
        }

        if (Player.Instance.Animator.GetBool("InCombat"))
        {

        }
    }

    private void LightAttack()
    {
        player.Animator.SetBool("InCombat", true);
        // Temporary fix to stop animations from repeating

        player.Animator.SetTrigger(lightAttacks[attackIndex]);
        attackIndex = (attackIndex + 1) % lightAttacks.Length;
        resetTimer = 0f;

        //print(resetTimer + "     " + lightAttackTimer);


        /*
        if (lightAttackTimer <= 0f)
        {
            lightAttackTimer = 0.5f;
            player.Animator.SetTrigger("LightAttack1");
        }
        */
    }
}
