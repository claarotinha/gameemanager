using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private int player1Wins = 0;
    private int player2Wins = 0;

    private TMP_Text player1RoundText;
    private TMP_Text player2RoundText;

    [Header("Players")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private Vector3 player1StartPosition;
    private Vector3 player2StartPosition;

    private void Start()
    {
        player1StartPosition = player1.position;
        player2StartPosition = player2.position;

        FindRoundUI();
        UpdateRoundUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Player2WinsRound();
        }

        if (other.CompareTag("Player2"))
        {
            Player1WinsRound();
        }
    }

    private void FindRoundUI()
    {
        player1RoundText =
            GameObject.Find("P1RoundText")?.GetComponent<TMP_Text>();

        player2RoundText =
            GameObject.Find("P2RoundText")?.GetComponent<TMP_Text>();
    }

    private void UpdateRoundUI()
    {
        if (player1RoundText != null)
        {
            player1RoundText.text = "P1: " + player1Wins;
        }

        if (player2RoundText != null)
        {
            player2RoundText.text = "P2: " + player2Wins;
        }
    }

    private void Player1WinsRound()
    {
        player1Wins++;

        UpdateRoundUI();

        Debug.Log("Player 1 ganhou o round!");
        Debug.Log(
            "Placar: P1 " +
            player1Wins +
            " x " +
            player2Wins +
            " P2"
        );

        CheckMatchWinner();
    }

    private void Player2WinsRound()
    {
        player2Wins++;

        UpdateRoundUI();

        Debug.Log("Player 2 ganhou o round!");
        Debug.Log(
            "Placar: P1 " +
            player1Wins +
            " x " +
            player2Wins +
            " P2"
        );

        CheckMatchWinner();
    }

    private void CheckMatchWinner()
    {
        if (player1Wins >= 2)
        {
            FinalizarPartida(1);
        }
        else if (player2Wins >= 2)
        {
            FinalizarPartida(2);
        }
        else
        {
            ResetRound();
        }
    }

    private void FinalizarPartida(int jogadorVencedor)
    {
        Debug.Log(
            "PLAYER " +
            jogadorVencedor +
            " GANHOU A PARTIDA!"
        );

        if (MatchResultData.Instance == null)
        {
            Debug.LogError(
                "MatchResultData não encontrada!"
            );

            return;
        }

        MatchResultData.Instance.DefinirVencedor(
            jogadorVencedor
        );

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene(
                "TelaVitoria"
            );
        }
        else
        {
            Debug.LogError(
                "GameManager.Instance não encontrado!"
            );
        }
    }

    private void ResetRound()
    {
        player1.position = player1StartPosition;
        player2.position = player2StartPosition;

        Debug.Log("Novo round começou!");
    }
}