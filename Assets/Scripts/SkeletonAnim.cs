using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    GameObject g;
    SkeletonScript enemy;

    void Start()
    {
        anim = GetComponent<Animator>();
        g = GameObject.Find("Skeleton");
        enemy = g.GetComponent<SkeletonScript>();

    }

    void Update()
    {
        if (enemy.angry)
            anim.SetBool("angry", true);
        else
            anim.SetBool("angry", false);

        Fight2D.Action(new Vector2(g.transform.position.x, g.transform.position.y), 1, 11, 10, false);
        if (Fight2D.isEnemyNear)
        {
            anim.SetBool("possibleToAttack", true);
            enemy.attack();
        }
        else
            anim.SetBool("possibleToAttack", false);
        if (enemy.speed == 0)
            anim.SetBool("stay", true);
        else
            anim.SetBool("stay", false);
        if (enemy.currentHealth<=0)
            anim.SetBool("dead", true);
    }
}
