using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.transform.root.GetComponentInChildren<PlayerMovement>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }
}