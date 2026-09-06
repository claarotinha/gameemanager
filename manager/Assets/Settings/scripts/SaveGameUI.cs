using UnityEngine;

public class SaveGameUI : MonoBehaviour
{
    public void SalvarSlot1()
    {
        Salvar(1);
    }

    public void SalvarSlot2()
    {
        Salvar(2);
    }

    public void SalvarSlot3()
    {
        Salvar(3);
    }

    private void Salvar(int slot)
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogError(
                "SaveManager não encontrado!"
            );

            return;
        }

        SaveManager.Instance.SalvarManual(slot);

        Debug.Log(
            "Slot " + slot +
            " salvo com sucesso!"
        );
    }
}