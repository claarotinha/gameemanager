
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
        // Impede pegar mais que 5 moedas
        if (coins >= maxCoins)
            return;

        // Adiciona uma moeda
        coins++;

        // Aplica o bônus correspondente à quantidade de moedas
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

    // Reseta as moedas e os bônus no começo de um novo round
    public void ResetCoins()
    {
        // Zera as moedas
        coins = 0;

        // Volta os atributos da bolinha para os valores originais
        if (bolinhaController != null)
        {
            bolinhaController.RecarregarDados();
        }

        // Atualiza a interface para mostrar 0/5
        PlayerObserverManager.NotifyCoinChanged(
            playerID,
            coins
        );
    }
}