using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : MonoBehaviour
{
    [Tooltip("Name of power up")]
    string powerUpName = "";

    [Tooltip("Player movement speed increase/decrease percent")]
    public float movementSpeed = 1;
    [Tooltip("Physical size of the players attack/bullet increase/decrease percent")]
    public float attackSize = 1;
    [Tooltip("How fast the player can attack if Melee OR firerate increase/decrease percent for projectiles")]
    public float attackSpeed = 1;
    [Tooltip("Damage of player attack increase/decrease percent")]
    public float attackDamage = 1;
}
