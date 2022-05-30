using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float playerHeight = 2f;
    [SerializeField] Transform orientation;

    [Header("Movement Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] bool isMoving;
    [SerializeField] bool isSprinting;
    public float defaultSpringSpeed = 7f;
    public float sprintSpeed = 7f;
    [Tooltip("Transition between current and desired movement speed.")] [SerializeField] float acceleration = 20f;

    [Header("Ground Settings")]
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] Transform groundCheckPosition;
    [Tooltip("Radius of the isGrounded checking sphere.")] [SerializeField] float groundCheckDistance = 0.2f;
    [Tooltip("Layer mask type that counts as standable ground for the player.")] [SerializeField] LayerMask walkableMask;
    [SerializeField] bool onSlope;
    [SerializeField] float maxSlopeAngle = 40f;

    [Header("Jump Settings")]
    public float defaultJumpForce = 12f;
    public float jumpForce = 12f;

    [Header("Air Settings")]
    [Tooltip("Amount of new input force affects player while airborne")] [SerializeField] float airMultiplier = 0.3f;
    [SerializeField] float airDrag = 2f;
    
    float movementMultiplier = 10f;
    Vector3 moveDirection;

    float horizontalMovement;
    float verticalMovement;

    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Start() 
    {
        rb.freezeRotation = true;    
    }

    private void Update()
    {
        ControlSpeed();
        ControlDrag();

        CheckSlope();
        CheckGround();

        rb.useGravity = !onSlope;
    }

    private void FixedUpdate() 
    {
        MovePlayer();    
    }

    public void GetMovementInput(Vector2 moveInputVector)
    {
        // checks if player is giving any movement input
        if (moveInputVector == new Vector2(0, 0))
            isMoving = false;
        else
            isMoving = true;

        horizontalMovement = moveInputVector.x;
        verticalMovement = moveInputVector.y;

        // calculate and set movement direction value based on axis values 
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    public void GetSprintInput(bool sprintInput)
    {
        isSprinting = sprintInput;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            // adds upwards jump force to the player
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void MovePlayer()
    {
        // adds force to player in input direction on a flat surface
        if (isGrounded && !onSlope)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        // adds force to player in input direction on a sloped surface
        else if (isGrounded && onSlope)
        {
            rb.AddForce(slopeMoveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        // adds force to player in input direction while in the air
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void ControlSpeed()
    {
        // transitions current movement speed to sprint speed 
        if (isSprinting && isGrounded)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }

        // transitions current movement speed to walk speed
        else if (isMoving)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, acceleration * Time.deltaTime);
        }

        // transitions current movment speed to a halt
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, 0f, acceleration * Time.deltaTime);
        }
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }

        else if (!isGrounded)
        {
            rb.drag = airDrag;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckDistance, walkableMask);
    }

    private void CheckSlope()
    {
        // sends phyrics raycast down to the surface under the player, caches normal of that surface
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);

            // checks if surface is sloped and if the angle is under the max slope angle
            if (slopeHit.normal != Vector3.up && slopeAngle < maxSlopeAngle)
            {
                onSlope = true;
            }

            else
            {
                onSlope = false;
            }
        }

        else
        {
            onSlope = false;
        }

        // cache vector3 position of vector located on the slope
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
}
