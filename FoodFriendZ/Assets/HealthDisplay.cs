using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
    public Canvas canvas;
    public Vector2 startLocation = new Vector2(-362.5f, 193);
    public Image healthSprite;
    public float heartSpacing;

    private int playerMaxHealth;
    private int playerCurrentHealth;

    private Image[] health;

    private MainPlayer playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<MainPlayer>();
        playerMaxHealth = playerHealth.health;
        playerCurrentHealth = playerMaxHealth;

        health = new Image[playerMaxHealth];
        
        for(int i = 0; i < playerMaxHealth; i++)
        {
            health[i] = Instantiate(healthSprite, new Vector2(startLocation.x + (i * heartSpacing), startLocation.y), Quaternion.identity);
            health[i].transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerMaxHealth; i++)
        {
            if(i < playerHealth.health)
            {
                health[i].enabled = true;
            }
            else
            {
                health[i].enabled = false;
            }
        }
    }
}
