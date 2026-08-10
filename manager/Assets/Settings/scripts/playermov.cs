
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    private BolinhaController bolinha;

    [Header("Movimentação")]
    [SerializeField] private float forceMultiplier = 10f;
    [SerializeField] private float maxMovementSpeed = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bolinha = GetComponent<BolinhaController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (bolinha == null)
        {
            Debug.LogError(
                "BolinhaController não encontrado!"
            );

            return;
        }

        Vector3 movement = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );

        // Aplica força para a bolinha se movimentar
        if (movement.sqrMagnitude > 0.01f)
        {
            rb.AddForce(
                movement.normalized *
                bolinha.GetSpeed() *
                forceMultiplier,
                ForceMode.Force
            );
        }

        // Limita somente a velocidade horizontal
        Vector3 horizontalVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        if (horizontalVelocity.magnitude > maxMovementSpeed)
        {
            horizontalVelocity =
                horizontalVelocity.normalized *
                maxMovementSpeed;

            rb.linearVelocity = new Vector3(
                horizontalVelocity.x,
                rb.linearVelocity.y,
                horizontalVelocity.z
            );
        }
    }
}
