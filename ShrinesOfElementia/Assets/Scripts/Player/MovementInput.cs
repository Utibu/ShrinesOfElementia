using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joakim Ljung
public class MovementInput : MonoBehaviour
{

    private float inputX;
    private float inputZ;
    [SerializeField] private Vector3 desiredMoveDirection;
    [SerializeField] private bool faceCameraDirection;
    public bool FaceCameraDirection { set { faceCameraDirection = value; } }
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

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float runSpeed;

    public float DefaultSpeed { get { return defaultSpeed; } }
    public float RunSpeed { get { return runSpeed; } }


    private void Start()
    {
        player = Player.Instance;
        animator = player.Animator;
        camera = CameraReference.Instance;
        controller = GetComponent<CharacterController>();
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (faceCameraDirection)
        {
            animator.SetBool("InCombat", true);
        }

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
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        
        if (faceCameraDirection)
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
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //print("X: " + inputX + "Z: " + inputZ);
        print(player.Animator.GetFloat("InputX") + " " + player.Animator.GetFloat("InputZ"));


        animator.SetFloat("InputZ", inputZ);
        animator.SetFloat("InputX", inputX);
        
        

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
        //print(speed);

        animator.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        
        if(speed > allowPlayerRotation || faceCameraDirection)
        {
            PlayerMoveAndRotation();
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
