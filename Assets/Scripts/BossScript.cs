using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public float walkSpeed;
    public int positionOfPatrol;
    public Transform point;
    public bool movingRight = true;
    
    Transform player;
    public float stoppingDistance;
    bool chill = false;
    public bool angry = false;
    bool goBack = false;

    public HealthBar healthBar;
    public int currentHealth;
    public int maxHealth ;
    AudioSource audioSource;
    public AudioClip Attack;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        { goBack = true; }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        { angry = true; chill = false; goBack = false; }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        { goBack = true; angry = false; }

        if (chill) { Chill(); }
        else if (angry) { Angry(); }
        else if (goBack)
        {
            if (transform.position.x > point.position.x + positionOfPatrol)
            {
                movingRight = false;
            }
            else if (transform.position.x < point.position.x - positionOfPatrol)
            { movingRight = true; }

            if (Mathf.Abs(transform.position.x - point.position.x) < 0.1f)
            {
                Chill(); chill = true;
            }
            else GoBack();

        }
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        { { Invoke("destr", 0.5f); }
            Win();
        }
    }

    void Win()
    {
        
        Application.LoadLevel("Win");

    }
    void Chill()
    {
        speed = 0;
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (transform.position.x < player.position.x)
            movingRight = true;
        else
            movingRight = false;
        speed = walkSpeed;
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = walkSpeed;
    }

    void Flip()
    {
        if ((!movingRight && transform.localScale.x > 0) || (movingRight && transform.localScale.x < 0))
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        if (movingRight)
        {
            Vector3 Scaler2 = healthBar.transform.localScale;
            Scaler2.x = Mathf.Abs(Scaler2.x);
            healthBar.transform.localScale = Scaler2;
        }
        else
        {
            Vector3 Scaler2 = healthBar.transform.localScale;
            Scaler2.x = Mathf.Abs(Scaler2.x) * -1;
            healthBar.transform.localScale = Scaler2;
        }


    }

    void destr()
    {
        Destroy(this.gameObject);
    }

    public void attack()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = Attack;
            audioSource.Play();
        }
    }

    /* void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject g = GameObject.Find("Player");
            PlayerController p = g.GetComponent<PlayerController>();
            p.currentHealth -= 10;
        }
    }
    */

}
