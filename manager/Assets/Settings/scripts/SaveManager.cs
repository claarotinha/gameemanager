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
            "Posição salva: " +
            dados.possuiPosicaoSalva
        );

        Debug.Log(
            "Checkpoint: " +
            dados.checkpointAtivado
        );

        Debug.Log(
            "Moedas salvas: " +
            dados.moedasSalvas
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
            NovoCheckpointManager.Instance
            == null
        )
        {
            Debug.LogError(
                "NovoCheckpointManager não encontrado!"
            );

            return;
        }

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

        // Pega a posição EXATA do Player
        Vector3 posicaoJogador =
            player.transform.position;

        // Cria o save manual
        SaveData dados =
            NovoCheckpointManager.Instance
            .CriarDadosSaveManual(
                posicaoJogador
            );

        // Salva no slot escolhido
        Salvar(
            dados,
            slot
        );

        // Também copia para Slot 0
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
            "Posição: " +
            posicaoJogador
        );

        Debug.Log(
            "Moedas: " +
            dados.moedasSalvas
        );

        Debug.Log(
            "Checkpoint: " +
            dados.checkpointAtivado
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

        // Se estava pausado
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
            "Posição salva: " +
            dadosCarregados.possuiPosicaoSalva
        );

        Debug.Log(
            "Moedas salvas: " +
            dadosCarregados.moedasSalvas
        );

        Debug.Log(
            "================================"
        );

        // Copia save manual para Slot 0
        if (slot != 0)
        {
            Salvar(
                dadosCarregados,
                0
            );
        }

        // Força o carregamento da fase
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
        if (
            dadosCarregados == null
        )
            return;

        if (
            cena.name !=
            dadosCarregados.fase
        )
            return;

        Debug.Log(
            "Cena carregada. Aplicando save..."
        );

        AplicarDados();

        dadosCarregados = null;
    }

    // ==========================================
    // APLICAR SAVE
    // ==========================================

    private void AplicarDados()
    {
        if (
            dadosCarregados == null
        )
            return;

        // ======================================
        // MOEDAS DO SAVE MANUAL
        // ======================================

        if (
            NovoCoinManager.Instance != null
        )
        {
            NovoCoinManager.Instance
                .DefinirMoedas(
                    dadosCarregados
                    .moedasSalvas
                );

            NovoCoinManager.Instance
                .RestaurarMoedasPorNome(
                    dadosCarregados
                    .moedasColetadasSalvas
                );
        }

        // ======================================
        // RESTAURAR CHECKPOINT
        // ======================================

        if (
            NovoCheckpointManager.Instance
            != null
        )
        {
            Vector3 checkpoint =
                new Vector3(
                    dadosCarregados.checkpointX,
                    dadosCarregados.checkpointY,
                    dadosCarregados.checkpointZ
                );

            NovoCheckpointManager.Instance
                .CarregarCheckpoint(
                    dadosCarregados
                    .checkpointAtivado,

                    checkpoint,

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

        Vector3 posicaoPlayer;

        // ======================================
        // 1º: POSIÇÃO DO SAVE MANUAL
        // ======================================

        if (
            dadosCarregados
            .possuiPosicaoSalva
        )
        {
            posicaoPlayer =
                new Vector3(
                    dadosCarregados
                    .posicaoSalvaX,

                    dadosCarregados
                    .posicaoSalvaY,

                    dadosCarregados
                    .posicaoSalvaZ
                );

            Debug.Log(
                "Usando posição EXATA do save manual."
            );
        }

        // ======================================
        // 2º: CHECKPOINT
        // ======================================

        else if (
            dadosCarregados
            .checkpointAtivado
        )
        {
            posicaoPlayer =
                new Vector3(
                    dadosCarregados
                    .checkpointX,

                    dadosCarregados
                    .checkpointY,

                    dadosCarregados
                    .checkpointZ
                );

            Debug.Log(
                "Usando posição do checkpoint."
            );
        }

        // ======================================
        // 3º: PONTO INICIAL
        // ======================================

        else
        {
            Debug.Log(
                "Sem posição salva e sem checkpoint."
            );

            Debug.Log(
                "Player começará no PontoInicial."
            );

            return;
        }

        // ======================================
        // COLOCAR PLAYER
        // ======================================

        Rigidbody rb =
            player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                Vector3.zero;

            rb.angularVelocity =
                Vector3.zero;

            rb.position =
                posicaoPlayer;
        }
        else
        {
            player.transform.position =
                posicaoPlayer;
        }

        Debug.Log(
            "Player colocado em: " +
            posicaoPlayer
        );

        Debug.Log(
            "Save aplicado com sucesso!"
        );
    }

    // ==========================================
    // APAGAR SAVES
    // ==========================================

    public void ApagarTodosOsSaves()
    {
        for (
            int slot = 0;
            slot <= 3;
            slot++
        )
        {
            string caminho =
                ObterCaminho(slot);

            if (File.Exists(caminho))
            {
                File.Delete(
                    caminho
                );

                Debug.Log(
                    "Save do Slot " +
                    slot +
                    " apagado."
                );
            }
        }

        Debug.Log(
            "Todos os saves foram apagados!"
        );
    }

    // ==========================================
    // CAMINHO
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
                new MemoryStream(
                    dados
                )
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
    // CHAVE
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