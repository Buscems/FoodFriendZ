using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /*NOTES
     * this is really broken
     * nothing works
     * dont try it
     * i broke everything
     */
public class PowerUps : MonoBehaviour
{
    public float playerSpeed, originalSpeed;
    public Rigidbody2D playerRb;
    public int playerId;
    public float internalTimer, itemTimer, maxTimer;
    //public GameObject projectile;
    public Vector2 velocity;

    public bool activeItem;

    //public Vector2 spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        internalTimer = Time.timeScale;
        itemTimer = maxTimer;
        playerSpeed = originalSpeed;
        //spawnPoint = new Vector2(transform.position.x + 5f, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //basic movement 
        velocity.x = Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;

        playerRb.MovePosition(playerRb.position + velocity * Time.deltaTime);

        //item Timeout
        if (activeItem)
        {
            itemTimer-= Time.deltaTime;
        }

        if (itemTimer <= 0)
        {
            itemTimer = maxTimer;
            activeItem = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (activeItem == false) {
            if (other.gameObject.tag == "SlowSpeed")
            {
                playerSpeed *= .5f;
                Destroy(other.gameObject);
                activeItem = true;
            }

            if (other.gameObject.tag == "SlowTime")
            {
                internalTimer = .5f;
                Destroy(other.gameObject);
                activeItem = true;
            }

            if (other.gameObject.tag == "FastSpeed")
            {
                playerSpeed *= 1.5f;
                Destroy(other.gameObject);
                activeItem = true;
            }
        }
        /*
        if (other.gameObject.tag == "Projectile")
        {
            Instantiate(projectile, spawnPoint, Quaternion.identity);
        }
        */
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerSpeed = originalSpeed;
        internalTimer = 1;
    }

}
