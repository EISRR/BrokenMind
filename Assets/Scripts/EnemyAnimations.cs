using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    GameObject g;
    Enemy enemy;

    void Start()
    {
        anim = GetComponent<Animator>();
        g = GameObject.Find("Enemy");
        enemy = g.GetComponent<Enemy>();
    }

    void Update()
    {
       if (enemy.angry)
            anim.SetBool("isAngry", true);
       else 
            anim.SetBool("isAngry", false);
        


    }
}
