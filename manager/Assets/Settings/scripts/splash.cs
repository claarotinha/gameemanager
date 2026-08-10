using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SplashRoutine());
    }

    IEnumerator SplashRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}