using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);
        GameManager.Instance.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}