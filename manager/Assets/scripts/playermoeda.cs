using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int playerID = 1;

    [SerializeField] private int maxCoins = 5;

    private int coins;

    private BolinhaController bolinhaController;

    private void Awake()
    {
        bolinhaController = GetComponent<BolinhaController>();
    }

    public void CollectCoin()
    {
        if (coins >= maxCoins)
            return;

        coins++;

        if (bolinhaController != null)
        {
            bolinhaController.AddCoinBonus();
        }

        PlayerObserverManager.NotifyCoinCollected();
        PlayerObserverManager.NotifyCoinChanged(playerID, coins);
    }

    public int GetCoins()
    {
        return coins;
    }
}