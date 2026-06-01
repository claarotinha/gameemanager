using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState CurrentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.Iniciando);
        LoadScene("Splash");
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Estado atual: " + newState);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        switch (sceneName)
        {
            case "MenuPrincipal":
                SetState(GameState.MenuPrincipal);
                break;

            case "SampleScene":
                SetState(GameState.Gameplay);

                // Carrega a cena GUI junto da Gameplay
                SceneManager.LoadScene("GUI", LoadSceneMode.Additive);

                Debug.Log("Gameplay carregada");
                Debug.Log("GUI carregada");
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}