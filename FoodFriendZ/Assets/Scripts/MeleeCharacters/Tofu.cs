using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tofu : Melee
{

    [Header("Tofu Specific Variables")]
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float swordOffset;

    // Start is called before the first frame update
    void Start()
    {
        MeleeStart();
        offset = swordOffset;

    }

    // Update is called once per frame
    void Update()
    {
        MeleeUpdate();
        //need to keep updating these guys so that any powerups can accurately effect them
        swordAnim.speed = attackSpeed;
        pm.speed = moveSpeed;
    }

}
