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
    void Attack() {
        if (attackSound != null) {
            attackSound.Play();
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies) {
            SkeletonHealth skeleton = enemy.GetComponent<SkeletonHealth>();
            if (skeleton != null) {
                skeleton.TakeDamage(attackDamage);
                Debug.Log("Hit Skeleton for " + attackDamage + " damage!");
            }
        }
    }

    private void OGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }
}
