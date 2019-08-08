using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //holds the type of powerUps that I can set
    public enum PowerUpTypes
    {
        SlowSpeed,
        FastSpeed,
        SlowTime

    }
    //lets me set up the typer of powerUp
    public PowerUpTypes currentPowerUp;

    //PowerUp Variables
    public float multiplier;
    public float timer = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {

            if (other.CompareTag("Player"))
            {
                StartCoroutine(Pickup(other));

            }
        
    }

   IEnumerator Pickup(Collider2D player)
   {
        PlayerStatTemp stats = player.GetComponent<PlayerStatTemp>();

        if (stats.activeItem == false)
        {
            switch (currentPowerUp)
            {
                //case 1
                case PowerUpTypes.FastSpeed:

                    stats.activeItem = true;

                    multiplier = 1.5f;
                    stats.playerSpeed *= multiplier;

                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<Collider2D>().enabled = false;

                    yield return new WaitForSeconds(timer);

                    stats.playerSpeed /= multiplier;

                    Destroy(gameObject);
                    stats.activeItem = false;

                    break;

                //case 2
                case PowerUpTypes.SlowSpeed:

                    stats.activeItem = true;

                    multiplier = .75f;
                    stats.playerSpeed *= multiplier;

                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<Collider2D>().enabled = false;

                    yield return new WaitForSeconds(timer);

                    stats.playerSpeed /= multiplier;

                    Destroy(gameObject);
                    stats.activeItem = false;

                    break;

                //case 3
                case PowerUpTypes.SlowTime:

                    stats.activeItem = true;

                    multiplier = .75f;
                    Time.timeScale *= multiplier;

                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<Collider2D>().enabled = false;

                    yield return new WaitForSeconds(timer);

                    Time.timeScale /= multiplier;

                    Destroy(gameObject);
                    stats.activeItem = false;

                    break;

                default:
                    Debug.Log("Not a Power Up");
                    break;
            }
        }
   }
}//END OF SCRIPT
