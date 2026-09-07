using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NovaTelaFinal : MonoBehaviour
{
    [SerializeField] private GameObject painelFinal;
    [SerializeField] private TMP_Text textoMoedas;

    private void Start()
    {
        painelFinal.SetActive(false);
    }

    public void MostrarFinal()
    {
        int moedas = NovoCoinManager.Instance.GetMoedas();
        int total = NovoCoinManager.Instance.GetTotalMoedas();

        painelFinal.SetActive(true);

        textoMoedas.text =
            "Moedas: " + moedas + "/" + total;

        Time.timeScale = 0f;
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MenuPrincipal");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}