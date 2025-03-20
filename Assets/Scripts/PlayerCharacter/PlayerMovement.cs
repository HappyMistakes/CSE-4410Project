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
    private SpriteRenderer spriteRenderer; // Reference to sprite

    void Start() {
        body.gravityScale = gravityScale; // Apply gravity scale
        jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * gravityScale * 2.2f);

        Debug.Log("Calculated Jump Force: " + jumpForce);

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update() {
        //Getting the inputs from unity
        float xInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(xInput * speed, body.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

        if (xInput > 0) {
            spriteRenderer.flipX = true; // Face right
        } 
        else if (xInput < 0) {
            spriteRenderer.flipX = false; // Face left
        }
    }
}
