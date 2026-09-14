using UnityEngine;

public class NovaZonaMorte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        NovoPlayerRespawn respawn =
            other.GetComponent<NovoPlayerRespawn>();

        if (respawn != null)
        {
            respawn.Morrer();
        }
    }
}