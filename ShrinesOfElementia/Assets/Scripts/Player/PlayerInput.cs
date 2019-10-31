// Author: Bilal El Medkouri

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private MovementInput movementInput;
    private AbilityManager abilityManager;

    private bool blockTrigger = false, isBlocking = false;

    /*     Moved to AbilityManager
    [Header("Temporary Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed, fireballCooldown;
    [SerializeField] private GameObject fireballSpawnLocation;
    private float fireballTimer;

    [Header("Temporary Geyser attributes")]
    [SerializeField] private GameObject geyserPrefab;
    [SerializeField] private float geyserCooldown;
    [SerializeField] private GameObject geyserSpawnLocation;
    private float geyserTimer;
    */

    private float resetTimer = 0;
    [SerializeField] private float lightAttackTimer; // Temporary fix
    private string[] lightAttacks;
    private int attackIndex = 0;
    private bool isAttacking;
    private float attackSpeed;

    [SerializeField] private RectTransform shrinePanel; //TEMPORARY, REMOVE AFTER POC

    private void Start()
    {
        player = Player.Instance;
        movementInput = player.GetComponent<MovementInput>();
        abilityManager = GetComponent<AbilityManager>();
        lightAttacks = new string[] { "LightAttack1", "LightAttack2" };
        isAttacking = false;

        //fireballTimer = 0f;
        //geyserTimer = 0f;
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
            player.Health.CurrentHealth += 30;
        }


        // Fireball
        if (Input.GetKeyDown(KeyCode.Q)) //  && fireballTimer <= 0f
        {
            abilityManager.CastFireBall();

            /*
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation.transform.position, Quaternion.identity);
            fireball.GetComponent<Rigidbody>().AddForce(transform.forward * fireballSpeed, ForceMode.VelocityChange);
            fireballTimer = fireballCooldown;
            */
        }

        //fireballTimer -= Time.deltaTime;

        // Geyser
        if (Input.GetKeyDown(KeyCode.R)) // && geyserTimer <= 0f
        {
            abilityManager.CastGeyser();
            /*GameObject geyser = */
            /*
            Instantiate(geyserPrefab, geyserSpawnLocation.transform.position, Quaternion.identity);

            geyserTimer = geyserCooldown;
            */
        }

        //geyserTimer -= Time.deltaTime;

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Entire Body.Sprint")) 
        {
            player.Animator.SetBool("InCombat", false);
            player.Animator.SetTrigger("IsSprinting");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            print("sprint key up");
            player.Animator.SetTrigger("ToNeutral");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !movementInput.IsDodging && !isBlocking && player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Entire Body.Sprint"))
        {
            movementInput.OnDodge();
        }

        if((player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 1")
            || player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 2")))
        {
            //isAttacking = true;
        }
        else
        {
            //isAttacking = false;
        }

        // Mouse buttons, 0 - Primary Button, 1 - Secondary Button, 2 - Middle Click

        // Left click / Primary button
        if (Input.GetMouseButtonDown(0) && attackIndex < lightAttacks.Length && !isBlocking)
        {
            if(!player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 1")
            || !player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 2"))
            {
                LightAttack();
                print("attack input");
            }
            
        }

        if (attackIndex > 0)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > lightAttackTimer)
            {
                print("resetting attack");
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

        if (Input.GetKeyDown(KeyCode.Escape) && shrinePanel.gameObject.activeSelf == true)
        {
            shrinePanel.gameObject.SetActive(false);
            movementInput.TakeInput = true;
        }
    }

    private void LightAttack()
    {
        player.Animator.SetBool("InCombat", true);
        // Temporary fix to stop animations from repeating

        player.Animator.SetTrigger(lightAttacks[attackIndex]);
        attackIndex = (attackIndex + 1) % lightAttacks.Length;
        resetTimer = 0f;

        //print(attackIndex);

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
