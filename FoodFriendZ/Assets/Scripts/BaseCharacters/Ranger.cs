using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class Ranger : MonoBehaviour
{
    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Projectile Variables")]
    public GameObject projectile;
    Vector3 direction;
    public enum type { fullAuto, semiAuto, burst };
    type projectileType;

    private void Awake()
    {
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

    }

    // Start is called before the first frame update
    public void RangerStart()
    {
        direction = new Vector3(0, 1);
    }

    // Update is called once per frame
    public void RangerUpdate()
    {

        transform.up = direction;

        if (Mathf.Abs(myPlayer.GetAxisRaw("AimHorizontal")) > 0 || Mathf.Abs(myPlayer.GetAxisRaw("AimVertical")) > 0)
        {
            direction = new Vector3(myPlayer.GetAxisRaw("AimHorizontal"), myPlayer.GetAxisRaw("AimVertical"));
        }

        //this will recognize if the current food is the type where you can hold down the trigger, you have to keep clicking, or if it shoots bursts of rounds and how many in each burst
        switch (projectileType) {
            case type.fullAuto:

                break;
            case type.semiAuto:

                break;
            case type.burst:

                break;
        }

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
