using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHealth : MonoBehaviour {
    
    public int maxHealth = 3; // Skeleton's total health
    private int currentHealth;
    public float damagePercentage = 10f; // Percentage-based damage to the player
    public float damageInterval = 1f; // Time between each hit to the player
    public GameObject potionPrefab; // Potion prefab to drop when skeleton dies]
    public AudioSource attackSound;

    private Dictionary<PlayerHealth, Coroutine> activeDamageCoroutines = new Dictionary<PlayerHealth, Coroutine>();

    void Start() {
        currentHealth = maxHealth; // Initialize skeleton's health
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log(gameObject.name + " has died!");

        // 50% chance to drop a potion
        if (Random.value < 0.5f && potionPrefab != null) { 
            Instantiate(potionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Potion dropped!");
        }

        Destroy(gameObject); // Remove skeleton from the scene
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null && !activeDamageCoroutines.ContainsKey(playerHealth)) {
                Coroutine damageCoroutine = StartCoroutine(DealContinuousDamage(playerHealth));
                activeDamageCoroutines[playerHealth] = damageCoroutine;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null && activeDamageCoroutines.ContainsKey(playerHealth)) {
                StopCoroutine(activeDamageCoroutines[playerHealth]);
                activeDamageCoroutines.Remove(playerHealth);
            }
        }
    }

    private IEnumerator DealContinuousDamage(PlayerHealth playerHealth) {
        while (true) {
            if (playerHealth != null) {
                if (attackSound != null) {
                attackSound.Play();
                }
            }
            
            if (playerHealth != null) {
                int damageAmount = Mathf.RoundToInt(playerHealth.maxHealth * (damagePercentage / 100f)); // Calculate percentage-based damage
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Player took " + damageAmount + " damage (" + damagePercentage + "% of max health)");
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
