using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public float attackCooldown = 0.5f;
    public AudioSource attackSound;

    private float nextAttackTime = 0f;

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime) {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    void Attack()
    {
        if (attackSound != null)
        {
            attackSound.Play();
        }

        // Detect all enemies within the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Handle Skeletons
            SkeletonHealth skeleton = enemy.GetComponent<SkeletonHealth>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(attackDamage);
                Debug.Log("Hit Skeleton for " + attackDamage + " damage!");
                continue;
            }

            // Handle Goblins
            GoblinHealth goblin = enemy.GetComponent<GoblinHealth>();
            if (goblin != null)
            {
                goblin.TakeDamage(attackDamage);
                Debug.Log("Hit Goblin for " + attackDamage + " damage!");
            }
            // Handles the wizard
            WizardHealth wizard = enemy.GetComponent<WizardHealth>();
            if (wizard != null)
            {
            wizard.TakeDamage(attackDamage);
            Debug.Log("Hit Wizard for " + attackDamage + " damage!");
            continue;
            }
        }
    }

    private void OGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }
}
