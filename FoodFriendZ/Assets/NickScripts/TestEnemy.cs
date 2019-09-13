using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public BaseEnemy currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemy.Start();
    }

    // Update is called once per frame
    void Update()
    {

        currentEnemy.currentPos = this.transform.position;

        currentEnemy.Update();
        currentEnemy.Aggro();

    }
}
