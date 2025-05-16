using System.Collections;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 5f;
    public float attackCooldown = 2f;
    public float detectionRange = 10f;
    public Transform player;

    private float cooldownTimer = 0f;
    public AudioClip fireballSound;
    private AudioSource audioSource;


    void Start()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

            if (distanceToPlayer <= detectionRange && cooldownTimer >= attackCooldown)
            {
                FireFireball();
                cooldownTimer = 0f;
            }
        }
    }

    void FireFireball()
    {   
        if (fireballSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireballSound);
        }

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * fireballSpeed;
        }
    }
}
