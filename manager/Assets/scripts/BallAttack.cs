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

    private float cooldownTimer;

    private BolinhaController bolinha;


    // Usado pela barra de cooldown
    public float Cooldown => cooldown;

    public float CooldownPercent
    {
        get
        {
            if (cooldown <= 0)
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
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer < 0)
                cooldownTimer = 0;
        }
    }


    // Chamado pelo Input System
    public void OnPush(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;


        if (cooldownTimer > 0)
            return;


        PushEnemy();

        cooldownTimer = cooldown;
    }


    private void PushEnemy()
    {
        if(enemy == null || enemyRb == null)
        {
            Debug.LogWarning(
                "Inimigo não configurado no BallAttack"
            );

            return;
        }


        float distance =
            Vector3.Distance(
                transform.position,
                enemy.position
            );


        // Muito longe não empurra
        if(distance > maxDistance)
            return;


        // Direção do ataque
        Vector3 direction =
            (enemy.position - transform.position)
            .normalized;


        // Quanto mais perto, maior a força
        float distanceMultiplier =
            1f - (distance / maxDistance);


        float force =
            bolinha.GetPushForce()
            *
            distanceMultiplier;


        enemyRb.AddForce(
            direction * force,
            ForceMode.Impulse
        );
    }
}