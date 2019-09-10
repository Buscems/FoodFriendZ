using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class MainPlayer : MonoBehaviour
{

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    Vector3 direction;

    public BasePlayer currentChar;

    float speed;

    Rigidbody2D rb;
    Vector3 velocity;

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

        currentChar.Start();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        currentChar.Update();
        speed = currentChar.speed;
        currentChar.currentPosition = this.transform.position;

        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        if(velocity.x != 0)
        {
            currentChar.currentDirection.x = velocity.x;
        }
        if (velocity.y != 0)
        {
            currentChar.currentDirection.y = velocity.y;
        }

    }

    private void FixedUpdate()
    {

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
