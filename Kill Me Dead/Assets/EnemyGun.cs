using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] Transform barrelExitPoint;
    [SerializeField] GameObject bullet;

    EnemyAI enemyAI;

    private void Awake() 
    {
        enemyAI = transform.root.GetComponentInChildren<EnemyAI>();    
    }

    public void FireGun()
    {
        Vector3 aimDirection = (enemyAI.target.position - barrelExitPoint.position).normalized;
        Instantiate(bullet, barrelExitPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
    }
}