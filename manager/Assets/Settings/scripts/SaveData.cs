using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string fase;

    public bool checkpointAtivado;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;

    public int moedasCheckpoint;

    public List<string> moedasColetadasCheckpoint =
        new List<string>();

    public bool faseConcluida;
}