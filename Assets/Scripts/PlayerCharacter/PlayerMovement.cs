using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D body;
    public float gravityScale = 2f;
    public float jumpHeight = 2.5f;
    public float speed = 7f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;
    private SpriteRenderer spriteRenderer; 
    private Animator animator;

    void Start() {
        body.gravityScale = gravityScale;
        jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * gravityScale * 2.2f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

void Update() {

    float xInput = Input.GetAxis("Horizontal");
    body.velocity = new Vector2(xInput * speed, body.velocity.y);

    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    if (Input.GetButtonDown("Jump") && isGrounded) {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    if (xInput > 0) {
        spriteRenderer.flipX = false;
    } 
    else if (xInput < 0) {
        spriteRenderer.flipX = true;
    }

    animator.SetBool("isMoving", Mathf.Abs(xInput) > 0.01f);

    
    if (Input.GetMouseButtonDown(0)) { // 0 = Left Mouse Button
        animator.SetTrigger("Attack");
    }

}
}
