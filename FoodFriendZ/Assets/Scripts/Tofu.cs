using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tofu : Melee
{

    [Header("Tofu Specific Variables")]
    public float attackDamage;
    public float tofuAttackSpeed;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        MeleeStart();
        attackSpeed = tofuAttackSpeed;
        pm.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MeleeUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "")
        {

        }
    }

}
