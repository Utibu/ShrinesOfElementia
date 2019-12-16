// Author: Bilal El Medkouri
// Co-author: Joakim Ljung
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private MovementInput movementInput;
    private AbilityManager abilityManager;
    private AbilityIndicator abilityIndicator;

    private StaminaManager staminaManager;
    private bool invincible;

    private bool blockTrigger = false, isBlocking = false;
    public bool IsBlocking { get { return isBlocking; } }

    private float resetTimer = 0;
    private string[] lightAttacks;
    private int attackIndex = 0;
    private bool isAttacking;
    private float attackSpeed;
    private bool canAttack = true;
    private bool canBlock = true;

    
    [SerializeField] private float lightAttackTimer; // Temporary fix
    [SerializeField] private float lightAttack1;
    [SerializeField] private float lightAttack2;

    private void Start()
    {
        player = Player.Instance;
        movementInput = player.GetComponent<MovementInput>();
        abilityManager = GetComponent<AbilityManager>();
        abilityIndicator = GetComponent<AbilityIndicator>();
        lightAttacks = new string[] { "LightAttack1", "LightAttack2" };
        isAttacking = false;
        staminaManager = GetComponent<StaminaManager>();
    }

    private void Update()
    {
        /* Dirty cheats
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
        */

        // Fireball
        if (Input.GetKeyDown(InputManager.Instance.keyCode["Fire"].keyCode) && !IsBlocking)
        {
            abilityManager.CheckFireBall();
        }

        // Geyser
        if (Input.GetKey(InputManager.Instance.keyCode["Water"].keyCode) && !IsBlocking)
        {
            // activate projector
            //abilityIndicator.ShowIndicator();
            abilityManager.ToggleAim();
        }

        //Wind blade
        if (Input.GetKeyDown(InputManager.Instance.keyCode["Wind"].keyCode) && !IsBlocking)
        {
            abilityManager.CheckWindBlade();
        }

        //Earth spikes
        if (Input.GetKeyDown(InputManager.Instance.keyCode["Earth"].keyCode) && !IsBlocking)
        {
            abilityManager.CheckEarthSpikes();
        }

        else if (Input.GetKeyUp(InputManager.Instance.keyCode["Water"].keyCode) && !IsBlocking)
        {
            abilityManager.CheckGeyser();
            //abilityIndicator.HideIndicator();
            //print("hiding indicator");
            abilityManager.ToggleAim();
        }

        // Temporary water walking. Layer 9 is the player, and layer 4 is water

        /* Dirty cheats
        //Enable water walking
        if (Input.GetKeyDown(KeyCode.P))
        {
            Physics.IgnoreLayerCollision(9, 4, false);
        }

        //Disable water walking
        if (Input.GetKeyDown(KeyCode.O))
        {
            Physics.IgnoreLayerCollision(9, 4, true);
        }
        */

        /*
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Entire Body.Sprint")
            && staminaManager.CurrentStamina > 0) 
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                player.Animator.SetBool("InCombat", false);
                player.Animator.SetTrigger("IsSprinting");
            }
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || (player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Entire Body.Sprint") 
            && staminaManager.CurrentStamina <= 0))
        {
            player.Animator.SetTrigger("ToNeutral");
        }
        */

        if (Input.GetKeyDown(KeyCode.LeftControl) && !movementInput.IsDodging && !isBlocking 
            && player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Entire Body.Sprint")
            && staminaManager.CanDodge())
        {
            movementInput.OnDodge();
        }

        /*
        if((player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 1")
            || player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 2")))
        {
            //player.Animator.SetBool("CanBlock", true);
            print("is attacking");
        }
       */


        /* Dirty cheats
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(movementInput.RespawnLocation != null)
            {
                Player.Instance.MovementInput.MoveTo(movementInput.RespawnLocation.transform.position);
            }
        }
        */

        // Left click / Primary button
        if (Input.GetMouseButtonDown(0) && attackIndex < lightAttacks.Length && !isBlocking && canAttack && !movementInput.IsStaggered)
        {
            if(!player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 1")
            || !player.Animator.GetCurrentAnimatorStateInfo(1).IsName("Sword and Shield Slash 2"))
            {
                if(attackIndex == 0)
                {
                    TimerManager.Instance.SetNewTimer(gameObject, lightAttack1, AttackTimerEnd);
                    canAttack = false;
                }
                else if(attackIndex == 1)
                {
                    TimerManager.Instance.SetNewTimer(gameObject, lightAttack2, AttackTimerEnd);
                    canAttack = false;
                }
                LightAttack();
            }
            
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

        if (isBlocking == true  && player.Animator.GetBool("CanBlock")) // Start blocking
        {
            player.Animator.SetBool("InCombat", true);


            if (blockTrigger == true)   // Makes it so that the trigger is only called on once per "action"
                return;

            player.Animator.SetTrigger("ShieldBlock");

            blockTrigger = true;
        }
        else if ((blockTrigger == true && isBlocking == false) || staminaManager.CurrentStamina <= 0) // Resets the trigger if the player isn't blocking
        {
            blockTrigger = false;
            player.Animator.SetBool("CanBlock", true);
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Fire activated", "Fire"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Water activated", "Water"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Earth activated", "Wind"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Wind activated", "Earth"));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            invincible = !invincible;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            EventManager.Instance.FireEvent(new ExperienceEvent("", 10));
        }
        if (invincible)
        {
            Player.Instance.Health.CurrentHealth = Player.Instance.Health.MaxHealth;
        }
        
    }

    private void CancelAttack()
    {
        GetComponent<WeaponController>().SetSwordDisabled();
        player.Animator.SetBool("CanBlock", true);
        attackIndex = 0;
    }

    private void LightAttack()
    {
        player.Animator.SetBool("CanBlock", false);
        if (player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Entire Body.Sprint")) {
            player.Animator.SetBool("InCombat", true);
        }

        player.Animator.SetTrigger(lightAttacks[attackIndex]);
        attackIndex = (attackIndex + 1) % lightAttacks.Length;
        resetTimer = 0f;

        EventManager.Instance.FireEvent(new AttackEvent("...and the player strikes again"));
    }

    public void AttackTimerEnd()
    {
        if(player.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Sword and Shield Slash 2"))
            canAttack = true;
    }
}
