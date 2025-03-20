using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float maxHealth = 20f;
    public float currentHealth;
    public Slider healthBar; // Health UI Slider
    public GameOver gameOver; // Game Over Panel
    public int potionCount = 1; // Player starts with 1 potion
    public int healAmount = 20; // How much each potion heals
    
    public TMP_Text potionText;
    public AudioSource potionSound;

    void Start() {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealthUI(); // Makes UI start correctly
        UpdatePotionUi();
}

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
        Heal();
        }
    }


    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }
    public void AddPotion(int amount) {
        potionCount += amount;
        Debug.Log("Potion collected! Total Potions: " + potionCount);
        UpdatePotionUi();
    }


    public void Heal() {
        
        if (potionCount > 0 && currentHealth < maxHealth) {
            currentHealth += healAmount;
            potionCount--;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        if (potionSound != null) {
            potionSound.Play();
        }
        UpdateHealthUI();
        UpdatePotionUi();
        Debug.Log("Used a potion! Remaining potions: " + potionCount);
        } 
        else if (potionCount == 0) {
        Debug.Log("No potions left!");
        } 
        else {
        Debug.Log("Health is already full!");
        }
    }


    void UpdateHealthUI() {
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        } 
        else {
        Debug.LogError("HealthBar UI not assigned in PlayerHealth!");
        }
    }

    void UpdatePotionUi() {
        if (potionText != null) {
            potionText.text = "" + potionCount;
        }
        else {
            Debug.LogError("Potion UI text not assigned");
        }
    }


    private void Die() {
        gameOver.ShowGameOver();
        Debug.Log("Player has died!");
    }
}
