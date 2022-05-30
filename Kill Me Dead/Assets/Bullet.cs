using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    Rigidbody rb;
    EnemyAI enemyAI;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        enemyAI = GameObject.FindObjectOfType<EnemyAI>();    
    }

    private void Start() 
    {
        BeABullet();
    }

    private void BeABullet()
    {
        rb.velocity = transform.forward * bulletSpeed;
        // rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) 
    {
        PlayerHealth playerHealth;
        if (other.transform.parent.TryGetComponent<PlayerHealth>(out playerHealth))
        {
            playerHealth.TakeDamage();
        }

        Destroy(gameObject);
    }
}