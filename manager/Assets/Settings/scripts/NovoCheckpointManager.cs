using UnityEngine;
using System.Collections.Generic;

public class NovoCheckpointManager : MonoBehaviour
{
    public static NovoCheckpointManager Instance;

    private Vector3 posicaoCheckpoint;
    private int moedasCheckpoint;

    private List<NovaMoeda> moedasColetadasCheckpoint =
        new List<NovaMoeda>();

    private bool checkpointAtivado = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AtivarCheckpoint(Vector3 posicao)
    {
        posicaoCheckpoint = posicao;

        moedasCheckpoint =
            NovoCoinManager.Instance.GetMoedas();

        moedasColetadasCheckpoint.Clear();

        NovaMoeda[] todasAsMoedas =
            FindObjectsByType<NovaMoeda>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (NovaMoeda moeda in todasAsMoedas)
        {
            if (moeda.EstaColetada())
            {
                moedasColetadasCheckpoint.Add(moeda);
            }
        }

        checkpointAtivado = true;

        Debug.Log("Checkpoint salvo!");
        Debug.Log(
            "Moedas no checkpoint: " + moedasCheckpoint
        );
    }

    public bool CheckpointAtivado()
    {
        return checkpointAtivado;
    }

    public Vector3 GetPosicaoCheckpoint()
    {
        return posicaoCheckpoint;
    }

    public int GetMoedasCheckpoint()
    {
        return moedasCheckpoint;
    }

    public void RestaurarMoedasDoCheckpoint()
    {
        NovoCoinManager.Instance.RestaurarMoedasDoCheckpoint(
            moedasColetadasCheckpoint
        );
    }
}