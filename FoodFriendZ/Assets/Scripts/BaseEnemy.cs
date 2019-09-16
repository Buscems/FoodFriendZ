using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseEnemy : ScriptableObject
{

    [Header("Generic Enemy Values")]
    [Tooltip("How fast we want the enemy to move")]
    public float speed;
    [Tooltip("How far away we want the enemy to be able to see the player")]
    public float aggroRange;
    [Tooltip("How long we want the enemy to be aggro after losing sight of the player")]
    public float aggroTimerMax;

    float aggroTimer;

    Vector3 currentPos;

    bool aggro;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {





    }

    public void Aggro()
    {

        if (aggro)
        {

            if (aggroTimer <= 0)
            {
                aggroTimer = aggroTimerMax;
                aggro = false;
            }

        }

        if ((target.transform.position - currentPos).magnitude < aggroRange)
        {
            aggro = true;
            aggroTimer = aggroTimerMax;
        }
        else
        {
            if (aggro)
            {
                aggroTimer -= Time.deltaTime;
            }
        }

    }

    void Movement(){

    }
}