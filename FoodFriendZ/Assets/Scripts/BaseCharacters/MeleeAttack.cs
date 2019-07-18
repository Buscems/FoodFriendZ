using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Melee thisCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        thisCharacter.attacking = true;
        //this will have the sword spawn where the player is facing + a certain distance away
        this.transform.position = thisCharacter.transform.position + thisCharacter.direction * thisCharacter.offset;
        this.transform.up = thisCharacter.direction;
    }

    public void EndAttack()
    {

        thisCharacter.attacking = false;
        this.gameObject.SetActive(false);
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //decrease the enemy's health, this will be for regular enemies as well as boss enemies
            collision.GetComponent<BaseEnemy>();
        }    
    }

}
