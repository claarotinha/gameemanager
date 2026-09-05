using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 7f;

    private InputAction moveAction;
    private InputAction jumpAction;

    private Rigidbody rb;
    private Vector2 movimentoInput;
    private bool noChao;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = playerControls.FindAction("Player1/Move");
        jumpAction = playerControls.FindAction("Player1/Push");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();

        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnJump;

        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        movimentoInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movimento = new Vector3(
            movimentoInput.x,
            0f,
            movimentoInput.y
        );

        movimento.Normalize();

        Vector3 velocidade = movimento * moveSpeed;

        rb.linearVelocity = new Vector3(
            velocidade.x,
            rb.linearVelocity.y,
            velocidade.z
        );
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!noChao)
            return;

        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        noChao = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = false;
        }
    }
}