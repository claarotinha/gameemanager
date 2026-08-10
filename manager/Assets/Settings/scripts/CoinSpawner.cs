
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Collider spawnArea;

    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private int maxCoins = 5;

    private int currentCoins;

    private void Start()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("CoinSpawner: Coin Prefab não foi configurado!");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("CoinSpawner: Spawn Area não foi configurada!");
            return;
        }

        InvokeRepeating(nameof(SpawnCoin), 1f, spawnTime);
    }

    private void SpawnCoin()
    {
        if (currentCoins >= maxCoins)
            return;

        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        float y = bounds.max.y + 0.1f;

        Vector3 spawnPosition = new Vector3(x, y, z);

        GameObject coin = Instantiate(
            coinPrefab,
            spawnPosition,
            Quaternion.identity
        );

        currentCoins++;

        Coin coinScript = coin.GetComponent<Coin>();

        if (coinScript != null)
        {
            coinScript.SetSpawner(this);
        }
    }

    public void RemoveCoin()
    {
        if (currentCoins > 0)
        {
            currentCoins--;
        }
    }
}