using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCoins playerCoins =
            other.GetComponent<PlayerCoins>();

        if (playerCoins != null)
        {
            playerCoins.CollectCoin();

            Destroy(gameObject);
        }
    }
}