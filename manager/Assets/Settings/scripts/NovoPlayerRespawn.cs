using UnityEngine;

public class NovoPlayerRespawn : MonoBehaviour
{
    [Header("Ponto inicial")]
    [SerializeField] private Transform pontoInicial;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Morrer()
    {
        Respawn();
    }

    private void Respawn()
    {
        Vector3 posicaoRespawn;

        // Se existe checkpoint, volta para ele
        if (NovoCheckpointManager.Instance != null &&
            NovoCheckpointManager.Instance.CheckpointAtivado())
        {
            posicaoRespawn =
                NovoCheckpointManager.Instance.GetPosicaoCheckpoint();

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.position = posicaoRespawn;

            // Volta a quantidade de moedas do checkpoint
            if (NovoCoinManager.Instance != null)
            {
                NovoCoinManager.Instance.DefinirMoedas(
                    NovoCheckpointManager.Instance.GetMoedasCheckpoint()
                );

                NovoCheckpointManager.Instance
                    .RestaurarMoedasDoCheckpoint();
            }

            Debug.Log("Respawn no checkpoint.");
        }
        else
        {
            // Sem checkpoint: volta para o começo da fase
            if (pontoInicial == null)
            {
                Debug.LogError(
                    "Ponto Inicial não foi atribuído!"
                );

                return;
            }

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.position = pontoInicial.position;

            Debug.Log("Sem checkpoint. Voltando ao início da fase.");
        }
    }
}