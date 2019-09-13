using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{

    //VARIABLES
    public GameObject enemyObject;
    private GameObject player;
    private Vector2 playerTarget;

    public float speed;




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerTarget;
        transform.LookAt(playerTarget);
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTarget, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "Player")
        {
            Damage();
            Destroy(gameObject);
        }
    }

    void Damage()
    {

    }
}
