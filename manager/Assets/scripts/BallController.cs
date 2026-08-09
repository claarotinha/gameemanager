
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BolinhaController : MonoBehaviour
{
    [Header("Configuração da Bolinha")]
    public BallData ballData;

    private Rigidbody rb;

    // Valores atuais da bolinha
    private float currentSpeed;
    private float currentPushForce;
    private float currentWeight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        LoadBallData();
    }

    private void LoadBallData()
    {
        if (ballData == null)
        {
            Debug.LogError(
                "Nenhum BallData atribuído em " + gameObject.name
            );

            return;
        }

        // Carrega os valores originais
        currentSpeed = ballData.speed;
        currentPushForce = ballData.pushForce;
        currentWeight = ballData.weight;

        // Aplica o peso no Rigidbody
        rb.mass = currentWeight;

        // Aplica o tamanho da bolinha
        transform.localScale =
            Vector3.one * ballData.size;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }

    public float GetPushForce()
    {
        return currentPushForce;
    }

    public float GetWeight()
    {
        return currentWeight;
    }

    // Chamado quando o jogador coleta uma moeda
    public void AddCoinBonus()
    {
        PlayerCoins playerCoins =
            GetComponent<PlayerCoins>();

        if (playerCoins == null)
            return;

        // Quantidade atual de moedas
        int coins = playerCoins.GetCoins();

        // Volta para os valores originais
        currentSpeed = ballData.speed;
        currentPushForce = ballData.pushForce;
        currentWeight = ballData.weight;

        // =====================================
        // BÔNUS DAS MOEDAS
        // =====================================

        // Cada moeda:
        // -0.2 de velocidade
        // +1 de força
        // +0.2 de peso

        currentSpeed -= coins * 0.2f;

        currentPushForce += coins * 1f;

        currentWeight += coins * 0.2f;

        // =====================================
        // LIMITES
        // =====================================

        // A velocidade nunca pode ficar abaixo de 1
        if (currentSpeed < 1f)
        {
            currentSpeed = 1f;
        }

        // Atualiza o peso do Rigidbody
        rb.mass = currentWeight;
    }

    // Usado para reiniciar os atributos
    // no começo de uma nova rodada
    public void RecarregarDados()
    {
        LoadBallData();
    }
}