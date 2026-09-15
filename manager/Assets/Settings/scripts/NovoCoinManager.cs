using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NovoCoinManager : MonoBehaviour
{
    public static NovoCoinManager Instance;

    [SerializeField]
    private TMP_Text contadorTexto;

    private int moedas = 0;

    private int totalMoedas = 0;

    private List<NovaMoeda> todasAsMoedas =
        new List<NovaMoeda>();

    // ==========================================
    // AWAKE
    // ==========================================

    private void Awake()
    {
        Instance = this;

        NovaMoeda[] moedasEncontradas =
            FindObjectsByType<NovaMoeda>(
                FindObjectsInactive.Include
            );

        todasAsMoedas.AddRange(
            moedasEncontradas
        );

        totalMoedas =
            todasAsMoedas.Count;
    }

    // ==========================================
    // START
    // ==========================================

    private void Start()
    {
        AtualizarContador();
    }

    // ==========================================
    // ADICIONAR MOEDA
    // ==========================================

    public void AdicionarMoeda(
        NovaMoeda moeda
    )
    {
        moedas++;

        AtualizarContador();
    }

    // ==========================================
    // DEFINIR MOEDAS
    // ==========================================

    public void DefinirMoedas(
        int quantidade
    )
    {
        moedas =
            quantidade;

        AtualizarContador();
    }

    // ==========================================
    // RESTAURAR MOEDAS POR NOME
    // ==========================================

    public void RestaurarMoedasPorNome(
        List<string> moedasColetadas
    )
    {
        if (moedasColetadas == null)
        {
            moedasColetadas =
                new List<string>();
        }

        foreach (
            NovaMoeda moeda
            in todasAsMoedas
        )
        {
            if (moeda == null)
                continue;

            if (
                moedasColetadas.Contains(
                    moeda.GetID()
                )
            )
            {
                moeda.gameObject
                    .SetActive(false);
            }
            else
            {
                moeda.RestaurarMoeda();
            }
        }
    }

    // ==========================================
    // RESTAURAR TODAS AS MOEDAS
    // ==========================================

    public void RestaurarTodasAsMoedas()
    {
        foreach (
            NovaMoeda moeda
            in todasAsMoedas
        )
        {
            if (moeda == null)
                continue;

            moeda.RestaurarMoeda();
        }

        AtualizarContador();
    }

    // ==========================================
    // ATUALIZAR CONTADOR
    // ==========================================

    private void AtualizarContador()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text =
                "Moedas: " +
                moedas +
                "/" +
                totalMoedas;
        }
    }

    // ==========================================
    // GET MOEDAS
    // ==========================================

    public int GetMoedas()
    {
        return moedas;
    }

    // ==========================================
    // GET TOTAL
    // ==========================================

    public int GetTotalMoedas()
    {
        return totalMoedas;
    }

    // ==========================================
    // GET MOEDAS COLETADAS
    // ==========================================

    public List<string>
        GetMoedasColetadas()
    {
        List<string> resultado =
            new List<string>();

        foreach (
            NovaMoeda moeda
            in todasAsMoedas
        )
        {
            if (
                moeda != null &&
                moeda.EstaColetada()
            )
            {
                resultado.Add(
                    moeda.GetID()
                );
            }
        }

        return resultado;
    }
}