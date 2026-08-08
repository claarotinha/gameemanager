using UnityEngine;

public class BolinhaSelectionData : MonoBehaviour
{
    public static BolinhaSelectionData Instance;

    public BallData escolhaP1;
    public BallData escolhaP2;

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

    public void DefinirEscolhas(BallData p1, BallData p2)
    {
        escolhaP1 = p1;
        escolhaP2 = p2;
    }
}