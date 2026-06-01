using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    private int coins;

    public void CollectCoin()
    {
        coins++;

        PlayerObserverManager.NotifyCoinChanged(coins);
    }
}