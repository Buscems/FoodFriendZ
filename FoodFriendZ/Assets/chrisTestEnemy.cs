using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chrisTestEnemy : MonoBehaviour
{
    //Movement 
    public float Speed;
    private Transform Player;
    public float distToPlayer;


    //HEALTH
    public float Health;
    public float HealthMax;

    //Death Effect 
    public GameObject DeathParticles;

    //Sound source

    //public AudioSource gruntSoundSource;
    //public AudioClip[] gruntClips;

    //private Rigidbody2D rbody;

    private bool facingRight;



    // Use this for initialization
    void Start()
    {

        //rbody = GetComponent<Rigidbody2D>();

        Health = HealthMax;
        facingRight = true;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }



    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, Player.position) < distToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }

        Flip();


    }



    public void Hurt(float damage)
    {
        Health -= damage;
        Debug.Log("HIT. Health Left: " + Health);
        if (Health <= 0)
        {
            Debug.Log("Kill");
            Destroy(gameObject);
            //Instantiate(DeathParticles, transform.position, transform.rotation);
            //KillCountScript.KillCount += 1;
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Bolt")
        {
            //Hurt(10);
            Destroy(gameObject);
            //gruntSoundSource.Play();

            //play random sound 
            //int randID = (int)Random.Range(0, gruntClips.Length);
            //gruntSoundSource.clip = gruntClips[randID];
            //gruntSoundSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bolt")
        {
            Destroy(gameObject);
        }
    }


    private void Flip()
    {

        if (Player.position.x > transform.position.x && !facingRight || Player.position.x < transform.position.x && facingRight)
        {

            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
}

