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

    // ==========================================
    // MORRER
    // ==========================================

    public void Morrer()
    {
        Respawn();
    }

    // ==========================================
    // RESPAWN
    // ==========================================

    private void Respawn()
    {
        if (rb == null)
        {
            Debug.LogError(
                "Rigidbody não encontrado no Player!"
            );

            return;
        }

        // ======================================
        // TEM CHECKPOINT?
        // ======================================

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

            // Volta às moedas do checkpoint
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
                "Morreu: voltando ao checkpoint."
            );
        }
        else
        {
            // ==================================
            // SEM CHECKPOINT
            // ==================================

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

            // IMPORTANTE:
            // Sem checkpoint, as moedas voltam
            // para 0.
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
                "Voltando ao início com 0 moedas."
            );
        }
    }
}