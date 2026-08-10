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

        if (nomeBolaP1 != null)
        {
            nomeBolaP1.text = bola.ballName;
        }

        if (statusP1 != null)
        {
            statusP1.text =
                "Velocidade: " + bola.speed.ToString("F1") +
                "\nForça: " + bola.pushForce.ToString("F1") +
                "\nPeso: " + bola.weight.ToString("F1") +
                "\nTamanho: " + bola.size.ToString("F1");
        }

        // Imagem principal
        AtualizarImagem(
            imagemBolaP1,
            bola,
            true
        );

        // Preview
        AtualizarImagem(
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

        if (nomeBolaP2 != null)
        {
            nomeBolaP2.text = bola.ballName;
        }

        if (statusP2 != null)
        {
            statusP2.text =
                "Velocidade: " + bola.speed.ToString("F1") +
                "\nForça: " + bola.pushForce.ToString("F1") +
                "\nPeso: " + bola.weight.ToString("F1") +
                "\nTamanho: " + bola.size.ToString("F1");
        }

        // Imagem principal
        AtualizarImagem(
            imagemBolaP2,
            bola,
            false
        );

        // Preview
        AtualizarImagem(
            previewBolaP2,
            bola,
            false
        );
    }


    // =====================================================
    // ATUALIZAR IMAGEM
    // =====================================================

    private void AtualizarImagem(
        Image imagem,
        BallData bola,
        bool jogador1)
    {
        if (imagem == null)
        {
            Debug.LogWarning(
                "Uma imagem da seleção não foi atribuída."
            );

            return;
        }

        if (bola == null)
        {
            Debug.LogWarning(
                "BallData vazio."
            );

            return;
        }

        // Ativa o objeto
        imagem.gameObject.SetActive(true);

        // Sprite do BallData
        if (bola.sprite != null)
        {
            imagem.sprite = bola.sprite;
        }
        else
        {
            Debug.LogWarning(
                "A bolinha " +
                bola.ballName +
                " não possui Sprite."
            );
        }

        // Escolhe a cor
        Color cor;

        if (jogador1)
        {
            cor = bola.player1Color;
        }
        else
        {
            cor = bola.player2Color;
        }

        // Garante que a imagem fique visível
        cor.a = 1f;

        imagem.color = cor;

        // Mantém proporção
        imagem.preserveAspect = true;

        // Tamanho
        float tamanho = bola.size;

        imagem.rectTransform.sizeDelta =
            new Vector2(
                150f * tamanho,
                150f * tamanho
            );

        // Garante que o CanvasRenderer esteja ativo
        CanvasRenderer renderer =
            imagem.GetComponent<CanvasRenderer>();

        if (renderer != null)
        {
            renderer.SetAlpha(1f);
        }
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

        if (BolinhaSelectionData.Instance == null)
        {
            Debug.LogError(
                "BolinhaSelectionData não encontrada!"
            );

            return;
        }

        BolinhaSelectionData.Instance.DefinirEscolhas(
            escolhaP1,
            escolhaP2
        );

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