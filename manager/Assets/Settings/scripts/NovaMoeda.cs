using UnityEngine;

public class NovaMoeda : MonoBehaviour
{
    [SerializeField] private float velocidadeRotacao = 100f;

    private void Update()
    {
        transform.Rotate(0f, velocidadeRotacao * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NovoCoinManager.Instance.AdicionarMoeda();

            Destroy(gameObject);
        }
    }
}