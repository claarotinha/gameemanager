using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner spawner;

    public void SetSpawner(CoinSpawner coinSpawner)
    {
        spawner = coinSpawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCoins playerCoins =
            other.GetComponent<PlayerCoins>();

        if (playerCoins != null)
        {
            playerCoins.CollectCoin();

            if (spawner != null)
            {
                spawner.RemoveCoin();
            }

            Destroy(gameObject);
        }
    }
}