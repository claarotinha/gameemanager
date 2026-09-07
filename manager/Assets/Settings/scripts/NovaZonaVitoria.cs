using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NovaZonaVitoria : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private TMP_Text textoVitoria;
    [SerializeField] private TMP_Text textoContinuar;

    [Header("Próxima fase")]
    [SerializeField] private string proximaFase = "Fase2";

    [Header("Última fase")]
    [SerializeField] private bool ultimaFase = false;
    [SerializeField] private NovaTelaFinal telaFinal;

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

        // ==========================================
        // AUTOSAVE
        // ==========================================

        if (SaveManager.Instance != null)
        {
            SaveData dados = new SaveData();

            if (ultimaFase)
            {
                // Última fase concluída
                dados.fase = "Fase2";
            }
            else
            {
                // Salva a próxima fase
                dados.fase = proximaFase;
            }

            dados.checkpointAtivado = false;

            dados.checkpointX = 0f;
            dados.checkpointY = 0f;
            dados.checkpointZ = 0f;

            dados.moedasCheckpoint = 0;

            dados.moedasColetadasCheckpoint =
                new List<string>();

            dados.faseConcluida = true;

            SaveManager.Instance.Salvar(
                dados,
                0
            );

            Debug.Log(
                "Conclusão da fase salva no Slot 0!"
            );
        }
    }

    private void Update()
    {
        if (!venceu)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            if (ultimaFase)
            {
                if (telaFinal != null)
                {
                    telaFinal.MostrarFinal();
                }
                else
                {
                    Debug.LogError(
                        "Tela Final não foi atribuída!"
                    );
                }
            }
            else
            {
                SceneManager.LoadScene(
                    proximaFase
                );
            }
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}