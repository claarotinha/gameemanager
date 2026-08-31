using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadScene("SelecaoBolinhas");
    }

    public void StartPlatformGame()
    {
        GameManager.Instance.LoadScene("NovoJogo");
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}