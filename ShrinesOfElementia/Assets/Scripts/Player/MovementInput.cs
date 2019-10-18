using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joakim Ljung
public class MovementInput : MonoBehaviour
{

    private float inputX;
    private float inputZ;
    [SerializeField] private Vector3 desiredMoveDirection;
    [SerializeField] private bool blockRotationPlayer;
    [SerializeField] private float desiredRotationSpeed;
    private Animator animator;
    private float speed;
    private float allowPlayerRotation;
    private CameraReference camera;
    private CharacterController controller;
    //[SerializeField] private bool isGrounded;   Old code, didn't work. Keeping just in case.
    private Player player;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private Vector3 moveVector = Vector3.zero;
    [SerializeField] private float distanceToGround;


    private void Start()
    {
        player = Player.Instance;
        animator = player.Animator;
        camera = CameraReference.Instance;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputMagnitude();
        //isGrounded = controller.isGrounded;   Old code, didn't work. Keeping just in case.
        if (IsGrounded() && moveVector.y < 0.5f)
        {
            //animator.SetBool("IsGrounded", true);   Jumping animation (not good)

            moveVector.y = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveVector.y = jumpSpeed;
                //animator.SetBool("IsGrounded", false);   Jumping animation (not good)
            }
            
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;

        }
        controller.Move(moveVector * Time.deltaTime);
        //Debug.Log(animator.GetBool("IsGrounded"));
    }

    private void PlayerMoveAndRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;

        if(blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
    }

    private void InputMagnitude()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        animator.SetFloat("InputZ", inputZ, 0.0f, Time.deltaTime * 2f);
        animator.SetFloat("InputX", inputX, 0.0f, Time.deltaTime * 2f);

        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        if(speed > allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if(speed < allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distanceToGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
