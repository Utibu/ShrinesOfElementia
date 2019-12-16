// Main Author: Joakim Ljung
// Co-Authors: Sofia Kauko, Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{




    // Components
    private Player player;
    private Animator animator;
    private PlayerInput playerInput;

    //should be private, and set to camera.Instance in start. this was just quickfix bc error mayhem // ymer
    public CameraReference cameraCamCamTheGlam;
    private CharacterController controller;
    

    // Variables
    private Vector2 movementInput;
    private Vector3 desiredMoveDirection;

    private bool faceCameraDirection;
    public bool FaceCameraDirection { set { faceCameraDirection = value; } }
    
    private float animationSpeed;
    private float allowPlayerRotation;
    
    private float dodgeTimer = 0.0f;
    private bool isGliding;
    private bool fromGlide;
    private bool hasGlide;
    private Vector3 moveVector = Vector3.zero;
    private float velocityOnImpact = 0f;
    private bool takeInput = true;
    private bool isCasting = false;

    public bool IsStaggered { get; set; }

    //Achievements
    private GameObject glideTimer;

    

    [Header("Movement")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float animationDamping;
    [SerializeField] private float runAnimationSpeedMultiplier = 1.3f;
    private float movementSpeed;

    [Header("Stagger")]
    [SerializeField] private float staggerSpeed;
    [SerializeField] private float staggerAnimationSlow = 0.4f;
    [SerializeField] private AnimationClip staggerAnimation;
    private float staggerDuration;


    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;

    [Header("Glide")]
    [SerializeField] private float glideSpeed;
    [SerializeField] private float glideStrength;
    [SerializeField] private float glideGravityMultiplier;
    [SerializeField] private float glideDistanceFromGround;


    [Header("Dodge")]
    [SerializeField] private float dodgeLength;
    [SerializeField] private float dodgeDuration;

    [Header("Fall Damage")]
    [SerializeField] private float maxAirTime;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float damageMultiplier;

    [Header("Misc")]
    [SerializeField] private float distanceToGround;
    [SerializeField] private float desiredRotationSpeed;
    [SerializeField] private LayerMask groundcheckMask;
    [SerializeField] private LayerMask waterGroundCheckMask;
   

    //For Debugging
    public GameObject RespawnLocation; // ?

    private float airTime;

    public bool IsDodging { get; set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        Physics.IgnoreLayerCollision(9, 4, true);

        fromGlide = false;
        hasGlide = false;
        airTime = 0f;
    }

    private void Start()
    {
        player = Player.Instance;
        animator = player.Animator;
        playerInput = player.GetComponent<PlayerInput>();
        staggerDuration = staggerAnimation.length;

        //cameraCamCamTheGlam = CameraReference.Instance;
        IsStaggered = false;
    }

    private void Update()
    {
        // Sprint
        if (IsStaggered)
        {
            movementSpeed = staggerSpeed;
        }
        else if (movementInput.y < 0)
        {
            movementSpeed = defaultSpeed / 2;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = runSpeed;
            animator.SetFloat("MovementSpeed", runAnimationSpeedMultiplier);
        }
        else
        {
            movementSpeed = defaultSpeed;
            animator.SetFloat("MovementSpeed", 1f);
        }

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        // Movement
        if ((IsGrounded() || controller.isGrounded) && takeInput)
        {
            moveVector = new Vector3(movementInput.x, 0.0f, movementInput.y);
            moveVector = CameraRelativeFlatten(moveVector, Vector3.up);
            moveVector.Normalize();
            moveVector *= movementSpeed;
        }

        // Dodge
        if (IsDodging)
        {
            if(movementInput.x == 0 && movementInput.y == 0)
            {
                moveVector = new Vector3(0.0f, 0.0f, -1f);
            }
            else
            {
                moveVector = new Vector3(movementInput.x, 0.0f, movementInput.y);
            }
            moveVector.Normalize();
            moveVector = CameraRelativeFlatten(moveVector, Vector3.up);
            moveVector *= dodgeLength;
        }

        // Glide
        else if (isGliding)
        {
            moveVector = new Vector3(movementInput.x, glideStrength, movementInput.y);
            moveVector.Normalize();

            moveVector += Vector3.ProjectOnPlane(CameraReference.Instance.transform.forward, Vector3.up);

            moveVector.Normalize(); 
            moveVector *= glideSpeed;
            fromGlide = true;
            velocityOnImpact = 0;
        }

        // Maybe move this to a separate script?
        if (animator.GetBool("InCombat"))
        {
            faceCameraDirection = true;
        }
        else if (animator.GetBool("InCombat") == false)
        {
            faceCameraDirection = false;
        }
        

        ApplyGravity();

        InputMagnitude();

        CheckFallDamage();

        
        // Remove when the state machine is done
        if (controller.isGrounded && fromGlide)
        {
            print("from glide");
            isGliding = false;
            animator.SetBool("IsGliding", false);

            fromGlide = false;

            //destroy glideTimer so flightExpert never sets to true.
            Destroy(glideTimer);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            OnKnockback();
        }

        // Jump
        // Maybe add "better jump"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGliding && IsGrounded())
            {
                animator.SetTrigger("OnJump");
                moveVector.y = jumpSpeed;
                animator.SetBool("IsGrounded", false);
            }
            else if (!isGliding && !CheckGlideDistance(glideDistanceFromGround) && hasGlide)
            {
                isGliding = true;
                animator.SetBool("IsGliding", true);
                glideTimer = TimerManager.Instance.SetNewTimer(gameObject, 5f, FlightExpertAchieved);
            }
            else if (isGliding && !IsGrounded() && hasGlide)
            {
                isGliding = false;
                animator.SetBool("IsGliding", false);
            }
        }

        // This is what actually moves the character
        controller.Move(moveVector * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the player
    /// </summary>
    private void PlayerRotation()
    {
        Vector3 forward = cameraCamCamTheGlam.transform.forward;
        Vector3 right = cameraCamCamTheGlam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //transform.rotation = Quaternion.LookRotation(forward, Vector3.zero);

        
        if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Entire Body.Sprint"))
        {
            desiredMoveDirection = forward * movementInput.y + right * movementInput.x;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(forward, Vector3.zero);
        }
        
    }

    /// <summary>
    /// Gets the input magnitude from the controller/keyboard and turns that into a float that
    /// is used by the animator to determine which animation to use. (Idle, walk, or run.)
    /// 
    /// If we're not going to implement controller support, we could change/remove this.
    /// </summary>
    private void InputMagnitude()
    {
        animator.SetFloat("InputX", movementInput.x, animationDamping, Time.deltaTime);
        animator.SetFloat("InputZ", movementInput.y, animationDamping, Time.deltaTime);
       
        animationSpeed = new Vector2(movementInput.x, movementInput.y).sqrMagnitude;

        animator.SetFloat("InputMagnitude", animationSpeed, animationDamping, Time.deltaTime);
        
        if(animationSpeed > allowPlayerRotation || animator.GetBool("InCombat"))
        {
            PlayerRotation();
        }

    }

    private void FlightExpertAchieved()
    {
        print("Flight expert unlocked");
        AchievementManager.Instance.FlightExpert = true;
    }

    public void StartCasting()
    {
        isCasting = true;
    }

    public void StopCasting()
    {
        isCasting = false;
    }

    private void OnKnockback()
    {
        moveVector += CameraRelativeFlatten(moveVector += Vector3.back, Vector3.up) * 5f;
    }

    private bool CheckDistanceFromGround(float distance)
    {
        RaycastHit hit;
        bool onGround;
        if (Player.Instance.GetComponent<AbilityManager>().hasWater)
        {
             onGround = Physics.Raycast(transform.position, Vector3.down, out hit, distance, waterGroundCheckMask); //temporary solution
        }
        else
        {
             onGround =  Physics.Raycast(transform.position, Vector3.down, out hit, distance, groundcheckMask);
        }

        if (onGround && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log(hit.collider.name + " " + hit.collider.tag);
            hit.collider.gameObject.GetComponent<EnemySM>().MoveAway(); // No get components in live code like this.
            controller.Move(Vector3.forward * 0.2f);
        }
        
        return onGround;
    }

    private bool IsGrounded()
    {
        if (CheckDistanceFromGround(distanceToGround))
        {
            animator.SetBool("IsGrounded", true);
            return true;
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            return false;
        }
    }

    private bool CheckGlideDistance(float distance)
    {
        return Physics.Raycast(transform.position, CameraRelativeFlatten(new Vector3(0f, -1f, 0.7f).normalized, Vector3.up), distance, waterGroundCheckMask);
    }

    private void ApplyGravity()
    {
        if (isGliding)
        {
            moveVector.y -= gravity * glideGravityMultiplier;
        }

        // Invert this. We don't want to always check for the scenario that occurs the most often
        else if (CheckDistanceFromGround(0.05f) && moveVector.y < 0.001f)
        {
            moveVector.y = -gravity * Time.deltaTime;
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;
        }

    }

    //Flytta ut
    private void CheckFallDamage()
    {
        
        if (!IsGrounded())
        {
            airTime += Time.deltaTime;
            if(moveVector.y < velocityOnImpact)
            {
                velocityOnImpact = moveVector.y;
            }
        }
        
        else if(IsGrounded() && airTime > maxAirTime && velocityOnImpact < -maxVelocity)
        {
            velocityOnImpact *= damageMultiplier;
            player.Health.CurrentHealth -= Mathf.Abs((int)velocityOnImpact);
            airTime = 0;
        }

        else
        {
            airTime = 0;
            velocityOnImpact = 0;
        }
    }
    
    
    public void OnDodge()
    {
        if (IsGrounded())
        {
            IsDodging = true;
            TimerManager.Instance.SetNewTimer(gameObject, dodgeDuration, ExitDodge);
            player.Animator.SetTrigger("OnDodge");
            EventManager.Instance.FireEvent(new DodgeEvent("stamina drained", 15));
            takeInput = false;
        }
    }

    private void ExitDodge()
    {
        IsDodging = false;
        dodgeTimer = 0.0f;
        moveVector.x = 0.0f;
        moveVector.z = 0.0f;
        takeInput = true;
    }

    //slow down when hit, called frpm dmageEventListener
    public void SlowDown()
    {
        if (!IsStaggered && !isCasting)
        {
            EventManager.Instance.FireEvent(new StaggerEvent("staggered", gameObject));
            Debug.Log("PLAYER IS STAGGERED");
            player.Animator.SetTrigger("OnStagger");
            IsStaggered = true;
            TimerManager.Instance.SetNewTimer(gameObject, staggerDuration/3, Recover);
            animator.speed -= staggerAnimationSlow;
            //GetComponent<WeaponController>().SetSwordDisabled();
        }


    }

    public void Recover()
    {
        IsStaggered = false;
        animator.speed += staggerAnimationSlow;
        animator.SetBool("CanBlock", true);
        //playerInput.AttackTimerEnd();
        
        Debug.Log("PLAYER RECOVERED FROM STAGGER");
    }

    //Code from: https://gamedev.stackexchange.com/questions/125945/camera-relative-movement-is-pushing-into-off-the-ground-instead-of-parallel/125954#125954?newreg=bdca6bbf7889474cbb8e7eabbfd2f130
    Vector3 CameraRelativeFlatten(Vector3 input, Vector3 localUp)
    {
        Quaternion flatten = Quaternion.LookRotation(-localUp, cameraCamCamTheGlam.transform.forward) * Quaternion.Euler(Vector3.right * -90f);
        return flatten * input;
    }

    //Gör property i ability manager eller nåt bättre (?)
    public void ActivateGlide()
    {
        hasGlide = true;
    }

    // Used by killzones to move the player
    public void MoveTo(Vector3 position)
    {
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
    }
}
