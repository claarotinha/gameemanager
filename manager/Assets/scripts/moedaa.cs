using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner spawner;


    private void Start()
    {
        spawner =
            FindFirstObjectByType<CoinSpawner>();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerCoins playerCoins =
            other.GetComponent<PlayerCoins>();

        if(playerCoins != null)
        {
            int before =
                playerCoins.GetCoins();


            playerCoins.CollectCoin();


            if(playerCoins.GetCoins() > before)
            {
                spawner.RemoveCoin();

                Destroy(gameObject);
            }
        }
    }
}