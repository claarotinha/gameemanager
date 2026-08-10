using UnityEngine;
using UnityEngine.InputSystem;

public class BallAttack : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Rigidbody enemyRb;
    [SerializeField] private Transform enemy;

    [Header("Distância do ataque")]
    [SerializeField] private float maxDistance = 8f;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 3f;

    [Header("Controle do empurrão")]
    [SerializeField] private float maxKnockbackSpeed = 12f;

    private float cooldownTimer;

    private BolinhaController bolinha;

    public float Cooldown => cooldown;

    public float CooldownPercent
    {
        get
        {
            if (cooldown <= 0f)
                return 1f;

            return Mathf.Clamp01(
                1f - (cooldownTimer / cooldown)
            );
        }
    }

    private void Awake()
    {
        bolinha = GetComponent<BolinhaController>();
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
                cooldownTimer = 0f;
        }
    }

    public void OnPush(InputAction.CallbackContext context)
    {
        // Só executa no momento em que aperta
        if (!context.started)
            return;

        // Ainda está no cooldown
        if (cooldownTimer > 0f)
            return;

        if (enemy == null || enemyRb == null)
        {
            Debug.LogWarning(
                "Enemy ou Enemy Rigidbody não configurado."
            );

            return;
        }

        if (bolinha == null)
        {
            Debug.LogWarning(
                "BolinhaController não encontrado."
            );

            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            enemy.position
        );

        if (distance > maxDistance)
        {
            Debug.Log("Inimigo está fora do alcance.");
            return;
        }

        // Começa o cooldown
        cooldownTimer = cooldown;

        PushEnemy(distance);
    }

    private void PushEnemy(float distance)
    {
        Vector3 direction =
            (enemy.position - transform.position).normalized;

        float distanceMultiplier =
            1f - (distance / maxDistance);

        float force =
            bolinha.GetPushForce() *
            distanceMultiplier;

        // Remove velocidade horizontal anterior
        Vector3 velocity = enemyRb.linearVelocity;

        enemyRb.linearVelocity = new Vector3(
            0f,
            velocity.y,
            0f
        );

        // Aplica UM impulso
        enemyRb.AddForce(
            direction * force,
            ForceMode.Impulse
        );

        // Limita a velocidade horizontal
        Vector3 horizontalVelocity =
            new Vector3(
                enemyRb.linearVelocity.x,
                0f,
                enemyRb.linearVelocity.z
            );

        if (horizontalVelocity.magnitude > maxKnockbackSpeed)
        {
            horizontalVelocity =
                horizontalVelocity.normalized *
                maxKnockbackSpeed;

            enemyRb.linearVelocity = new Vector3(
                horizontalVelocity.x,
                enemyRb.linearVelocity.y,
                horizontalVelocity.z
            );
        }

        Debug.Log(
            "EMPURRÃO APLICADO | Força: " + force
        );
    }
}