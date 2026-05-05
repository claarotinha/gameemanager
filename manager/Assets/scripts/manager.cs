using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState currentState;

    void Awake()
    {
        // Singleton
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        ChangeState(GameState.Iniciando);

        Debug.Log("Carregando Splash...");
        LoadScene("Splash"); // começa indo pra Splash
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado atual: " + currentState);
    }

    // SÓ o GameManager troca cena
    public void LoadScene(string sceneName)
    {
        Debug.Log("Tentando carregar: " + sceneName);

        if (currentState == GameState.Iniciando ||
            currentState == GameState.MenuPrincipal ||
            currentState == GameState.Gameplay)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Troca de cena bloqueada no estado: " + currentState);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada: " + scene.name);

        if (scene.name == "MenuPrincipal")
        {
            Debug.Log("Menu Principal carregado!");
            ChangeState(GameState.MenuPrincipal);
        }
    }
}