using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatTemp : MonoBehaviour
{
    //legit only has one thing
    //eventually thisll merge with other player stuff
    //but i just made this so i can mess with stuff without breaking everything

    public float playerSpeed;
    public Vector3 velocity;
    Rigidbody2D playerRb;

    //prevents stacking
    public bool activeItem;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal")*playerSpeed;

        playerRb.MovePosition(transform.position + velocity *Time.deltaTime);

        Debug.Log("Time Scale = " + Time.timeScale);
        Debug.Log("Speed: " + playerSpeed);
    }
}
