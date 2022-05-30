using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    InputMaster inputMaster;

    PlayerMovement playerMovement;
    PlayerView playerView;

    MeleeAttack meleeAttack;
    PlayerZipAttack playerZipAttack;

    private void Awake()
    {
        inputMaster = new InputMaster();

        playerMovement = GetComponent<PlayerMovement>();
        playerView = GetComponent<PlayerView>();

        meleeAttack = GetComponent<MeleeAttack>();
        playerZipAttack = GetComponent<PlayerZipAttack>();
    }

    private void Update() 
    {   
        playerMovement.GetMovementInput(inputMaster.Player.Movement.ReadValue<Vector2>());
        playerMovement.GetSprintInput(inputMaster.Player.Sprint.IsPressed());
        
        playerView.GetMouseInput(inputMaster.Player.View.ReadValue<Vector2>());

        inputMaster.Player.Jump.performed += context => playerMovement.Jump();
        inputMaster.Player.AttackWithMelee.performed += context => meleeAttack.PerformMeleeAttack();
        inputMaster.Player.Zip.performed += context => playerZipAttack.Zip(); 
    }

    private void OnEnable()
    {
        inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
    }
}
