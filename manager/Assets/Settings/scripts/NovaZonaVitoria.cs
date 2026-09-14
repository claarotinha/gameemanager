using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NovaZonaVitoria : MonoBehaviour
{
    [Header("Painel da Vitória")]
    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private TMP_Text textoVitoria;
    [SerializeField] private TMP_Text textoContinuar;

    [Header("Próxima fase")]
    [SerializeField] private string proximaFase = "Fase2";

    [Header("Última fase")]
    [SerializeField] private bool ultimaFase = false;

    [Header("Tela Final")]
    [SerializeField] private GameObject painelFinal;
    [SerializeField] private TMP_Text textoMoedasFinal;

    private bool venceu = false;
    private bool abriuTelaFinal = false;

    private void Start()
    {
        painelVitoria.SetActive(false);

        if (painelFinal != null)
        {
            painelFinal.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (venceu)
            return;

        if (!other.CompareTag("Player"))
            return;

        venceu = true;

        // ==========================================
        // PEGAR MOEDAS ATUAIS
        // ==========================================

        int moedas = NovoCoinManager.Instance.GetMoedas();
        int total = NovoCoinManager.Instance.GetTotalMoedas();

        // ==========================================
        // MOSTRAR PAINEL DE VITÓRIA
        // ==========================================

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
                // Fase 2 é a última
                dados.fase = "Fase2";
            }
            else
            {
                // Fase 1 salva a próxima fase
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

        Debug.Log("Fase concluída!");
        Debug.Log("Moedas: " + moedas + "/" + total);
    }

    private void Update()
    {
        if (!venceu)
            return;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            // ==========================================
            // SE FOR A ÚLTIMA FASE
            // ==========================================

            if (ultimaFase)
            {
                AbrirTelaFinal();
            }
            else
            {
                // ==========================================
                // SE NÃO FOR A ÚLTIMA FASE
                // ==========================================

                SceneManager.LoadScene(
                    proximaFase
                );
            }
        }
    }

    // ==========================================
    // TELA FINAL
    // ==========================================

    private void AbrirTelaFinal()
    {
        if (painelFinal == null)
        {
            Debug.LogError(
                "Painel Final não foi atribuído!"
            );

            return;
        }

        abriuTelaFinal = true;

        painelVitoria.SetActive(false);

        painelFinal.SetActive(true);

        int moedas =
            NovoCoinManager.Instance.GetMoedas();

        int total =
            NovoCoinManager.Instance.GetTotalMoedas();

        if (textoMoedasFinal != null)
        {
            textoMoedasFinal.text =
                "Moedas: " +
                moedas +
                "/" +
                total;
        }

        Time.timeScale = 0f;

        Debug.Log("Tela final aberta!");
    }

    // ==========================================
    // VOLTAR AO MENU
    // ==========================================

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MenuPrincipal"
        );
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}