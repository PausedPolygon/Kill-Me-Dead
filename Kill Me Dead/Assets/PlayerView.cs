using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Mouse Sensitivity Settings")]
    [SerializeField] float xSensitivity = 300f;
    [SerializeField] float ySensitivity = 300f;

    [Header("Read for Debug")]
    public bool isViewActive;

    // automatic object references
    [SerializeField] Transform playerView;
    [SerializeField] Transform playerOrientation;

    float xRotation;
    float yRotation;

    float multiplier = 0.01f;

    private void Start() 
    {
        // lock cursor to the center of the screen, makes it invisible
        Cursor.lockState = CursorLockMode.Locked;

        isViewActive = true;
    }

    private void Update() 
    {
        // set rotation of camera container and player orientation objects respectively
        if (isViewActive)
        {
            playerView.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            playerOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    public void GetMouseInput(Vector2 viewInputVector)
    {
        // sets mouse position values to received input
        float mouseX = viewInputVector.x;
        float mouseY = viewInputVector.y;

        yRotation += mouseX * xSensitivity * multiplier;
        xRotation -= mouseY * ySensitivity * multiplier;

        // limits player's look angle
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
    }

    public void ToggleView()
    {
        isViewActive = !isViewActive;
    }
}