using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{

    public GameObject uiObject;
    public GameObject ableToBuyText;
    public GameObject ableToExitText;
    public GameObject shopMenu;
    public bool canShop;
    public bool isShopping;

    // Start is called before the first frame update
    void Start()
    {
        ableToBuyText.SetActive(false);
        ableToExitText.SetActive(false);
        canShop = false;
        isShopping = false;
        shopMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShop == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isShopping)
                {
                    Resume();
                }
                else
                {
                    isShopping = true;
                    Time.timeScale = 0f;
                    shopMenu.SetActive(true);
                    ableToBuyText.SetActive(false);
                    ableToExitText.SetActive(true);
                }
            }
        }
    }

    public void Resume(){
        isShopping = false;
        Time.timeScale = 1f;
        shopMenu.SetActive(false);
        ableToBuyText.SetActive(true);
        ableToExitText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableToBuyText.SetActive(true);
            canShop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ableToBuyText.SetActive(false);
        canShop = false;
    }
}
