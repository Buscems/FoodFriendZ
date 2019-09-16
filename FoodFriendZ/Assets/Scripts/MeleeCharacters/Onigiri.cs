using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onigiri : Ranger
{

    [Header("Onigiri Specific Variables")]
    public float attackDamage;
    public float tofuAttackSpeed;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RangerStart();
    }

    // Update is called once per frame
    void Update()
    {
        RangerUpdate();
    }
}
