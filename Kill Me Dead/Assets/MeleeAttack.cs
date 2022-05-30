using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] MeleeWeapon meleeWeapon; 
    [SerializeField] bool isAttackingWithMelee;

    Animator animator;

    int random;

    private void Awake() 
    {
        animator = transform.root.GetComponent<Animator>();
    }

    public bool GetAttackingWithMeleeStatus() { return isAttackingWithMelee; }

    public void PerformMeleeAttack()
    {
        if (meleeWeapon.canAttack)
        {
            if (!isAttackingWithMelee)
            {
                isAttackingWithMelee = true;
                PlayAnimation(true);
                StartCoroutine(meleeWeapon.AttackWithMelee(this));
            }
        }
    }

    public void EndMeleeAttack()
    {
        isAttackingWithMelee = false;
        PlayAnimation(false);
    }

    private void PlayAnimation(bool shouldGetNewRandom)
    {
        if (shouldGetNewRandom)
        {
            random = Random.Range(1, 3);
        }

        if (random == 1)
            animator.SetBool("isAttacking", isAttackingWithMelee);

        else if (random == 2)
            animator.SetBool("isAttacking2", isAttackingWithMelee);
    }
}