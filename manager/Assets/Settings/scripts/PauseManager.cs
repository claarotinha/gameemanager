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

    public void Pausar()
    {
        pausado = true;

        painelPausa.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Jogo pausado.");
    }

    public void Continuar()
    {
        pausado = false;

        painelPausa.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("Jogo continuando.");
    }

    // ==========================================
    // VOLTAR AO MENU
    // ==========================================

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