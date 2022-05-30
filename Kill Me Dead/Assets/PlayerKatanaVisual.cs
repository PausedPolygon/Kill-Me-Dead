using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaVisual : MonoBehaviour
{
    [SerializeField] GameObject normal;
    [SerializeField] GameObject green;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject orange;

    private void Start() 
    {
        TurnOff();

        normal.SetActive(true);
        green.SetActive(false);
        blue.SetActive(false);
        orange.SetActive(false);   
    }

    public void SpeedState()
    {
        TurnOff();

        normal.SetActive(false);
        green.SetActive(true);
        blue.SetActive(false);
        orange.SetActive(false);
    }

    public void JumpState()
    {
        TurnOff();

        normal.SetActive(false);
        green.SetActive(false);
        blue.SetActive(true);
        orange.SetActive(false);
    }

    public void ZipState()
    {
        TurnOff();

        normal.SetActive(false);
        green.SetActive(false);
        blue.SetActive(false);
        orange.SetActive(true);
    }

    private void TurnOff()
    {
        normal.SetActive(false);
        green.SetActive(false);
        blue.SetActive(false);
        orange.SetActive(false);
    }
}
