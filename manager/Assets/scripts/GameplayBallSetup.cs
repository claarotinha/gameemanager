using UnityEngine;

public class GameplayBallSetup : MonoBehaviour
{
    [Header("Bolinhas do Gameplay")]
    public BolinhaController bolinhaP1;
    public BolinhaController bolinhaP2;

    private void Start()
    {
        AplicarEscolhas();
    }

    private void AplicarEscolhas()
    {
        if (BolinhaSelectionData.Instance == null)
        {
            Debug.LogError("BolinhaSelectionData não encontrada!");
            return;
        }

        if (BolinhaSelectionData.Instance.escolhaP1 == null)
        {
            Debug.LogError("P1 não possui uma bolinha escolhida!");
            return;
        }

        if (BolinhaSelectionData.Instance.escolhaP2 == null)
        {
            Debug.LogError("P2 não possui uma bolinha escolhida!");
            return;
        }

        bolinhaP1.ballData =
            BolinhaSelectionData.Instance.escolhaP1;

        bolinhaP2.ballData =
            BolinhaSelectionData.Instance.escolhaP2;

        Debug.Log("Bolinha P1: " +
            bolinhaP1.ballData.ballName);

        Debug.Log("Bolinha P2: " +
            bolinhaP2.ballData.ballName);

        AplicarDados(bolinhaP1);
        AplicarDados(bolinhaP2);

        AplicarCor(bolinhaP1, true);
        AplicarCor(bolinhaP2, false);
    }

    private void AplicarDados(BolinhaController bolinha)
    {
        bolinha.RecarregarDados();
    }

    private void AplicarCor(BolinhaController bolinha, bool jogador1)
    {
        Renderer renderer = bolinha.GetComponent<Renderer>();

        if (renderer == null)
        {
            Debug.LogError(
                "A bolinha " + bolinha.gameObject.name +
                " não possui Renderer."
            );

            return;
        }

        Color cor;

        if (jogador1)
        {
            cor = bolinha.ballData.player1Color;
        }
        else
        {
            cor = bolinha.ballData.player2Color;
        }

        renderer.material.color = cor;
    }
}