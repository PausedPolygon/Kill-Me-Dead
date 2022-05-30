using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isAlive;
    [SerializeField] GameObject normalDummy;
    [SerializeField] GameObject ragdollDummy;
    [SerializeField] GameObject enemyGun;

    Animator animator;
    Rigidbody rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();   
    }

    private void Start() 
    {
        isAlive = true;
        animator.SetBool("isAlive", isAlive);    
    }

    public void Die()
    {
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        normalDummy.SetActive(false);
        ragdollDummy.SetActive(true);
        enemyGun.SetActive(false);

        rb.AddForce(transform.up * 10, ForceMode.Impulse);
    }
}