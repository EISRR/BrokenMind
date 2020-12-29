using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    GameObject g,g2;
    BossScript enemy;
    PlayerController player;
    void Start()
    {
        anim = GetComponent<Animator>();
        
        g = GameObject.Find(this.gameObject.name);
        enemy = g.GetComponent<BossScript>();
        g2 = GameObject.Find("Player");
        player = g2.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (enemy.angry)
            anim.SetBool("angry", true);
        else
            anim.SetBool("angry", false);

        Fight2D.Action(new Vector2(g.transform.position.x-(float)0.28, g.transform.position.y), 2, 11, 15, false);
        if (Fight2D.isEnemyNear)
        {
            if(!player.godMod)
                player.currentHealth -= 15;
            anim.SetBool("possibleToAttack", true);
            enemy.attack();
        }
        else
            anim.SetBool("possibleToAttack", false);
        if (enemy.speed == 0 || enemy.currentHealth <= 0)
            anim.SetBool("stay", true);
        else
            anim.SetBool("stay", false);


    }
}
