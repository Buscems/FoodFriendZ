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

    [HideInInspector]
    public Vector3 currentPos;

    bool aggro;

    [Tooltip("This will be for how many players are in the game just in case we think about doing multiplayer")]
    public Transform[] target;
    Transform currentTarget;

    // Start is called before the first frame update
    public void Start()
    {
        target[0] = GameObject.FindGameObjectWithTag("Player1").transform;
    }

    // Update is called once per frame
    public void Update()
    {

        if (currentTarget != null)
        {
           // Debug.Log(currentTarget.gameObject.name);
        }

        //Debug.Log((target[0].transform.position - currentPos).magnitude);

    }

    public void Aggro()
    {

        for (int i = 0; i < target.Length; i++)
        {

            if ((target[i].transform.position - currentPos).magnitude < aggroRange && !aggro)
            {
                aggro = true;
                currentTarget = target[i];
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

        if (aggro)
        {

            if (aggroTimer <= 0)
            {
                aggroTimer = aggroTimerMax;
                aggro = false;
                currentTarget = null;
            }

        }

    }

    void Movement()
    {

    }

}
