using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public List<string> names;

    private Animator anim;
    public PlayerController player;
    public int extraJumps;
    public int currentExtraJumps;
    public AudioClip JumpSound;
    private AudioSource audioSource;
    public const float timer = 0.5f;
    public float timeLeft;

    void Start() {
        names= new List<string>() { "superpunch", "punch" };
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        GameObject g = GameObject.Find("Player");
        player = g.GetComponent<PlayerController>();
        extraJumps = player.extraJumpValue;
        timeLeft = 0;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (player.isOnGround())
            currentExtraJumps = extraJumps;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
              anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentExtraJumps>0)
        {
            anim.SetTrigger("jump");
            currentExtraJumps--;
            audioSource.Stop();
            audioSource.clip = JumpSound;
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var Rand = Random.Range(0, names.Count);
            anim.SetTrigger(names[Rand]);
            
        }

        }
    }




