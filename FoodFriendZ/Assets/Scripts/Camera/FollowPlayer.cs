using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float radius;

    private float distance;

    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.position;
        distance = Vector2.Distance(playerPos, Camera.main.transform.position);

        if (Mathf.Abs(distance) >= radius)
        {

            playerPos.z = -10;
            transform.position = Vector3.Slerp(transform.position, playerPos, player.GetComponent<MainPlayer>().speed * .35f * Time.deltaTime);
        }
    }
}
