using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    int defaultPlayerLives = 1;
    [SerializeField] int playerLives = 1;

    PlayerStateHandler playerStateHandler;
    [SerializeField] TextMeshProUGUI healthTextDisplay;

    public bool canTakeDamage = true;

    private void Awake() 
    {
        playerStateHandler = GetComponent<PlayerStateHandler>();    
    }

    private void Start() 
    {
        playerLives = defaultPlayerLives;
        UpdateUI();
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            playerLives--;
            UpdateUI();

            if (playerLives == 0)
            {
                playerStateHandler.InitiateSpeedState();
            }

            else if (playerLives == -1)
            {
                playerStateHandler.InitiateJumpState();
            }

            else if (playerLives == -2)
            {
                playerStateHandler.InitiateZipState();
            }

            else if (playerLives == -3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void UpdateUI()
    {
        healthTextDisplay.text = playerLives.ToString();
    }
}