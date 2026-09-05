using UnityEngine;

public class NovoPlayerRespawn : MonoBehaviour
{
    [SerializeField] private float alturaDeMorte = -5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < alturaDeMorte)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (NovoCheckpointManager.Instance.CheckpointAtivado())
        {
            Vector3 posicao =
                NovoCheckpointManager.Instance.GetPosicaoCheckpoint();

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.position = posicao;

            NovoCoinManager.Instance.DefinirMoedas(
                NovoCheckpointManager.Instance.GetMoedasCheckpoint()
            );

            NovoCheckpointManager.Instance
                .RestaurarMoedasDoCheckpoint();
        }
        else
        {
            Debug.Log("Nenhum checkpoint ativado.");
        }
    }
}