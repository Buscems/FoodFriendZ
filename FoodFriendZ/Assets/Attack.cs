using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector]
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //decrease the enemy's health, this will be for regular enemies as well as boss enemies
            other.GetComponent<BaseEnemy>();
            print("hit");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
