﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joakim Ljung
public class MovementInput : MonoBehaviour
{

    private float inputX;
    private float inputZ;
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
        if (takeInput)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            if (breakUpdate == true)
            {
                breakUpdate = false;
                return;
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
                    moveVector = new Vector3(inputX, 0.0f, inputZ);
                    moveVector.Normalize();
                    moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
                    moveVector.y = 0.0f;
                    moveVector *= dodgeLength;
                    
                }

            }

            else if (isGliding)
            {
                print("gliding");
                moveVector = new Vector3(inputX, glideStrength, inputZ);
                moveVector.y += 10;
                moveVector.Normalize();
                moveVector = CameraReference.Instance.transform.TransformDirection(moveVector);
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

            //isGrounded = controller.isGrounded;   Old code, didn't work. Keeping just in case.
            if (IsGrounded() && moveVector.y < 0.5f)
            {
                //animator.SetBool("IsGrounded", true);   Jumping animation (not good)
                moveVector.y = -gravity * Time.deltaTime;
            }

          
            else
            {
                moveVector.y -= gravity * Time.deltaTime;
            }

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
            controller.Move(moveVector * Time.deltaTime);
        }
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
            desiredMoveDirection = forward * inputZ + right * inputX;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

    }

    private void InputMagnitude()
    {
        

        //print("X: " + inputX + "Z: " + inputZ);
        //print(player.Animator.GetFloat("InputX") + " " + player.Animator.GetFloat("InputZ"));


        animator.SetFloat("InputZ", inputZ);
        animator.SetFloat("InputX", inputX);
        
        

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
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

    
    public void OnDodge()
    {
        if (IsGrounded())
        {
            isDodging = true;
            player.Animator.SetTrigger("OnDodge");
            EventSystem.Current.FireEvent(new StaminaDrainEvent("stamina drained", 15));
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
