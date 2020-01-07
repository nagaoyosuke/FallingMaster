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
        Save.ReSet();
        Save.PointReset();
        Save.stageState = Save.StageState.SIMPLESTAGE1;
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoMain));
    }

    //無間地獄
    public void OnPushedEndlessButton()
    {
        Save.ReSet();
        Save.PointReset();
        Save.stageState = Save.StageState.ENDLESS;
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoEndless));
    }


    //段位認定モード
    public void OnPushedCertificationRankButton()
    {
        Save.ReSet();
        Save.PointReset();
        Save.stageState = Save.StageState.STAGE1;
        StartCoroutine(ChangeSceneCroutine(MySceneManager.GoMain));
    }

    private IEnumerator ChangeSceneCroutine(Action sceneFunc)
    {
        Sound.PlaySe("osu01");
        yield return new WaitForSeconds(0.75f);
        screenFader.isFadeOut = true;
        yield return new WaitForSeconds(1.5f);
        Sound.StopBgm();
        sceneFunc();
    }
}
