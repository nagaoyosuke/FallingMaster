using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Titleの画面タップを管理するクラス(02/02豊田)
/// </summary>

public class TapEventManager : MonoBehaviour
{
    private bool canTap = true;

    [SerializeField] private Animator ani;
    [SerializeField] private GameObject creditGameObject;

    private void Start()
    {
        Sound.PlaySe("taiko02");
        Sound.PlayBgm("Result1");
        ani.enabled = false;
    }

    private IEnumerator WaitingFadeOut()
    {

        ani.enabled = true;
        yield return new WaitForSeconds(0.75f);
        GameObject.Find("Panel").GetComponent<ScreenFader>().isFadeOut = true;
        yield return new WaitForSeconds(1.5f);

        MySceneManager.GoModeSelect();
    }

    public void OnPushedStartButton() 
    {
        if (canTap)
        {
            Sound.PlaySe("osu01");
            canTap = false;
            StartCoroutine(WaitingFadeOut());
        }
    }

    public void OnPushedCreditButton()
    {
        if (canTap)
        {
            canTap = false;
            Sound.PlaySe("taiko01");
            creditGameObject.SetActive(true);
        } else {
            canTap = true;
            creditGameObject.SetActive(false);
        }
    }
}
