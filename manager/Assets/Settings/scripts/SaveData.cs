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
    // ==========================================

    public bool checkpointAtivado;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;

    public int moedasCheckpoint;

    public List<string> moedasColetadasCheckpoint =
        new List<string>();

    // ==========================================
    // FASE CONCLUÍDA
    // ==========================================

    public bool faseConcluida;
}