using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Jogo antigo das bolinhas
    public void StartGame()
    {
        GameManager.Instance.LoadScene(
            "SelecaoBolinhas"
        );
    }

    // Abre a tela do novo jogo
    public void StartNewGame()
    {
        GameManager.Instance.LoadScene(
            "NovoJogo"
        );
    }

    // Começa o novo jogo
    public void StartPlatformGame()
    {
        GameManager.Instance.LoadScene(
            "Fase1"
        );
    }

    // Abre a tela de carregar
    public void OpenLoadGame()
    {
        GameManager.Instance.LoadScene(
            "CarregarJogo"
        );
    }

    // Volta para o menu principal
    public void BackToMenu()
    {
        GameManager.Instance.LoadScene(
            "MenuPrincipal"
        );
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}