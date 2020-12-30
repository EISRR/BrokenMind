using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int damage;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    public bool godMod = false;
    private Rigidbody2D rb;

    public bool Coolmask = false;
    
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
    public Transform sword;
    public float swordRadius;
    AudioSource audioSource;
    public AudioClip Punch;
    public AudioClip Woosh;
    public AudioClip SwordAttack;
    public AudioClip Coin;
    public AudioClip Sword;
    private int defaultDamage;

    private void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
        defaultDamage = damage;
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
        if (Coolmask)
        {
            damage =defaultDamage*2;
        }
        else damage = defaultDamage;
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Coolmask)
            {
                Fight2D.Action(punch.position, punchRadius, 10, damage, false);
                audioSource.volume = 0.15f;
                if (Fight2D.isEnemyNear)
                    audioSource.clip = Punch;

                else
                    audioSource.clip = Woosh;
                audioSource.Play();
            }
            else {
                Fight2D.Action(punch.position, swordRadius, 10, damage, false);
                audioSource.volume = 0.5f;
                if (Fight2D.isEnemyNear)
                    audioSource.clip = SwordAttack;

                else
                    audioSource.clip = Sword;
                audioSource.Play();






            }


        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Coolmask = !Coolmask;
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
            Loset();
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
    void Loset()
    {
        
        Application.LoadLevel("Lose");
        

    }
    

    public void SetGodMod()
    {
        godMod = true;
        Invoke("SetGodModToFalse",1f);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 2.0F, ForceMode2D.Impulse);
    }
    public void SetGodModToFalse()
    {
        godMod = false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.StartsWith("Heal"))
        {
            Destroy(col.gameObject);
            if (currentHealth < maxHealth)
                currentHealth += 30;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            audioSource.volume = 0.4f;
            audioSource.clip = Coin;
            audioSource.Play();


        }
    }
}
