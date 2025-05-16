using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 5;
    public float lifetime = 5f;
    public bool fromPlayer = false; // 🔁 tells who cast it

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (fromPlayer)
        {
            // Damage Enemies
            GoblinHealth goblin = collision.GetComponent<GoblinHealth>();
            if (goblin != null)
            {
                goblin.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            SkeletonHealth skeleton = collision.GetComponent<SkeletonHealth>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            WizardHealth wizard = collision.GetComponent<WizardHealth>();
            if (wizard != null)
            {
                wizard.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            // Damage Player
            if (collision.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                Destroy(gameObject);
                return;
            }
        }

        if (!collision.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
