using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TelaVitoriaManager : MonoBehaviour
{
    [Header("Interface")]
    public TMP_Text textoVencedor;
    public TMP_Text textoBolinha;

    private void Start()
    {
        MostrarResultado();
    }

    private void MostrarResultado()
    {
        if (MatchResultData.Instance == null)
        {
            Debug.LogError("MatchResultData não encontrada!");
            return;
        }

        if (BolinhaSelectionData.Instance == null)
        {
            Debug.LogError("BolinhaSelectionData não encontrada!");
            return;
        }

        int vencedor = MatchResultData.Instance.jogadorVencedor;

        BallData bolinhaVencedora = null;

        if (vencedor == 1)
        {
            bolinhaVencedora =
                BolinhaSelectionData.Instance.escolhaP1;
        }
        else if (vencedor == 2)
        {
            bolinhaVencedora =
                BolinhaSelectionData.Instance.escolhaP2;
        }

        if (textoVencedor != null)
        {
            textoVencedor.text =
                "JOGADOR " + vencedor + " VENCEU!";
        }

        if (textoBolinha != null && bolinhaVencedora != null)
        {
            textoBolinha.text =
                "Bolinha: " + bolinhaVencedora.ballName;
        }
    }

    public void VoltarParaSelecao()
    {
        SceneManager.LoadScene("SelecaoBolinhas");
    }
}