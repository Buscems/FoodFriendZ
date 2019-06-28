using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class Melee : MonoBehaviour
{

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Attack Variables")]
    public GameObject sword;
    public bool attacking;
    bool swordStart;
    Vector3 swordStartPos;
    public Vector3 direction;
    public float offset;
    public float attackDuration;
    private float attackTimer;
    //these are for the rotation of the sword
    public float Radius;
    private Vector2 _centre;
    private float _angle;
    [HideInInspector]
    public float attackSpeed;

    [Header("Script References")]
    [Tooltip("This is the reference for the main character controller that controls movement")]
    public PlayerMovement pm;

    private void Awake()
    {
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

    }

    // Start is called before the first frame update
    public void MeleeStart()
    {

        pm = this.GetComponent<PlayerMovement>();
        sword.SetActive(false);
    }

    // Update is called once per frame
    public void MeleeUpdate()
    {

        //keeping track of the direction of the player
        this.direction = pm.velocity / pm.speed;

        //melee attack
        if (myPlayer.GetButtonDown("Attack") && !attacking)
        {
            Attack();
        }

        if (attacking)
        {

            if (!swordStart)
            {
                _angle = 0;
                attackTimer = attackDuration;
                swordStart = true;
            }

            _centre = transform.position;

            _angle += attackSpeed * Time.deltaTime;

            var newOffset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            sword.transform.position = _centre + newOffset;
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                EndAttack();
            }
        }

        if (!attacking)
        {
            swordStart = false;
        }

    }
    void Attack()
    {
        attacking = true;
        sword.SetActive(true);
    }

    void EndAttack()
    {
        
        sword.SetActive(false);
        attacking = false;

    }

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
                    Debug.LogError("Player Num is 0, please change to a number >0");
                    break;
            }


        }
    }

}
