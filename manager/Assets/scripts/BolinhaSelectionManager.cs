using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BolinhaSelectionManager : MonoBehaviour
{
    [Header("Bolinhas disponíveis")]
    public BallData[] bolinhas;

    [Header("P1 - Interface")]
    public Image imagemBolaP1;
    public TMP_Text nomeBolaP1;
    public TMP_Text statusP1;

    [Header("P2 - Interface")]
    public Image imagemBolaP2;
    public TMP_Text nomeBolaP2;
    public TMP_Text statusP2;

    [Header("Preview")]
    public Image previewBolaP1;
    public Image previewBolaP2;

    [Header("Estado da seleção")]
    private int indiceP1 = 0;
    private int indiceP2 = 0;

    private bool p1Confirmado = false;
    private bool p2Confirmado = false;

    public BallData escolhaP1 { get; private set; }
    public BallData escolhaP2 { get; private set; }


    private void Start()
    {
        AtualizarP1();
        AtualizarP2();
    }


    // =====================================================
    // NAVEGAÇÃO P1
    // =====================================================

    public void ProximaBolaP1()
    {
        if (p1Confirmado)
            return;

        indiceP1++;

        if (indiceP1 >= bolinhas.Length)
            indiceP1 = 0;

        AtualizarP1();
    }


    public void BolaAnteriorP1()
    {
        if (p1Confirmado)
            return;

        indiceP1--;

        if (indiceP1 < 0)
            indiceP1 = bolinhas.Length - 1;

        AtualizarP1();
    }


    // =====================================================
    // NAVEGAÇÃO P2
    // =====================================================

    public void ProximaBolaP2()
    {
        if (p2Confirmado)
            return;

        indiceP2++;

        if (indiceP2 >= bolinhas.Length)
            indiceP2 = 0;

        AtualizarP2();
    }


    public void BolaAnteriorP2()
    {
        if (p2Confirmado)
            return;

        indiceP2--;

        if (indiceP2 < 0)
            indiceP2 = bolinhas.Length - 1;

        AtualizarP2();
    }


    // =====================================================
    // ATUALIZAR P1
    // =====================================================

    private void AtualizarP1()
    {
        if (bolinhas == null || bolinhas.Length == 0)
            return;

        BallData bola = bolinhas[indiceP1];

        // Mantido caso vocês utilizem Sprite futuramente
        if (imagemBolaP1 != null)
            imagemBolaP1.sprite = bola.sprite;

        if (nomeBolaP1 != null)
            nomeBolaP1.text = bola.ballName;

        if (statusP1 != null)
        {
            statusP1.text =
                "Velocidade: " + bola.speed.ToString("F1") +
                "\nForça: " + bola.pushForce.ToString("F1") +
                "\nPeso: " + bola.weight.ToString("F1") +
                "\nTamanho: " + bola.size.ToString("F1");
        }

        AtualizarPreview(
            previewBolaP1,
            bola,
            true
        );
    }


    // =====================================================
    // ATUALIZAR P2
    // =====================================================

    private void AtualizarP2()
    {
        if (bolinhas == null || bolinhas.Length == 0)
            return;

        BallData bola = bolinhas[indiceP2];

        // Mantido caso vocês utilizem Sprite futuramente
        if (imagemBolaP2 != null)
            imagemBolaP2.sprite = bola.sprite;

        if (nomeBolaP2 != null)
            nomeBolaP2.text = bola.ballName;

        if (statusP2 != null)
        {
            statusP2.text =
                "Velocidade: " + bola.speed.ToString("F1") +
                "\nForça: " + bola.pushForce.ToString("F1") +
                "\nPeso: " + bola.weight.ToString("F1") +
                "\nTamanho: " + bola.size.ToString("F1");
        }

        AtualizarPreview(
            previewBolaP2,
            bola,
            false
        );
    }


    // =====================================================
    // PREVIEW
    // =====================================================

    private void AtualizarPreview(
        Image preview,
        BallData bola,
        bool jogador1)
    {
        if (preview == null || bola == null)
            return;

        // Escolhe a cor de acordo com o jogador
        Color cor;

        if (jogador1)
            cor = bola.player1Color;
        else
            cor = bola.player2Color;

        // Aplica a cor
        preview.color = cor;

        // Altera o tamanho da imagem de acordo
        // com o tamanho da bolinha escolhida
        float tamanho = bola.size;

        preview.rectTransform.sizeDelta =
            new Vector2(
                150f * tamanho,
                150f * tamanho
            );
    }


    // =====================================================
    // CONFIRMAR P1
    // =====================================================

    public void ConfirmarP1()
    {
        if (p1Confirmado)
            return;

        escolhaP1 = bolinhas[indiceP1];

        p1Confirmado = true;

        Debug.Log(
            "P1 escolheu: " +
            escolhaP1.ballName
        );

        VerificarConfirmacoes();
    }


    // =====================================================
    // CONFIRMAR P2
    // =====================================================

    public void ConfirmarP2()
    {
        if (p2Confirmado)
            return;

        escolhaP2 = bolinhas[indiceP2];

        p2Confirmado = true;

        Debug.Log(
            "P2 escolheu: " +
            escolhaP2.ballName
        );

        VerificarConfirmacoes();
    }


    // =====================================================
    // VERIFICAR CONFIRMAÇÕES
    // =====================================================

    private void VerificarConfirmacoes()
    {
        if (!p1Confirmado || !p2Confirmado)
            return;

        Debug.Log(
            "Os dois jogadores confirmaram!"
        );

        // Verifica se existe o objeto que guarda
        // as escolhas dos jogadores.
        if (BolinhaSelectionData.Instance == null)
        {
            Debug.LogError(
                "BolinhaSelectionData não encontrada!"
            );

            return;
        }

        // Guarda as escolhas
        BolinhaSelectionData.Instance.DefinirEscolhas(
            escolhaP1,
            escolhaP2
        );

        // Usa o GameManager para carregar a Gameplay
        // e a GUI.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene(
                "SampleScene"
            );
        }
        else
        {
            Debug.LogError(
                "GameManager.Instance não encontrado!"
            );
        }
    }
}