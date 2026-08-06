using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector2 moveInput;

    private BolinhaController bolinha;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bolinha = GetComponent<BolinhaController>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        Debug.Log("Input recebido: " + moveInput);
    }


    private void FixedUpdate()
    {
        if (bolinha == null)
        {
            Debug.LogError("BolinhaController não encontrado!");
            return;
        }


        Vector3 movement = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );


        rb.MovePosition(
            rb.position +
            movement *
            bolinha.GetSpeed() *
            Time.fixedDeltaTime
        );
    }
}