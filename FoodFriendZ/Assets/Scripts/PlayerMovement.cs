using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class PlayerMovement : MonoBehaviour
{

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Movement Variables")]
    [HideInInspector]
    public float speed;
    public Vector3 velocity;
    public bool moving;
    Vector3 direction;
    Rigidbody2D rb;

    public Animator anim;

    private void Awake()
    {

        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //movement for players
        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal") * speed;
        velocity.y = myPlayer.GetAxisRaw("MoveVertical") * speed;

        //checking to see when the player is moving or not so we know what animations to play for the blend tree
        if(Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.y) > 0)
        {
            moving = true;
            //this tells the blend tree to change to that animation instead of idle using floats to switch animations from the same animation state
            anim.SetFloat("State", 1);
        }
        else if(velocity.x == 0 && velocity.y == 0)
        {
            moving = false;
            anim.SetFloat("State", 0);
        }

        rb.MovePosition(transform.position + velocity * Time.deltaTime);

    }

    //these two methods are for ReWired, if any of you guys have any questions about it I can answer them, but you don't need to worry about this for working on the game - Buscemi
    void OnControllerConnected(ControllerStatusChangedEventArgs arg)
    {
        CheckController(myPlayer);
    }

    void CheckController(Player player)
    {
        foreach (Joystick joyStick in player.controllers.Joysticks)
        {
            var ds4 = joyStick.GetExtension<DualShock4Extension>();
            if (ds4 == null) continue;//skip this if not DualShock4
            switch (playerNum)
            {
                case 4:
                    ds4.SetLightColor(Color.yellow);
                    break;
                case 3:
                    ds4.SetLightColor(Color.green);
                    break;
                case 2:
                    ds4.SetLightColor(Color.blue);
                    break;
                case 1:
                    ds4.SetLightColor(Color.red);
                    break;
                default:
                    ds4.SetLightColor(Color.white);
                    Debug.LogError("Player Num is 0, please change to a number > 0");
                    break;
            }


        }
    }

}
