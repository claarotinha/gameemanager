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
        if (rb == null)
        {
            Debug.LogError(
                "Rigidbody não encontrado no Player!"
            );

            return;
        }

        // ==========================================
        // COM CHECKPOINT
        // ==========================================

        if (
            NovoCheckpointManager.Instance != null &&
            NovoCheckpointManager.Instance
                .CheckpointAtivado()
        )
        {
            Vector3 posicaoRespawn =
                NovoCheckpointManager.Instance
                    .GetPosicaoCheckpoint();

            rb.linearVelocity =
                Vector3.zero;

            rb.angularVelocity =
                Vector3.zero;

            rb.position =
                posicaoRespawn;

            if (NovoCoinManager.Instance != null)
            {
                NovoCoinManager.Instance
                    .DefinirMoedas(
                        NovoCheckpointManager.Instance
                            .GetMoedasCheckpoint()
                    );

                NovoCheckpointManager.Instance
                    .RestaurarMoedasDoCheckpoint();
            }

            Debug.Log(
                "Morreu: voltou para o checkpoint."
            );
        }

        // ==========================================
        // SEM CHECKPOINT
        // ==========================================

        else
        {
            if (pontoInicial == null)
            {
                Debug.LogError(
                    "Ponto Inicial não foi atribuído!"
                );

                return;
            }

            rb.linearVelocity =
                Vector3.zero;

            rb.angularVelocity =
                Vector3.zero;

            rb.position =
                pontoInicial.position;

            if (NovoCoinManager.Instance != null)
            {
                NovoCoinManager.Instance
                    .DefinirMoedas(0);

                NovoCoinManager.Instance
                    .RestaurarTodasAsMoedas();
            }

            Debug.Log(
                "Morreu sem checkpoint."
            );

            Debug.Log(
                "Voltou ao início com 0 moedas."
            );
        }
    }
}