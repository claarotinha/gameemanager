using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NovoCoinManager : MonoBehaviour
{
    public static NovoCoinManager Instance;

    [SerializeField] private TMP_Text contadorTexto;

    private int moedas = 0;
    private int totalMoedas = 0;

    private List<NovaMoeda> todasAsMoedas =
        new List<NovaMoeda>();

    private void Awake()
    {
        Instance = this;

        NovaMoeda[] moedasEncontradas =
            FindObjectsByType<NovaMoeda>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        todasAsMoedas.AddRange(
            moedasEncontradas
        );

        totalMoedas =
            todasAsMoedas.Count;
    }

    private void Start()
    {
        AtualizarContador();
    }

    public void AdicionarMoeda(
        NovaMoeda moeda)
    {
        moedas++;

        AtualizarContador();
    }

    public void DefinirMoedas(
        int quantidade)
    {
        moedas = quantidade;

        AtualizarContador();
    }

    public void RestaurarMoedasPorNome(
        List<string> moedasColetadas)
    {
        foreach (
            NovaMoeda moeda
            in todasAsMoedas)
        {
            if (moeda == null)
                continue;

            if (moedasColetadas.Contains(
                moeda.GetID()))
            {
                moeda.gameObject.SetActive(false);
            }
            else
            {
                moeda.RestaurarMoeda();
            }
        }
    }

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

    public int GetMoedas()
    {
        return moedas;
    }

    public int GetTotalMoedas()
    {
        return totalMoedas;
    }

    public List<string>
        GetMoedasColetadas()
    {
        List<string> resultado =
            new List<string>();

        foreach (
            NovaMoeda moeda
            in todasAsMoedas)
        {
            if (moeda != null &&
                moeda.EstaColetada())
            {
                resultado.Add(
                    moeda.GetID()
                );
            }
        }

        return resultado;
    }
}