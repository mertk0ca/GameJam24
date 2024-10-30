using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer; //playerin spriteina ulasmak icin
    public Animator animator; //animatordeki parametreleri kullanmak icin

    private float h_Input;
    private bool jump_pressed = false;
    private int jumpCount = 0;

    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private float jumpAmount = 3.5f;
    [SerializeField] private float raycastDistance = 0.5f; 
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded = false;

    public Transform firePoint;

    public AudioClip jumpSound;
    private AudioSource jumpSource;

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        jumpSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetInput();
        CheckInput();
        CheckGround();
        animator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        flipImage();
    }

    void GetInput()
    {
        h_Input = Input.GetAxis("Horizontal");
    }

    void ApplyMovement()
    {
        playerRB.velocity = new Vector2(h_Input * movementSpeed, playerRB.velocity.y);

        if (jump_pressed && (isGrounded || jumpCount < 2))
        {
            jump_pressed = false;
            Jump();
        }
    }

    void CheckInput()
    {
        h_Input = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && (isGrounded || jumpCount < 1))
        {
            jump_pressed = true;

            jumpSource.clip = jumpSound;
            jumpSource.Play();
        }
    }

    void CheckGround()
    {
        Vector2 raycastOrigin = new Vector2(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 1f); //rayi karakterin altindan baslatmak icin
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, raycastDistance, groundLayer); //sadece groundlayera carpmasi icin

        Debug.DrawRay(raycastOrigin, Vector2.down * raycastDistance, Color.red);

        if (hit.collider != null)
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    public void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpAmount);
        jumpCount++;
    }

    void flipImage() //playerin saga ve sola bakmasini saglamak icin ve firepoint objesinin yonunu degistirmek icin
    {
        if (h_Input < 0)
        {
            spriteRenderer.flipX = true;
            firePoint.localEulerAngles = new Vector3(0, 0, 180);
        }
        else if (h_Input > 0)
        {
            spriteRenderer.flipX = false;
            firePoint.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
