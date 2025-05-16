using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float maxHealth = 20f;
    public float currentHealth;
    public Slider healthBar;
    public GameOver gameOver;
    public int potionCount = 1;
    public int healAmount = 20;

    public TMP_Text potionText;
    public AudioSource potionSound;

    void Start() {
        // Try to auto-find UI elements if not assigned
        if (healthBar == null) {
            GameObject hb = GameObject.Find("HealthBar");
            if (hb != null) healthBar = hb.GetComponent<Slider>();
        }

        if (potionText == null) {
            GameObject pt = GameObject.Find("PotionCounter");
            if (pt != null) potionText = pt.GetComponent<TMP_Text>();
        }

        if (gameOver == null) {
            GameObject go = GameObject.Find("GameOverScreen Panel");
            if (go != null) gameOver = go.GetComponent<GameOver>();
        }

        // Default values
        currentHealth = maxHealth;

        // Load from persistent PlayerStats if available
        if (PlayerStats.Instance != null && PlayerStats.Instance.currentHealth > 0) {
            currentHealth = PlayerStats.Instance.currentHealth;
            potionCount = PlayerStats.Instance.potionCount;
            maxHealth = PlayerStats.Instance.maxHealth;
        }

        UpdateHealthUI();
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
        }
    }

    void UpdateHealthUI() {
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void UpdatePotionUi() {
        if (potionText != null) {
            potionText.text = "" + potionCount;
        }
    }

    private void Die() {
        if (gameOver != null) {
            gameOver.ShowGameOver();
        }
        Debug.Log("Player has died.");
    }

    void OnDestroy() {
        // Save player state before unloading
        if (PlayerStats.Instance != null) {
            PlayerStats.Instance.currentHealth = currentHealth;
            PlayerStats.Instance.potionCount = potionCount;
            PlayerStats.Instance.maxHealth = maxHealth;
        }
    }
}
