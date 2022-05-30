using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyGun enemyGun;

    public void Attack()
    {
        enemyGun.FireGun();
    }
}