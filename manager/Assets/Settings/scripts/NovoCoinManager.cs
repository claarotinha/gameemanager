using UnityEngine;
using TMPro;

public class NovoCoinManager : MonoBehaviour
{
    public static NovoCoinManager Instance;

    [SerializeField] private TMP_Text contadorTexto;

    private int moedas = 0;
    private int totalMoedas = 0;

    private void Awake()
    {
        Instance = this;

        totalMoedas = FindObjectsByType<NovaMoeda>(FindObjectsSortMode.None).Length;
    }

    private void Start()
    {
        AtualizarContador();
    }

    public void AdicionarMoeda()
    {
        moedas++;
        AtualizarContador();
    }

    private void AtualizarContador()
    {
        contadorTexto.text = "Moedas: " + moedas + "/" + totalMoedas;
    }

    public int GetMoedas()
    {
        return moedas;
    }

    public int GetTotalMoedas()
    {
        return totalMoedas;
    }
}