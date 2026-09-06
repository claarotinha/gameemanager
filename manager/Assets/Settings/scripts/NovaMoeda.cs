using UnityEngine;

public class NovaMoeda : MonoBehaviour
{
    [SerializeField] private float velocidadeRotacao = 100f;

    private bool coletada = false;

    private void Update()
    {
        transform.Rotate(
            0f,
            velocidadeRotacao * Time.deltaTime,
            0f
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (coletada)
            return;

        if (other.CompareTag("Player"))
        {
            coletada = true;

            NovoCoinManager.Instance
                .AdicionarMoeda(this);

            gameObject.SetActive(false);
        }
    }

    public void RestaurarMoeda()
    {
        coletada = false;
        gameObject.SetActive(true);
    }

    public bool EstaColetada()
    {
        return coletada;
    }

    public string GetID()
    {
        return gameObject.name;
    }
}