using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject painelPausa;

    private bool pausado = false;

    private void Start()
    {
        painelPausa.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        if (pausado)
        {
            Continuar();
        }
        else
        {
            Pausar();
        }
    }

    // ==========================================
    // PAUSAR
    // ==========================================

    public void Pausar()
    {
        pausado = true;

        painelPausa.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Jogo pausado.");
    }

    // ==========================================
    // CONTINUAR
    // ==========================================

    public void Continuar()
    {
        pausado = false;

        painelPausa.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("Jogo continuando.");
    }

    // ==========================================
    // CARREGAR JOGO
    // ==========================================

    public void AbrirCarregarJogo()
    {
        // Despausa antes de trocar de cena
        Time.timeScale = 1f;

        Debug.Log(
            "Abrindo tela de Carregar Jogo..."
        );

        SceneManager.LoadScene(
            "CarregarJogo"
        );
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

    // ==========================================
    // GARANTIR QUE O TEMPO VOLTE
    // ==========================================

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}