using UnityEngine;

public class WizardHealth : MonoBehaviour
{
    public float maxHealth = 20f;
    private float currentHealth;

    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
