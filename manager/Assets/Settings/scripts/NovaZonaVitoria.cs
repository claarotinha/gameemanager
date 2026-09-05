using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NovaZonaVitoria : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private TMP_Text textoVitoria;
    [SerializeField] private TMP_Text textoContinuar;

    [Header("Próxima fase")]
    [SerializeField] private string proximaFase = "Fase2";

    private bool venceu = false;

    private void Start()
    {
        painelVitoria.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (venceu)
            return;

        if (!other.CompareTag("Player"))
            return;

        venceu = true;

        int moedas = NovoCoinManager.Instance.GetMoedas();
        int total = NovoCoinManager.Instance.GetTotalMoedas();

        painelVitoria.SetActive(true);

        textoVitoria.text = "VITÓRIA!";

        textoContinuar.text =
            "Moedas: " + moedas + "/" + total +
            "\n\nPressione ESPAÇO para continuar";

        Time.timeScale = 0f;

        Debug.Log("Fase concluída!");
        Debug.Log("Moedas: " + moedas + "/" + total);
    }

    private void Update()
    {
        if (!venceu)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(proximaFase);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}