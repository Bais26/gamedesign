using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerController playerController;
    // Untuk input dari button UI
    private float mobileInputX = 0f;

    private Vector2 moveInput;
    private bool isJumping = false;
    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private AudioSource audioSource;


    private enum MovementState { idle, walk, jump, fall, run}

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        playerController = new PlayerController(); 
    }

    private void OnEnable()
    {
        playerController.Enable();

        playerController.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerController.Movement.Move.canceled += ctx => moveInput = Vector2.zero;

        // playerController.Movement.Jump.performed += ctx => Jump();
        playerController.Movement.Jump.started += ctx => Jump();


        
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Update()
    {
        // Jika menggunakan mobile input, pakai itu
        if (Application.isMobilePlatform)
        {
            moveInput = new Vector2(mobileInputX, 0f);
        }
        else
        {
            // Kalau bukan mobile, pakai Input System
            moveInput = playerController.Movement.Move.ReadValue<Vector2>();
        }

    }

    private void FixedUpdate()
    {
        //gabungan mobile
        Vector2 targetVelocity = new Vector2((moveInput.x + mobileInputX) * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;

        UpdateAnimation();

        // Reset isJumping hanya saat grounded dan velocity Y mendekati 0
        if (isGrounded() && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJumping = false;
        }

    }

    private void UpdateAnimation()
    {
        MovementState state;

        // Gabungkan input dari keyboard dan mobile
        float horizontal = moveInput.x != 0 ? moveInput.x : mobileInputX;

        // Cek arah jalan
        if (horizontal > 0f)
        {
            state = MovementState.walk;
            sprite.flipX = false;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.walk;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // Cek apakah sedang lompat atau jatuh
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }


    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump()
    {
        // Cek ulang grounded saat ini, dan jangan gunakan isJumping (karena bisa delay)
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
             if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        }
    }

    // Fungsi ini dipanggil saat tombol kanan ditekan
    public void MoveRight(bool isPressed)
    {
        if (isPressed)
            mobileInputX = 1f;
        else if (mobileInputX == 1f)
            mobileInputX = 0f;
    }

    public void MoveLeft(bool isPressed)
    {
        if (isPressed)
            mobileInputX = -1f;
        else if (mobileInputX == -1f)
            mobileInputX = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.collider.CompareTag("KillZone"))
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Misal: reload level setelah delay
        StartCoroutine(RestartAfterDelay(1.5f));
    }
}

private IEnumerator RestartAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}


    // Fungsi ini dipanggil saat tombol lompat ditekan
    public void MobileJump()
    {
        if (isGrounded())
        {
            Jump();
        }
    }
}
