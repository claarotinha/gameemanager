using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int playerID = 1;

    private int coins;

    private BolinhaController bolinhaController;

    private void Awake()
    {
        bolinhaController = GetComponent<BolinhaController>();
    }

    public void CollectCoin()
    {
        // Agora não existe limite de moedas
        coins++;

        // Aplica o bônus da moeda
        if (bolinhaController != null)
        {
            bolinhaController.AddCoinBonus();
        }

        // Atualiza os eventos
        PlayerObserverManager.NotifyCoinCollected();
        PlayerObserverManager.NotifyCoinChanged(playerID, coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    // Reseta as moedas no começo de um novo round
    public void ResetCoins()
    {
        coins = 0;

        if (bolinhaController != null)
        {
            bolinhaController.RecarregarDados();
        }

        PlayerObserverManager.NotifyCoinChanged(
            playerID,
            coins
        );
    }
}