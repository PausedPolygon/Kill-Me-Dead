using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float jumpForceMultiplier = 2f;
    public bool canZip;

    [SerializeField] PlayerKatanaVisual playerKatanaVisual;

    PlayerMovement playerMovement;

    private void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();    
    }

    private void Start() 
    {
        ReturnToDefault();
    }

    public void ReturnToDefault()
    {
        playerMovement.sprintSpeed = playerMovement.defaultSpringSpeed;
        playerMovement.jumpForce = playerMovement.defaultJumpForce;
        canZip = false;
    }

    public void InitiateSpeedState()
    {
        playerMovement.sprintSpeed *= speedMultiplier;
        playerKatanaVisual.SpeedState();
    }

    public void InitiateJumpState()
    {
        playerMovement.jumpForce *= jumpForceMultiplier;
        playerKatanaVisual.JumpState();
    }

    public void InitiateZipState()
    {
        canZip = true;
        playerKatanaVisual.ZipState();
    }
}