using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // <- Ambil komponen Animator
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Gerak kiri/kanan
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Balik arah sprite
        if (moveX > 0)
            sr.flipX = false;
        else if (moveX < 0)
            sr.flipX = true;

        // Atur parameter "Speed"
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // Atur parameter "isJumping"
        animator.SetBool("isJumping", !isGrounded);

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
