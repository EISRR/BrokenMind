using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    public PlayerController player;
    public int extraJumps;
    public int currentExtraJumps;

    void Start() {
        anim = GetComponent<Animator>();
        GameObject g = GameObject.Find("Player");
        player = g.GetComponent<PlayerController>();
        extraJumps = player.extraJumpValue;
    }

    void Update()
    {
        if (player.isOnGround())
            currentExtraJumps = extraJumps;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (Input.GetKeyDown(KeyCode.UpArrow) && currentExtraJumps>0)
        {
            anim.SetTrigger("jump");
            currentExtraJumps--;
        }
    }
}
