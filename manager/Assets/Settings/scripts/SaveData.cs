using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    // ==========================================
    // FASE
    // ==========================================

    public string fase;

    // ==========================================
    // CHECKPOINT
    // Usado para RESPawn depois da morte
    // ==========================================

    public bool checkpointAtivado;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;

    public int moedasCheckpoint;

    public List<string> moedasColetadasCheckpoint =
        new List<string>();

    // ==========================================
    // SAVE MANUAL
    // Guarda o local EXATO onde o jogador salvou
    // ==========================================

    public bool possuiPosicaoSalva;

    public float posicaoSalvaX;
    public float posicaoSalvaY;
    public float posicaoSalvaZ;

    public int moedasSalvas;

    public List<string> moedasColetadasSalvas =
        new List<string>();

    // ==========================================
    // FASE CONCLUÍDA
    // ==========================================

    public bool faseConcluida;
}