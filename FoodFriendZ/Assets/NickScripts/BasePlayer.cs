using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasePlayer : ScriptableObject
{
    public float speed;
    [HideInInspector]
    public Vector3 currentPosition;
    public Vector3 currentDirection;

    [Tooltip("Can be 'Melee', 'Range', or 'Builder'")]
    public string attackType;

    [Header("Melee Characters")]
    public GameObject weapon;
    [Tooltip("This is the position the weapon will be at when it is not being used.")]
    public Vector3 awayPos;
    [Tooltip("This is going to be the size of the weapon, z is always 1.")]
    public Vector3 attackSize;
    [Tooltip("This will be how far the weapon is from the player when it is activated.")]
    public float offset;
    [Tooltip("This will be how fast the sword attack plays")]
    public float attackSpeed;
    //[Tooltip("This is the amount that the sword will spin around the player when attacking.")]
    //public float attackRange;
    [HideInInspector]
    public bool isAttacking;

    [Header("Ranged Characters")]
    public GameObject bullet;

    [Header("Building Characters")]
    public GameObject drop;

    // Start is called before the first frame update
    public void Start()
    {
        //this will be taking care of whether or not the player might accidentally have the wrong weapon for anything
        if(attackType == "Melee")
        {
            bullet = null;
            drop = null;
        }
        if (attackType == "Ranged")
        {
            weapon = null;
            drop = null;
        }
        if (attackType == "builder")
        {
            weapon = null;
            bullet = null;
        }

    }

    // Update is called once per frame
    public void Update()
    {

        

    }

}
