// Main Author: Joakim Ljung
// Co-Authors: Sofia Kauko, Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{

    private Vector2 playerInput;
    private Vector3 desiredMoveDirection;
    private bool faceCameraDirection;
    public bool FaceCameraDirection { set { faceCameraDirection = value; } }
    private Animator animator;
    private float speed;
    private float allowPlayerRotation;
    private CameraReference camera;
    private CharacterController controller;
    private float dodgeTimer = 0.0f;
    private bool isDodging;
    private bool isGliding;
    private bool fromGlide;
    private bool hasGlide;
    private Vector3 moveVector = Vector3.zero;
    float velocityOnImpact = 0;

    //just temporary placement
    private bool isPushed;
    private float pushTimer;
    private Vector3 pushVector;



    //[SerializeField] private bool isGrounded;   Old code, didn't work. Keeping just in case.
    private Player player;

    [Header("Movement")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float animationDamping;
    [SerializeField] private bool newMovement;
    private float movementSpeed;

    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;

    [Header("Glide")]
    [SerializeField] private float glideSpeed;
    [SerializeField] private float glideStrength;
    [SerializeField] private float glideGravityMultiplier;
    [SerializeField] private float glideDistanceFromGround;


    [Header("Movement Speed")]

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
    public GameObject RespawnLocation;



    private float airTime;

    public float DefaultSpeed { get { return defaultSpeed; } }
    public float RunSpeed { get { return runSpeed; } }
    public bool IsDodging { get { return isDodging; } }

    private bool breakUpdate;
    private bool takeInput = true;
    public bool TakeInput { set { takeInput = value; } }



    private void Start()
    {
        player = Player.Instance;
        animator = player.Animator;
        camera = CameraReference.Instance;
        controller = GetComponent<CharacterController>();
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Physics.IgnoreLayerCollision(9, 4, true);
        fromGlide = false;
        breakUpdate = false;
        hasGlide = false;
        airTime = 0;
        
    }

    private void Update()
    {
        //Temporary garbage
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = runSpeed;
        }
        else
        {
            if (true)
            {
                movementSpeed = defaultSpeed;
            }
            

        }//

        Vector3 savedValues = new Vector3(playerInput.x, 0.0f, playerInput.y);
        if (newMovement)
        {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        //more test stuff
        //playerInput.x = Mathf.Lerp(savedValues.x, playerInput.x, 0.1f);
        //playerInput.y = Mathf.Lerp(savedValues.y, playerInput.y, 0.1f);

        //TEST STUFF
        if ((IsGrounded() || controller.isGrounded) && newMovement)
        {
            moveVector = new Vector3(playerInput.x, 0.0f, playerInput.y);
            moveVector = CameraRelativeFlatten(moveVector, Vector3.up);
            moveVector.Normalize();
            //moveVector = camera.transform.TransformDirection(moveVector);
            //moveVector.y = 0.0f;
            //moveVector.Normalize();
            moveVector *= movementSpeed;
        }

        if (isDodging)
        {
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer >= dodgeDuration)
            {
                isDodging = false;
                dodgeTimer = 0.0f;
                moveVector.x = 0.0f;
                moveVector.z = 0.0f;
            }
            else
            {
                if(playerInput.x == 0 && playerInput.y == 0)
                {
                    moveVector = new Vector3(0.0f, 0.0f, -1f);
                    Vector3 fixThis = player.transform.forward;
                }
                else
                {
                    moveVector = new Vector3(playerInput.x, 0.0f, playerInput.y);
                }
                moveVector.Normalize();
                moveVector = CameraRelativeFlatten(moveVector, Vector3.up);
                moveVector *= dodgeLength;
            }
        }

        else if (isGliding)
        {
            moveVector = new Vector3(playerInput.x, glideStrength, playerInput.y);
            moveVector.Normalize();

            
            float saveY = moveVector.y; // test
            //moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
            moveVector += Vector3.ProjectOnPlane(CameraReference.Instance.transform.forward, Vector3.up);
            //moveVector.y = saveY; // test

            moveVector.Normalize(); 
            moveVector *= glideSpeed;
            fromGlide = true;
            velocityOnImpact = 0;
        }

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

        

        if (IsGrounded() && fromGlide)
        {
            isGliding = false;
            animator.SetBool("IsGliding", false);
            //moveVector.x = 0;
            //moveVector.z = 0;

            fromGlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGliding && IsGrounded())
            {
                animator.SetTrigger("OnJump");
                moveVector.y = jumpSpeed;
                animator.SetBool("IsGrounded", false);   //Jumping animation (not good)
                //too little too late
            }
            else if (!isGliding && !CheckDistanceFromGround(glideDistanceFromGround) && hasGlide)
            {
                isGliding = true;
                animator.SetBool("IsGliding", true);
            }
            else if (isGliding && !IsGrounded() && hasGlide)
            {
                isGliding = false;
                animator.SetBool("IsGliding", false);
            }
        }


        //temporary stuff to prevent crowdsurf.
        if (isPushed)
        {
            pushTimer -= Time.deltaTime;
            if (pushTimer <= 0)
            {
                isPushed = false;
                //moveVector -= pushVector; 
            }
            else if (pushTimer > 0)
            {
                //moveVector += pushVector * Time.deltaTime;
                gameObject.transform.position += pushVector * Time.deltaTime;
            }
        }

        //print(moveVector);


        controller.Move(moveVector * Time.deltaTime);
        //Debug.Log(animator.GetBool("IsGrounded"));
    }
    /*
    #region BilalMovementInput
    public void UpdateMovementInput()
    {
        playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //TEST STUFF
        //moveVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //moveVector.Normalize();
        //moveVector = camera.transform.TransformDirection(moveVector);
        //moveVector.y = 0.0f;
        //moveVector *= runSpeed;
        //print(moveVector);


        if (isDodging)
        {
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer >= dodgeDuration)
            {
                isDodging = false;
                dodgeTimer = 0.0f;
                moveVector.x = 0.0f;
                moveVector.z = 0.0f;
            }
            else
            {
                moveVector = new Vector3(playerInput.x, 0.0f, playerInput.y);
                moveVector.Normalize();
                moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
                moveVector.y = 0.0f;
                moveVector *= dodgeLength;
            }
        }

        else if (isGliding)
        {
            moveVector = new Vector3(playerInput.x, glideStrength, playerInput.y);
            moveVector.Normalize();


            float saveY = moveVector.y; // test
            //moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
            moveVector += Vector3.ProjectOnPlane(CameraReference.Instance.transform.forward, Vector3.up);
            //moveVector.y = saveY; // test

            moveVector.Normalize();
            moveVector *= glideSpeed;
            fromGlide = true;
            velocityOnImpact = 0;
        }

        if (animator.GetBool("InCombat"))
        {
            faceCameraDirection = true;
        }
        else if (animator.GetBool("InCombat") == false)
        {
            faceCameraDirection = false;
        }

        InputMagnitude();

        ApplyGravity();

        CheckFallDamage();




        if (IsGrounded() && fromGlide)
        {
            isGliding = false;
            animator.SetBool("IsGliding", false);
            moveVector.x = 0;
            moveVector.z = 0;
            fromGlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGliding && IsGrounded())
            {
                animator.SetTrigger("OnJump");
                moveVector.y = jumpSpeed;
                //animator.SetBool("IsGrounded", false);   Jumping animation (not good)
            }
            else if (!isGliding && !CheckDistanceFromGround(glideDistanceFromGround) && hasGlide)
            {
                isGliding = true;
                animator.SetBool("IsGliding", true);
            }
            else if (isGliding && !IsGrounded() && hasGlide)
            {
                isGliding = false;
                animator.SetBool("IsGliding", false);
            }
        }


        //temporary stuff to prevent crowdsurf :)).
        if (isPushed)
        {
            pushTimer -= Time.deltaTime;
            if (pushTimer <= 0)
            {
                isPushed = false;
                //moveVector -= pushVector; 
            }
            else if (pushTimer > 0)
            {
                //moveVector += pushVector * Time.deltaTime;
                gameObject.transform.position += pushVector * Time.deltaTime;
            }
        }

        //print(moveVector);
        controller.Move(moveVector * Time.deltaTime);
        //Debug.Log(animator.GetBool("IsGrounded"));
    }
    #endregion BilalMovementInput
    */


    private void PlayerMoveAndRotation()
    {
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        
        if (animator.GetBool("InCombat"))
        {
            transform.rotation = Quaternion.LookRotation(forward, Vector3.zero);

        }
        else
        {
            desiredMoveDirection = forward * playerInput.y + right * playerInput.x;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

    }

    private void InputMagnitude()
    {
        

        //print("X: " + inputX + "Z: " + inputZ);
        //print(player.Animator.GetFloat("InputX") + " " + player.Animator.GetFloat("InputZ"));


        animator.SetFloat("InputX", playerInput.x, animationDamping, Time.deltaTime);
        animator.SetFloat("InputZ", playerInput.y, animationDamping, Time.deltaTime);
        
        

        speed = new Vector2(playerInput.x, playerInput.y).sqrMagnitude;
        //print(speed);

        animator.SetFloat("InputMagnitude", speed, animationDamping, Time.deltaTime);
        
        if(speed > allowPlayerRotation || animator.GetBool("InCombat"))
        {
            PlayerMoveAndRotation();
        }

    }
    private bool CheckDistanceFromGround(float distance)
    {
        if (Player.Instance.GetComponent<AbilityManager>().hasWater)
        {
            return Physics.Raycast(transform.position, Vector3.down, distance, waterGroundCheckMask); //temporary solution

        }
        else
        {
            return Physics.Raycast(transform.position, Vector3.down, distance, groundcheckMask);
        }
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

    private void ApplyGravity()
    {
        if (isGliding)
        {
            moveVector.y -= gravity * glideGravityMultiplier;
        }
        else if (CheckDistanceFromGround(0.05f) && moveVector.y < 0.001f)
        {
            //animator.SetBool("IsGrounded", true);   //Jumping animation (not good)
            moveVector.y = -gravity * Time.deltaTime;
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;
        }

    }

    
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
            isDodging = true;
            player.Animator.SetTrigger("OnDodge");
            EventManager.Current.FireEvent(new DodgeEvent("stamina drained", 15));
        }
    }

    
    public void AddPush(Vector3 push)
    {
        if(isPushed == false)
        {
            isPushed = true;
            pushTimer = 1.5f;
            pushVector = push;
        }
        return;
    }

    //Code from: https://gamedev.stackexchange.com/questions/125945/camera-relative-movement-is-pushing-into-off-the-ground-instead-of-parallel/125954#125954?newreg=bdca6bbf7889474cbb8e7eabbfd2f130
    Vector3 CameraRelativeFlatten(Vector3 input, Vector3 localUp)
    {
        Quaternion flatten = Quaternion.LookRotation(-localUp, camera.transform.forward) * Quaternion.Euler(Vector3.right * -90f);
        return flatten * input;
    }

    public void ActivateGlide()
    {
        hasGlide = true;
    }

    public void MoveTo(Vector3 position)
    {
        breakUpdate = true;
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
    }
}
