using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private Transform spawnArea;

    [SerializeField] private float spawnTime = 5f;

    [SerializeField] private int maxCoins = 5;


    private int currentCoins;


    private void Start()
    {
        InvokeRepeating(
            nameof(SpawnCoin),
            1f,
            spawnTime
        );
    }


    private void SpawnCoin()
    {
        if(currentCoins >= maxCoins)
            return;


        Vector3 randomPosition = new Vector3(
            Random.Range(
                -spawnArea.localScale.x / 2,
                spawnArea.localScale.x / 2
            ),

            1f,

            Random.Range(
                -spawnArea.localScale.z / 2,
                spawnArea.localScale.z / 2
            )
        );


        Vector3 worldPosition =
            spawnArea.position + randomPosition;


        Instantiate(
            coinPrefab,
            worldPosition,
            Quaternion.identity
        );


        currentCoins++;
    }


    public void RemoveCoin()
    {
        currentCoins--;
    }
}