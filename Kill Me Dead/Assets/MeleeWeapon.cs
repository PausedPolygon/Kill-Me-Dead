using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] float attackDuration = 0.20f;
    [SerializeField] float attackCooldown = 0.25f;

    [Header("Object References")]
    Collider attackTriggerBox;

    public bool canAttack;
    
    MeleeAttack attackOrigin;

    public float GetAttackDuration() { return attackDuration; }

    private void Awake() 
    {
        attackTriggerBox = GetComponent<Collider>();    
    }

    private void Start() 
    {
        ResetAttack();
    }

    public IEnumerator AttackWithMelee(MeleeAttack meleeAttack)
    {
        // reference to object where the attack was called from
        this.attackOrigin = meleeAttack;

        if (canAttack)
        {
            // does attack
            canAttack = false;
            attackTriggerBox.enabled = true;

            yield return new WaitForSeconds(attackDuration);

            // finishes attack
            attackTriggerBox.enabled = false;
            
            if (attackOrigin != null)
                attackOrigin.EndMeleeAttack();

            yield return new WaitForSeconds(attackCooldown);
            ResetAttack();
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        // makes sure the attacker doesn't damage itself
        if (other.transform.root != transform.root)
        {
            EnemyHealth enemyHealth;
            if (other.TryGetComponent<EnemyHealth>(out enemyHealth))
            {
                enemyHealth.Die();
            }
        }
    }
}