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

        if (playerControls == null)
        {
            Debug.LogError("O Player Controls não foi atribuído!");
            return;
        }

        InputActionMap player1Map = playerControls.FindActionMap("Player1");

        if (player1Map == null)
        {
            Debug.LogError("O Action Map Player1 não foi encontrado!");
            return;
        }

        moveAction = player1Map.FindAction("Move");
        jumpAction = player1Map.FindAction("Push");

        if (moveAction == null)
        {
            Debug.LogError("A ação Move não foi encontrada dentro de Player1!");
        }

        if (jumpAction == null)
        {
            Debug.LogError("A ação Push não foi encontrada dentro de Player1!");
        }
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.Enable();
        }

        if (jumpAction != null)
        {
            jumpAction.Enable();
            jumpAction.performed += OnJump;
        }
    }

    private void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.performed -= OnJump;
            jumpAction.Disable();
        }

        if (moveAction != null)
        {
            moveAction.Disable();
        }
    }

    private void Update()
    {
        if (moveAction != null)
        {
            movimentoInput = moveAction.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

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