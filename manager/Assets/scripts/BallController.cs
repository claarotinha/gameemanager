using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BolinhaController : MonoBehaviour
{
    [Header("Configuração da Bolinha")]
    public BallData ballData;

    private Rigidbody rb;

    // Valores atuais (mudam com moedas)
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
            Debug.LogError("Nenhum BallData atribuído em " + gameObject.name);
            return;
        }


        currentSpeed = ballData.speed;
        currentPushForce = ballData.pushForce;
        currentWeight = ballData.weight;


        rb.mass = currentWeight;


        // Ajusta o tamanho da bolinha
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


    // Chamado quando pega moedas
    public void AddCoinBonus()
    {
        currentSpeed -= 0.2f;
        currentPushForce += 1f;
        currentWeight += 0.2f;


        if(currentSpeed < 1)
            currentSpeed = 1;


        rb.mass = currentWeight;
    }
}