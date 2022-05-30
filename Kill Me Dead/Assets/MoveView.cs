using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveView : MonoBehaviour
{
    [SerializeField] Transform viewPosition;

    private void Update() 
    {
        transform.position = viewPosition.position;    
    }  
}