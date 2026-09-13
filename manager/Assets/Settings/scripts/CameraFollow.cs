using UnityEngine;

public class NewCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Posição da câmera")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -8f);

    [Header("Velocidade")]
    [SerializeField] private float velocidade = 5f;

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 novaPosicao = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            novaPosicao,
            velocidade * Time.deltaTime
        );
    }
}