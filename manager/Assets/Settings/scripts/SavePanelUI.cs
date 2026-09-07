using UnityEngine;

public class SavePanelUI : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject painelSalvar;

    [Header("Pause")]
    [SerializeField] private GameObject painelPausa;

    private void Start()
    {
        painelSalvar.SetActive(false);
    }

    // Abrir painel de salvar
    public void AbrirPainelSalvar()
    {
        painelSalvar.SetActive(true);

        if (painelPausa != null)
        {
            painelPausa.SetActive(false);
        }
    }

    // Salvar no Slot 1
    public void SalvarSlot1()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SalvarManual(1);
        }
    }

    // Salvar no Slot 2
    public void SalvarSlot2()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SalvarManual(2);
        }
    }

    // Salvar no Slot 3
    public void SalvarSlot3()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SalvarManual(3);
        }
    }

    // Voltar para o menu de pausa
    public void Voltar()
    {
        painelSalvar.SetActive(false);

        if (painelPausa != null)
        {
            painelPausa.SetActive(true);
        }
    }
}