using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Object Cache")]
    public Transform target;
    [SerializeField] GameObject enemyView;
    
    [Header("Range Settings")]
    [SerializeField] float provokableRange = 7.5f;
    [SerializeField] float attackRange = 12f;
    [SerializeField] float ignoranceRange = 18f;

    [Header("Read for Debug")]
    [SerializeField] bool isProvoked;
    [SerializeField] bool isAttacking;

    [SerializeField] float rotationDamping = 20;

    public Transform zipPosition;

    bool isMoving;

    EnemyAttack enemyAttack;
    EnemyHealth enemyHealth;
    NavMeshAgent navMeshAgent;
    Animator animator;

    public void SetProvokedStatus(bool provokedStatus)
    {
        isProvoked = provokedStatus;
    }

    private void Awake() 
    {
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        isProvoked = false;     
    }

    private void Update() 
    {
        if (enemyHealth.isAlive)
        {
            // gets distance to the current target
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);

            // provokes enemy
            if (targetDistance <= provokableRange)
            {
                isProvoked = true;
            }

            // ignores enemy
            if (targetDistance > ignoranceRange)
            {
                isProvoked = false;
            }

            // handle enemy attacking
            if (isProvoked)
            {
                var lookPos = target.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
                
                // attacks target
                if (targetDistance <= attackRange)
                {
                    navMeshAgent.isStopped = true;
                    isMoving = false;
                    animator.SetBool("isMoving", isMoving);
                    isAttacking = true;
                    animator.SetBool("isAttacking", isAttacking);
                    // enemyAttack.AttackSetup(true, target.position);
                }

                // can't attack and pursue target to attackable range
                else
                {
                    navMeshAgent.isStopped = false;
                    isMoving = true;
                    animator.SetBool("isMoving", isMoving);
                    isAttacking = false;
                    animator.SetBool("isAttacking", isAttacking);

                    // enemyAttack.AttackSetup(false, target.position);
                    navMeshAgent.destination = target.position;
                }
            }

            // returns enemy to idle
            if (!isProvoked)
            {
                navMeshAgent.isStopped = true;
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
                isAttacking = false;
                animator.SetBool("isAttacking", isAttacking);
            }
        }
    }

    // range visuals for debug
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, provokableRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ignoranceRange);
    }
}