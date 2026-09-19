using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private SaveData dadosCarregados;

    private const string chave =
        "Plataforma3D_2026_Save_Key_123456";

    // ==========================================
    // AWAKE
    // ==========================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded +=
                AoCarregarCena;

            Debug.Log(
                "SaveManager iniciado."
            );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ==========================================
    // DESTROY
    // ==========================================

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -=
                AoCarregarCena;
        }
    }

    // ==========================================
    // SALVAR
    // ==========================================

    public void Salvar(
        SaveData dados,
        int slot
    )
    {
        string caminho =
            ObterCaminho(slot);

        string json =
            JsonUtility.ToJson(dados);

        string textoCriptografado =
            Criptografar(json);

        File.WriteAllText(
            caminho,
            textoCriptografado
        );

        Debug.Log(
            "Jogo salvo no Slot " +
            slot
        );

        Debug.Log(
            "Fase: " +
            dados.fase
        );

        Debug.Log(
            "Checkpoint: " +
            dados.checkpointAtivado
        );

        Debug.Log(
            "Moedas do checkpoint: " +
            dados.moedasCheckpoint
        );
    }

    // ==========================================
    // SAVE MANUAL
    // ==========================================

    public void SalvarManual(
        int slot
    )
    {
        if (
            slot < 1 ||
            slot > 3
        )
        {
            Debug.LogError(
                "Slot manual inválido."
            );

            return;
        }

        if (
            NovoCheckpointManager.Instance == null
        )
        {
            Debug.LogError(
                "NovoCheckpointManager não encontrado!"
            );

            return;
        }

        // ======================================
        // IMPORTANTE:
        // NÃO salva a posição atual.
        //
        // Salva somente o checkpoint.
        // ======================================

        SaveData dados =
            NovoCheckpointManager.Instance
                .CriarDadosDoSave();

        // Salva no slot escolhido
        Salvar(
            dados,
            slot
        );

        // Também copia para o Slot 0
        Salvar(
            dados,
            0
        );

        Debug.Log(
            "================================"
        );

        Debug.Log(
            "SAVE MANUAL REALIZADO"
        );

        Debug.Log(
            "Slot: " +
            slot
        );

        Debug.Log(
            "Checkpoint: " +
            dados.checkpointAtivado
        );

        Debug.Log(
            "Moedas do checkpoint: " +
            dados.moedasCheckpoint
        );

        Debug.Log(
            "================================"
        );
    }

    // ==========================================
    // EXISTE SAVE
    // ==========================================

    public bool ExisteSave(
        int slot
    )
    {
        return File.Exists(
            ObterCaminho(slot)
        );
    }

    // ==========================================
    // CARREGAR
    // ==========================================

    public void Carregar(
        int slot
    )
    {
        string caminho =
            ObterCaminho(slot);

        if (!File.Exists(caminho))
        {
            Debug.Log(
                "Slot " +
                slot +
                " está vazio."
            );

            return;
        }

        Time.timeScale = 1f;

        string textoCriptografado =
            File.ReadAllText(
                caminho
            );

        string json =
            Descriptografar(
                textoCriptografado
            );

        dadosCarregados =
            JsonUtility.FromJson<SaveData>(
                json
            );

        Debug.Log(
            "================================"
        );

        Debug.Log(
            "CARREGANDO SAVE"
        );

        Debug.Log(
            "Slot: " +
            slot
        );

        Debug.Log(
            "Fase: " +
            dadosCarregados.fase
        );

        Debug.Log(
            "Checkpoint: " +
            dadosCarregados.checkpointAtivado
        );

        Debug.Log(
            "================================"
        );

        // ======================================
        // SLOT MANUAL → SLOT 0
        // ======================================

        if (slot != 0)
        {
            Salvar(
                dadosCarregados,
                0
            );
        }

        // ======================================
        // FORÇA CARREGAMENTO DA FASE
        // ======================================

        SceneManager.LoadScene(
            dadosCarregados.fase,
            LoadSceneMode.Single
        );
    }

    // ==========================================
    // CENA CARREGADA
    // ==========================================

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo
    )
    {
        if (dadosCarregados == null)
            return;

        if (
            cena.name !=
            dadosCarregados.fase
        )
            return;

        AplicarDados();

        dadosCarregados = null;
    }

    // ==========================================
    // APLICAR SAVE
    // ==========================================

    private void AplicarDados()
    {
        if (dadosCarregados == null)
            return;

        // ======================================
        // MOEDAS
        // ======================================

        if (
            NovoCoinManager.Instance != null
        )
        {
            NovoCoinManager.Instance
                .DefinirMoedas(
                    dadosCarregados
                        .moedasCheckpoint
                );

            NovoCoinManager.Instance
                .RestaurarMoedasPorNome(
                    dadosCarregados
                        .moedasColetadasCheckpoint
                );
        }

        // ======================================
        // CHECKPOINT
        // ======================================

        if (
            NovoCheckpointManager.Instance
            != null
        )
        {
            Vector3 posicaoCheckpoint =
                new Vector3(
                    dadosCarregados.checkpointX,
                    dadosCarregados.checkpointY,
                    dadosCarregados.checkpointZ
                );

            NovoCheckpointManager.Instance
                .CarregarCheckpoint(
                    dadosCarregados
                        .checkpointAtivado,

                    posicaoCheckpoint,

                    dadosCarregados
                        .moedasCheckpoint,

                    dadosCarregados
                        .moedasColetadasCheckpoint
                );
        }

        // ======================================
        // POSIÇÃO DO PLAYER
        // ======================================

        GameObject player =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (player == null)
        {
            Debug.LogError(
                "Player não encontrado!"
            );

            return;
        }

        Rigidbody rb =
            player.GetComponent<Rigidbody>();

        // ======================================
        // COM CHECKPOINT
        // ======================================

        if (
            dadosCarregados
                .checkpointAtivado
        )
        {
            Vector3 posicao =
                new Vector3(
                    dadosCarregados.checkpointX,
                    dadosCarregados.checkpointY,
                    dadosCarregados.checkpointZ
                );

            if (rb != null)
            {
                rb.linearVelocity =
                    Vector3.zero;

                rb.angularVelocity =
                    Vector3.zero;

                rb.position =
                    posicao;
            }
            else
            {
                player.transform.position =
                    posicao;
            }

            Debug.Log(
                "Player carregado no CHECKPOINT."
            );
        }

        // ======================================
        // SEM CHECKPOINT
        // ======================================

        else
        {
            // Não colocamos uma posição manual.
            // O Player começa normalmente no
            // PontoInicial da cena.

            if (NovoCoinManager.Instance != null)
            {
                NovoCoinManager.Instance
                    .DefinirMoedas(0);

                NovoCoinManager.Instance
                    .RestaurarTodasAsMoedas();
            }

            Debug.Log(
                "Save sem checkpoint."
            );

            Debug.Log(
                "Player começará no início da fase."
            );
        }

        Debug.Log(
            "Save aplicado com sucesso!"
        );
    }

    // ==========================================
    // CAMINHO DO SAVE
    // ==========================================

    private string ObterCaminho(
        int slot
    )
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save_slot_" +
            slot +
            ".dat"
        );
    }

    // ==========================================
    // CRIPTOGRAFAR
    // ==========================================

    private string Criptografar(
        string texto
    )
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key =
                GerarChave();

            aes.IV =
                new byte[16];

            byte[] dados =
                Encoding.UTF8.GetBytes(
                    texto
                );

            using (
                MemoryStream memoria =
                new MemoryStream()
            )
            {
                using (
                    CryptoStream crypto =
                    new CryptoStream(
                        memoria,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write
                    )
                )
                {
                    crypto.Write(
                        dados,
                        0,
                        dados.Length
                    );

                    crypto.FlushFinalBlock();
                }

                return System.Convert
                    .ToBase64String(
                        memoria.ToArray()
                    );
            }
        }
    }

    // ==========================================
    // DESCRIPTOGRAFAR
    // ==========================================

    private string Descriptografar(
        string texto
    )
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key =
                GerarChave();

            aes.IV =
                new byte[16];

            byte[] dados =
                System.Convert
                    .FromBase64String(
                        texto
                    );

            using (
                MemoryStream memoria =
                new MemoryStream(dados)
            )
            {
                using (
                    CryptoStream crypto =
                    new CryptoStream(
                        memoria,
                        aes.CreateDecryptor(),
                        CryptoStreamMode.Read
                    )
                )
                {
                    using (
                        StreamReader leitor =
                        new StreamReader(
                            crypto
                        )
                    )
                    {
                        return leitor.ReadToEnd();
                    }
                }
            }
        }
    }

    // ==========================================
    // GERAR CHAVE
    // ==========================================

    private byte[] GerarChave()
    {
        using (
            SHA256 sha =
            SHA256.Create()
        )
        {
            return sha.ComputeHash(
                Encoding.UTF8.GetBytes(
                    chave
                )
            );
        }
    }
}