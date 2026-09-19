using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NovoCheckpointManager : MonoBehaviour
{
    public static NovoCheckpointManager Instance;

    // ==========================================
    // DADOS DO CHECKPOINT
    // ==========================================

    private Vector3 posicaoCheckpoint;

    private int moedasCheckpoint;

    private List<NovaMoeda> moedasColetadasCheckpoint =
        new List<NovaMoeda>();

    private bool checkpointAtivado = false;

    // ==========================================
    // AWAKE
    // ==========================================

    private void Awake()
    {
        Instance = this;
    }

    // ==========================================
    // ATIVAR CHECKPOINT
    // ==========================================

    public void AtivarCheckpoint(Vector3 posicao)
    {
        posicaoCheckpoint = posicao;

        moedasCheckpoint =
            NovoCoinManager.Instance.GetMoedas();

        moedasColetadasCheckpoint.Clear();

        NovaMoeda[] todasAsMoedas =
            FindObjectsByType<NovaMoeda>(
                FindObjectsInactive.Include
            );

        foreach (NovaMoeda moeda in todasAsMoedas)
        {
            if (moeda.EstaColetada())
            {
                moedasColetadasCheckpoint.Add(moeda);
            }
        }

        checkpointAtivado = true;

        // ======================================
        // AUTOSAVE NO SLOT 0
        // ======================================

        if (SaveManager.Instance != null)
        {
            SaveData dados =
                CriarDadosDoSave();

            SaveManager.Instance.Salvar(
                dados,
                0
            );

            Debug.Log(
                "Autosave realizado no checkpoint!"
            );
        }
        else
        {
            Debug.LogError(
                "SaveManager não encontrado!"
            );
        }

        Debug.Log(
            "Checkpoint ativado!"
        );

        Debug.Log(
            "Moedas no checkpoint: " +
            moedasCheckpoint
        );
    }

    // ==========================================
    // CRIAR DADOS DO SAVE
    // ==========================================

    public SaveData CriarDadosDoSave()
    {
        SaveData dados =
            new SaveData();

        dados.fase =
            SceneManager
            .GetActiveScene()
            .name;

        dados.checkpointAtivado =
            checkpointAtivado;

        // ======================================
        // SEM CHECKPOINT
        // ======================================

        if (!checkpointAtivado)
        {
            dados.checkpointX = 0f;
            dados.checkpointY = 0f;
            dados.checkpointZ = 0f;

            dados.moedasCheckpoint = 0;

            dados.moedasColetadasCheckpoint =
                new List<string>();

            dados.faseConcluida = false;

            return dados;
        }

        // ======================================
        // COM CHECKPOINT
        // ======================================

        dados.checkpointX =
            posicaoCheckpoint.x;

        dados.checkpointY =
            posicaoCheckpoint.y;

        dados.checkpointZ =
            posicaoCheckpoint.z;

        dados.moedasCheckpoint =
            moedasCheckpoint;

        dados.moedasColetadasCheckpoint =
            GetNomesMoedasCheckpoint();

        dados.faseConcluida = false;

        return dados;
    }

    // ==========================================
    // GETTERS
    // ==========================================

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

    // ==========================================
    // RESTAURAR MOEDAS
    // ==========================================

    public void RestaurarMoedasDoCheckpoint()
    {
        if (NovoCoinManager.Instance == null)
            return;

        NovoCoinManager.Instance
            .RestaurarMoedasPorNome(
                GetNomesMoedasCheckpoint()
            );
    }

    // ==========================================
    // NOMES DAS MOEDAS
    // ==========================================

    private List<string> GetNomesMoedasCheckpoint()
    {
        List<string> nomes =
            new List<string>();

        foreach (
            NovaMoeda moeda
            in moedasColetadasCheckpoint
        )
        {
            if (moeda != null)
            {
                nomes.Add(
                    moeda.GetID()
                );
            }
        }

        return nomes;
    }

    // ==========================================
    // CARREGAR CHECKPOINT
    // ==========================================

    public void CarregarCheckpoint(
        bool ativado,
        Vector3 posicao,
        int moedas,
        List<string> moedasColetadas
    )
    {
        checkpointAtivado =
            ativado;

        posicaoCheckpoint =
            posicao;

        moedasCheckpoint =
            moedas;

        moedasColetadasCheckpoint.Clear();

        if (moedasColetadas == null)
        {
            moedasColetadas =
                new List<string>();
        }

        NovaMoeda[] todasAsMoedas =
            FindObjectsByType<NovaMoeda>(
                FindObjectsInactive.Include
            );

        foreach (
            NovaMoeda moeda
            in todasAsMoedas
        )
        {
            if (
                moedasColetadas.Contains(
                    moeda.GetID()
                )
            )
            {
                moedasColetadasCheckpoint.Add(
                    moeda
                );
            }
        }

        Debug.Log(
            "Checkpoint carregado!"
        );
    }
}