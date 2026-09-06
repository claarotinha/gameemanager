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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += AoCarregarCena;

            // Verifica se existe um save no slot 0
            Debug.Log(
                "Existe save no slot 0? " + ExisteSave(0)
            );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
        }
    }

    // ==========================================
    // SALVAR
    // ==========================================

    public void Salvar(SaveData dados, int slot)
    {
        string caminho = ObterCaminho(slot);

        string json = JsonUtility.ToJson(dados);

        string textoCriptografado =
            Criptografar(json);

        File.WriteAllText(
            caminho,
            textoCriptografado
        );

        Debug.Log(
            "Jogo salvo no slot " + slot
        );

        Debug.Log(
            "Arquivo salvo em: " + caminho
        );
    }

    // ==========================================
    // SAVE MANUAL
    // ==========================================

    public void SalvarManual(int slot)
    {
        // Só permite os slots manuais 1, 2 e 3
        if (slot < 1 || slot > 3)
        {
            Debug.LogError(
                "Slot manual inválido: " + slot
            );

            return;
        }

        if (NovoCheckpointManager.Instance == null)
        {
            Debug.LogError(
                "NovoCheckpointManager não encontrado!"
            );

            return;
        }

        // Pega o estado salvo no checkpoint.
        // Isso significa que moedas coletadas DEPOIS
        // do checkpoint não entram no save.
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
            "Save manual realizado no slot " +
            slot +
            " e copiado para o slot 0!"
        );
    }

    // ==========================================
    // VERIFICAR SAVE
    // ==========================================

    public bool ExisteSave(int slot)
    {
        return File.Exists(
            ObterCaminho(slot)
        );
    }

    // ==========================================
    // CARREGAR
    // ==========================================

    public void Carregar(int slot)
    {
        string caminho = ObterCaminho(slot);

        if (!File.Exists(caminho))
        {
            Debug.Log(
                "Não existe save no slot " + slot
            );

            return;
        }

        string textoCriptografado =
            File.ReadAllText(caminho);

        string json =
            Descriptografar(textoCriptografado);

        dadosCarregados =
            JsonUtility.FromJson<SaveData>(json);

        Debug.Log(
            "Save carregado do slot " + slot
        );

        Debug.Log(
            "Fase salva: " + dadosCarregados.fase
        );

        // Se carregou um slot manual,
        // ele também passa a ser o autosave.
        if (slot != 0)
        {
            Salvar(
                dadosCarregados,
                0
            );

            Debug.Log(
                "Save do slot " +
                slot +
                " copiado para o slot 0."
            );
        }

        SceneManager.LoadScene(
            dadosCarregados.fase
        );
    }

    // ==========================================
    // APLICA O SAVE DEPOIS QUE A CENA CARREGA
    // ==========================================

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo)
    {
        if (dadosCarregados == null)
            return;

        if (cena.name != dadosCarregados.fase)
            return;

        AplicarDados();

        dadosCarregados = null;
    }

    private void AplicarDados()
    {
        if (NovoCoinManager.Instance != null)
        {
            NovoCoinManager.Instance.DefinirMoedas(
                dadosCarregados.moedasCheckpoint
            );

            NovoCoinManager.Instance
                .RestaurarMoedasPorNome(
                    dadosCarregados
                    .moedasColetadasCheckpoint
                );
        }

        if (NovoCheckpointManager.Instance != null)
        {
            Vector3 posicao = new Vector3(
                dadosCarregados.checkpointX,
                dadosCarregados.checkpointY,
                dadosCarregados.checkpointZ
            );

            NovoCheckpointManager.Instance
                .CarregarCheckpoint(
                    dadosCarregados.checkpointAtivado,
                    posicao,
                    dadosCarregados.moedasCheckpoint,
                    dadosCarregados
                    .moedasColetadasCheckpoint
                );
        }

        Debug.Log(
            "Save aplicado na cena!"
        );
    }

    // ==========================================
    // CAMINHO DOS SLOTS
    // ==========================================

    private string ObterCaminho(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save_slot_" + slot + ".dat"
        );
    }

    // ==========================================
    // CRIPTOGRAFIA
    // ==========================================

    private string Criptografar(string texto)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GerarChave();
            aes.IV = new byte[16];

            byte[] dados =
                Encoding.UTF8.GetBytes(texto);

            using (MemoryStream memoria =
                   new MemoryStream())
            {
                using (CryptoStream crypto =
                       new CryptoStream(
                           memoria,
                           aes.CreateEncryptor(),
                           CryptoStreamMode.Write))
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

    private string Descriptografar(string texto)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GerarChave();
            aes.IV = new byte[16];

            byte[] dados =
                System.Convert
                    .FromBase64String(texto);

            using (MemoryStream memoria =
                   new MemoryStream(dados))
            {
                using (CryptoStream crypto =
                       new CryptoStream(
                           memoria,
                           aes.CreateDecryptor(),
                           CryptoStreamMode.Read))
                {
                    using (StreamReader leitor =
                           new StreamReader(crypto))
                    {
                        return leitor.ReadToEnd();
                    }
                }
            }
        }
    }

    private byte[] GerarChave()
    {
        using (SHA256 sha =
               SHA256.Create())
        {
            return sha.ComputeHash(
                Encoding.UTF8.GetBytes(chave)
            );
        }
    }
}