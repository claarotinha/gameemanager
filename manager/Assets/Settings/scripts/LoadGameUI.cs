using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadGameUI : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private Button slot0;
    [SerializeField] private Button slot1;
    [SerializeField] private Button slot2;
    [SerializeField] private Button slot3;

    private void Start()
    {
        AtualizarSlots();
    }

    private void AtualizarSlots()
    {
        slot0.interactable =
            SaveManager.Instance != null &&
            SaveManager.Instance.ExisteSave(0);

        slot1.interactable =
            SaveManager.Instance != null &&
            SaveManager.Instance.ExisteSave(1);

        slot2.interactable =
            SaveManager.Instance != null &&
            SaveManager.Instance.ExisteSave(2);

        slot3.interactable =
            SaveManager.Instance != null &&
            SaveManager.Instance.ExisteSave(3);
    }

    public void CarregarSlot0()
    {
        CarregarSlot(0);
    }

    public void CarregarSlot1()
    {
        CarregarSlot(1);
    }

    public void CarregarSlot2()
    {
        CarregarSlot(2);
    }

    public void CarregarSlot3()
    {
        CarregarSlot(3);
    }

    private void CarregarSlot(int slot)
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogError(
                "SaveManager não encontrado!"
            );

            return;
        }

        if (!SaveManager.Instance.ExisteSave(slot))
        {
            Debug.Log(
                "Slot " + slot + " está vazio."
            );

            return;
        }

        Debug.Log(
            "Carregando slot " + slot
        );

        SaveManager.Instance.Carregar(slot);
    }

    public void Voltar()
    {
        SceneManager.LoadScene(
            "MenuPrincipal"
        );
    }
}