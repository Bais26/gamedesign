using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input kiri-kanan
        movement.x = Input.GetAxisRaw("Horizontal");

        // Atur animasi Speed berdasarkan gerakan
        animator.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    void FixedUpdate()
    {
        // Gerakkan karakter
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
