using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TofuMeleeAttack : MeleeAttack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    new private void OnTriggerEnter2D(Collider2D collision)
    {
        //this is where you would any extra effects such as making the enemy slowed or burned and what not

    }
}
