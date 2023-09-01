using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(0, 130 *20* Time.deltaTime,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
                FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            
            playerManager.numberOfCoins += 1;
            Destroy(gameObject);

        }
    }
}
