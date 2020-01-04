using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    private ScreenFader fader;

    private bool isPush;

    public void GoMenu()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        Save.ReSet();
        Save.PointReset();
        StartCoroutine(_GoMenu());
    }

    public void GoMain()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        StartCoroutine(_GoMain());
    }

    public void GoEndless()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        Save.ReSet();
        Save.PointReset();
        StartCoroutine(_GoEndless());
    }

    public void GoModeSelect()
    {
        if (isPush)
            return;
        isPush = true;
        fader.isFadeOut = true;
        StartCoroutine(_GoModeSelecrt());
    }

    IEnumerator _GoMain()
    {
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoMain();
    }

    IEnumerator _GoMenu()
    {
        Sound.PlaySe("taiko01");

        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoTitle();
    }

    IEnumerator _GoEndless()
    {
        Sound.PlaySe("taiko01");
        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoEndless();
    }

    IEnumerator _GoModeSelecrt()
    {
        Sound.PlaySe("taiko01");

        yield return new WaitForSeconds(1.5f);
        MySceneManager.GoModeSelect();
    }
}
