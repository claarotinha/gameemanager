using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CooldownBar : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player1";

    private Slider slider;
    private BallAttack ballAttack;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;

        FindPlayer();
    }

    private void Update()
    {
        if (ballAttack == null)
        {
            FindPlayer();
            return;
        }

        slider.value = ballAttack.CooldownPercent;
    }

    private void FindPlayer()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag(playerTag);

        if (player == null)
        {
            Debug.LogWarning(
                "Nenhum objeto encontrado com a tag: "
                + playerTag
            );

            return;
        }

        ballAttack =
            player.GetComponent<BallAttack>();

        if (ballAttack == null)
        {
            Debug.LogWarning(
                "O objeto " + player.name +
                " não possui BallAttack."
            );
        }
    }
}