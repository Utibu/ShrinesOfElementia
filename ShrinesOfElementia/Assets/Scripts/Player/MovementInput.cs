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


    //[SerializeField] private bool isGrounded;   Old code, didn't work. Keeping just in case.
    private Player player;

    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;

    [Header("Glide")]
    [SerializeField] private float glideSpeed;
    [SerializeField] private float glideStrength;
    [SerializeField] private float glideGravityMultiplier;

    [Header("Movement Speed")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float runSpeed;

    [Header("Dodge")]
    [SerializeField] private float dodgeLength;
    [SerializeField] private float dodgeDuration;

    [Header("Misc")]
    [SerializeField] private float distanceToGround;
    [SerializeField] private float desiredRotationSpeed;



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
    }

    private void Update()
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
            moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
            moveVector.y = saveY; // test

            moveVector.Normalize(); 
            moveVector *= glideSpeed;
            fromGlide = true;
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
            else if (!isGliding && !IsGrounded() && hasGlide)
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

        //print(moveVector);
        controller.Move(moveVector * Time.deltaTime);
        //Debug.Log(animator.GetBool("IsGrounded"));
    }

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


        animator.SetFloat("InputZ", playerInput.y);
        animator.SetFloat("InputX", playerInput.x);
        
        

        speed = new Vector2(playerInput.x, playerInput.y).sqrMagnitude;
        //print(speed);

        animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        
        if(speed > allowPlayerRotation || animator.GetBool("InCombat"))
        {
            PlayerMoveAndRotation();
        }

    }

    private bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distanceToGround))
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
            print(moveVector.magnitude); 
            moveVector.y -= gravity * glideGravityMultiplier;
        }
        else if (IsGrounded() && moveVector.y < 0.5f)
        {
            //animator.SetBool("IsGrounded", true);   Jumping animation (not good)
            moveVector.y = -gravity * Time.deltaTime;
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;
        }

    }

    
    public void OnDodge()
    {
        if (IsGrounded())
        {
            isDodging = true;
            player.Animator.SetTrigger("OnDodge");
            EventManager.Current.FireEvent(new StaminaDrainEvent("stamina drained", 15));
        }
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
