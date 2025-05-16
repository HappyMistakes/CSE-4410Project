using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;
    public float chargeSpeed = 5f;
    public float detectionRange = 70f;  
    public Transform player;

    private Rigidbody2D rb;
    private Transform targetPoint;
    private bool chasingPlayer = false;
    private Vector3 originalScale;

    private float chaseStartDelay = 2.5f;
    private float timeSinceStart;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        targetPoint = pointB;

        
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);
            chasingPlayer = distanceToPlayer <= detectionRange;

            timeSinceStart += Time.deltaTime;
            if (timeSinceStart < chaseStartDelay) return;

            Debug.Log("Goblin Pos: " + transform.position);
            Debug.Log("Player Pos: " + player.position);
            Debug.Log("Horizontal Distance to Player: " + distanceToPlayer);
        }

        if (chasingPlayer && player != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        float directionX = player.position.x - transform.position.x;
        float moveDir = Mathf.Sign(directionX);

        rb.velocity = new Vector2(moveDir * chargeSpeed, rb.velocity.y);

        // Flip sprite based on direction
        if (moveDir != 0)
        {
            transform.localScale = new Vector3(moveDir * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void Patrol()
    {
        float direction = targetPoint.position.x - transform.position.x;
        float moveDir = Mathf.Sign(direction);

        rb.velocity = new Vector2(moveDir * patrolSpeed, rb.velocity.y);

        if (Mathf.Abs(direction) < 0.2f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.2f);
            Gizmos.DrawWireSphere(pointB.position, 0.2f);
        }
    }
}
