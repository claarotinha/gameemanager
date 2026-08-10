
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BolinhaController : MonoBehaviour
{
    [Header("Configuração da Bolinha")]
    public BallData ballData;

    private Rigidbody rb;

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

        // Valores originais
        currentSpeed = ballData.speed;
        currentPushForce = ballData.pushForce;
        currentWeight = ballData.weight;

        rb.mass = currentWeight;

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

    // Chamado quando pega uma moeda
    public void AddCoinBonus()
    {
        PlayerCoins playerCoins =
            GetComponent<PlayerCoins>();

        if (playerCoins == null)
            return;

        int coins = playerCoins.GetCoins();

        // Volta aos valores originais
        currentSpeed = ballData.speed;
        currentPushForce = ballData.pushForce;
        currentWeight = ballData.weight;

        currentPushForce += coins * 1f;

        // + peso
        currentWeight += coins * 0.2f;

        // Atualiza o peso do Rigidbody
        rb.mass = currentWeight;
    }

    public void RecarregarDados()
    {
        LoadBallData();
    }
}
