using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private bool noChao;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            noChao = false;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 velocidade = movimento * moveSpeed;

        rb.linearVelocity = new Vector3(
            velocidade.x,
            rb.linearVelocity.y,
            velocidade.z
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
        }
    }
}