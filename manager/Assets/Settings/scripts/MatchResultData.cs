using UnityEngine;

public class MatchResultData : MonoBehaviour
{
    public static MatchResultData Instance;

    public int jogadorVencedor;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DefinirVencedor(int jogador)
    {
        jogadorVencedor = jogador;

        Debug.Log("Vencedor da partida: Jogador " + jogador);
    }
}