using UnityEngine;

public class SplashController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Splash começou");

        Invoke("IrParaMenu", 2f);
    }

    void IrParaMenu()
    {
        Debug.Log("Indo para Menu");

        GameManager.Instance.ChangeState(GameManager.GameState.MenuPrincipal);
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}