using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{

    public float rayLength;
    public float leftLength;
    public float rightLength;

    public GameObject Enemy;
    public GameObject Projectile;
    public float clipSize;
    public float currentClip;

    private Vector2 currentPos;
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        currentClip = clipSize;
    }

    
    void Update()
    {
        currentPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currentPos, transform.up, rayLength);
        RaycastHit2D Left = Physics2D.Raycast(currentPos, transform.right *-1, leftLength);
        RaycastHit2D Right = Physics2D.Raycast(currentPos, transform.right, rightLength);
        Debug.DrawRay(currentPos, transform.up, Color.red, rayLength);

        if(hit.collider != null)
        {
            Debug.Log("hit" + hit.collider.tag);
            if(hit.collider.tag == "Player")
            {
                Instantiate(Projectile, currentPos, Quaternion.identity);
                currentClip--;
            }
        }

        if(currentClip == 0)
        {
            Reload();
        }
    }

    void Reload()
    {
        currentClip = clipSize;
        //Add courotine
    }

}
