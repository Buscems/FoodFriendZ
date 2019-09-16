using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{

    public CurrencyManager curMoney;
    public int price;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void powerUpButton(){
        if (curMoney.totalMoney >= price){
            curMoney.totalMoney -= price;
        }
    }
}
