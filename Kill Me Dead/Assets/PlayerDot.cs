using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDot : MonoBehaviour
{
    [SerializeField] GameObject normalDot;
    [SerializeField] GameObject zipDot;

    private void Start() 
    {
        ChangeDot(false);    
    }

    public void ChangeDot(bool canZip)
    {
        if (canZip)
        {
            normalDot.SetActive(false);
            zipDot.SetActive(true);
        }

        else if (!canZip)
        {
            normalDot.SetActive(true);
            zipDot.SetActive(false);
        }
    }
}