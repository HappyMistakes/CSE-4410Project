using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 7f;
    public float fireCooldown = 1f;
    public AudioClip fireballSound;

    private float nextFireTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
        {
            ShootFireball();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void ShootFireball()
    {
        if (fireballSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireballSound);
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        if (direction == Vector2.zero) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        Collider2D fireballCol = fireball.GetComponent<Collider2D>();
        Collider2D playerCol = GetComponent<Collider2D>();
        if (fireballCol != null && playerCol != null)
        {
            Physics2D.IgnoreCollision(fireballCol, playerCol);
        }

        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.fromPlayer = true;
        }

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * fireballSpeed;
        }

        fireball.transform.right = direction;
    }
}
