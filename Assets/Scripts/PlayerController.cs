using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;

    private Rigidbody2D rb;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    public HealthBar healthBar;
    public int currentHealth;
    public int maxHealth = 100;

    public Transform punch;
    public float punchRadius;

    AudioSource audioSource;
    public AudioClip Punch;
    public AudioClip Woosh;

    private void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
            Flip();
        else if (facingRight == true && moveInput < 0)
            Flip();

    }

    private void Update() {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fight2D.Action(punch.position, punchRadius, 10, 10, false);
           
            if (Fight2D.isEnemyNear)
                audioSource.clip = Punch;
            else
                audioSource.clip = Woosh;
            audioSource.Play();


        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        healthBar.SetHealth(currentHealth);
        if (currentHealth<=0)
        {
            Lose();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        Vector3 Scaler2 = healthBar.transform.localScale;
        Scaler2.x *= -1;
        healthBar.transform.localScale = Scaler2;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public bool isOnGround()
    {
        return isGrounded;
    }
    public int getExtraJumpsCount()
    {
        return extraJumps;
    }
    void Lose()
    {
        Application.LoadLevel("Lose");
        

    }
}
