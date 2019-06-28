using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    public int totalMoney;
    public TextMesh moneyText;

    public bool inGame;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        totalMoney = PlayerPrefs.GetInt("total_money", 0);
        inGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame == true)
        {
            moneyText.text = "Money: " + totalMoney;
            PlayerPrefs.SetInt("total_money", totalMoney);
        }
        if (isDead == true){
            inGame = false;
            PlayerPrefs.SetInt("total_money", 0);
        }
    }
}
