using System;

public static class PlayerObserverManager
{
    public static Action<int, int> OnCoinChanged;


    public static void NotifyCoinChanged(int playerID, int amount)
    {
        OnCoinChanged?.Invoke(playerID, amount);
    }
}