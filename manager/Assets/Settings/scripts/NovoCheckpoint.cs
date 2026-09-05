using UnityEngine;

public class NovoCheckpoint : MonoBehaviour
{
    private bool ativado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ativado)
            return;

        ativado = true;

        NovoCheckpointManager.Instance.AtivarCheckpoint(
            transform.position
        );

        Debug.Log("Checkpoint ativado!");
    }
}