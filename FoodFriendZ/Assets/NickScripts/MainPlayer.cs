using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class MainPlayer : MonoBehaviour
{
    public int health;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    Vector3 direction;

    public Transform attackDirection;

    public BasePlayer currentChar;

    [HideInInspector]
    public float speed;

    Rigidbody2D rb;
    Vector3 velocity;

    private CameraShake cam;


    private void Awake()
    {

        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
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
        PlayerMovement();
        LookDirection();
        Attack();
    }


    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (velocity * speed) * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        currentChar.Update();
        speed = currentChar.speed;
        currentChar.currentPosition = this.transform.position;

        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        direction.x = myPlayer.GetAxisRaw("DirectionHorizontal");
        direction.y = myPlayer.GetAxisRaw("DirectionVertical");

        if (velocity.x != 0)
        {
            currentChar.currentDirection.x = velocity.x;
        }
        if (velocity.y != 0)
        {
            currentChar.currentDirection.y = velocity.y;
        }
    }

    private void LookDirection()
    {
        if (direction.y != 0)
        {
            attackDirection.transform.right = direction;
        }

        if (direction.x != 0)
        {
            attackDirection.transform.right = direction;
        }
    }
    
    private void Attack()
    {
        if (myPlayer.GetButtonDown("Attack"))
        {
            GameObject attack = Instantiate(currentChar.weapon, transform.position + (attackDirection.transform.right * currentChar.offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + currentChar.attackRotationalOffset));
            attack.transform.parent = transform;
            attack.GetComponent<Attack>().damage = currentChar.attackDamage;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            health--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            health++;
        }
    }

    public void HitEnemy()
    {
        cam.StartShake();
    }

    public void GetHit(int damage)
    {
        health -= damage;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "StatBoost")
        {
            BasePowerUp temp = other.gameObject.GetComponent<BasePowerUp>();

            currentChar.speed *= temp.movementSpeed;
            currentChar.attackSize *= temp.attackSize;
            currentChar.attackSpeed *= temp.attackSpeed; //for melee
            currentChar.firerate *= temp.attackSpeed; //for projectiles
            currentChar.attackDamage *= temp.attackDamage;

            Destroy(other.gameObject);
        }
    }

}
