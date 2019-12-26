using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ModeSelectでシーン飛ばす
/// </summary>
public class ModeSelectManager : MonoBehaviour
{

    [SerializeField] private ScreenFader screenFader;

    //シンプルモード
    public void OnPushedSimpleButton()
    {
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoSimple));
    }

    //無間地獄
    public void OnPushedEndlessButton()
    {
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoEndless));
    }


    //段位認定モード
    public void OnPushedCertificationRankButton()
    {
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoMain));
    }

    private IEnumerator ChangeSceneCroutine(Action sceneFunc)
    {
        Sound.PlaySe("osu01");
        yield return new WaitForSeconds(0.75f);
        screenFader.isFadeOut = true;
        yield return new WaitForSeconds(1.5f);
        sceneFunc();
    }
}
