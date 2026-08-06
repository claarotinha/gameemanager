using System;

public static class PlayerObserverManager
{
    // Evento chamado sempre que uma moeda é coletada
    public static Action OnCoinCollected;

    // Evento para atualizar a interface de moedas
    public static Action<int, int> OnCoinChanged;

    public static void NotifyCoinCollected()
    {
        OnCoinCollected?.Invoke();
    }

    public static void NotifyCoinChanged(int playerID, int amount)
    {
        OnCoinChanged?.Invoke(playerID, amount);
    }
}