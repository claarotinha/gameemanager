using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinChanged += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinChanged -= UpdateUI;
    }

    private void Start()
    {
        player1Text.text = "P1 Moedas: 0";
        player2Text.text = "P2 Moedas: 0";
    }

    private void UpdateUI(int playerID, int amount)
    {
        if (playerID == 1)
        {
            player1Text.text = "P1 Moedas: " + amount;
        }

        if (playerID == 2)
        {
            player2Text.text = "P2 Moedas: " + amount;
        }
    }
}