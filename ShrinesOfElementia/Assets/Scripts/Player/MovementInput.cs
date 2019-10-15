using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{

    private float inputX;
    private float inputZ;
    private Vector3 desiredMoveDirection;
    [SerializeField]private bool blockRotationPlayer;
    [SerializeField]private float desiredRotationSpeed;
    private Animator animator;
    private float speed;
    private float allowPlayerRotation;
    [SerializeField]private Camera camera;
    private CharacterController controller;
    private bool isGrounded;
    private Player player;

    [SerializeField]private float verticalVelocity;
    private Vector3 moveVector;


    private void Start()
    {
        player = Player.Instance;
        animator = player.Animator;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputMagnitude();
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVelocity = 0;
        }
        else
        {
            verticalVelocity -= 9.8f * Time.deltaTime;
        }

        moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector);
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
}
