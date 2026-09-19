using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // ==========================================
    // JOGO ANTIGO DAS BOLINHAS
    // ==========================================

    public void StartGame()
    {
        GameManager.Instance.LoadScene(
            "SelecaoBolinhas"
        );
    }

    // ==========================================
    // ABRIR TELA "NOVO JOGO"
    // ==========================================

    public void StartNewGame()
    {
        GameManager.Instance.LoadScene(
            "NovoJogo"
        );
    }

    // ==========================================
    // COMEÇAR NOVA PARTIDA
    // NÃO APAGA NENHUM SAVE
    // ==========================================

    public void StartPlatformGame()
    {
        GameManager.Instance.LoadScene(
            "Fase1"
        );
    }

    // ==========================================
    // CARREGAR JOGO
    // ==========================================

    public void OpenLoadGame()
    {
        GameManager.Instance.LoadScene(
            "CarregarJogo"
        );
    }

    // ==========================================
    // VOLTAR AO MENU
    // ==========================================

    public void BackToMenu()
    {
        GameManager.Instance.LoadScene(
            "MenuPrincipal"
        );
    }

    // ==========================================
    // SAIR
    // ==========================================

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}