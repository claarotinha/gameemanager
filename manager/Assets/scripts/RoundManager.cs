using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Debug.Log("Jogador 1 caiu!");
        }

        if (other.CompareTag("Player2"))
        {
            Debug.Log("Jogador 2 caiu!");
        }
    }
}