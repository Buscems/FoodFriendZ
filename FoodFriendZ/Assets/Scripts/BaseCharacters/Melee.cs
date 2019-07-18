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
    [HideInInspector]
    public Animator swordAnim;
    public bool attacking;
    public Vector3 direction;
    public float offset;

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
        swordAnim = sword.GetComponent<Animator>();
        sword.SetActive(false);

    }

    // Update is called once per frame
    public void MeleeUpdate()
    {

        //keeping track of the direction of the player
        this.direction = pm.velocity;

        //melee attack
        if (myPlayer.GetButtonDown("Attack") && !attacking)
        {
            sword.SetActive(true);
        }

    }
    //These functions are placements that have been moved to the MeleeAttack script
    /*
    public void Attack()
    {
        attacking = true;
        //this will have the sword spawn where the player is facing + a certain distance away
        sword.transform.position = this.transform.position + direction * offset;
    }

    public void EndAttack()
    {
        sword.SetActive(false);
        attacking = false;

    }
    */

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
