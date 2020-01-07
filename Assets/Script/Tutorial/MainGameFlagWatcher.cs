﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2020/01/05 toyoda
/// Save.isTutorialがtrueの時のみ実行する。
/// Save.csのmaingameflagを監視して、チュートリアルのウィンドウを操作する。
/// </summary>
public class MainGameFlagWatcher : MonoBehaviour
{

    [SerializeField] private TutorialController controller;
    [SerializeField] private VideoDataProvider provider;
    [SerializeField] private Canvas tutorialCanvas;

    private void Start()
    {
        if ((Save.stageState == Save.StageState.STAGE1) && Save.isTutorial)
        {
            StartCoroutine(ObserveVideoFlagCroutine());
        }
        if ((Save.stageState == Save.StageState.SIMPLESTAGE1) && Save.isTutorial)
        {
            StartCoroutine(SimpleObserveVideoFlagCroutine());
        }
    }

    private IEnumerator ObserveVideoFlagCroutine()
    {
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.STARTWAIT);
        StartCoroutine(DelayClass.DelayCoroutin(20, () => controller.PlayVideoClip(provider.throwTutorialPlayer)));

        yield return new WaitUntil(() => (provider.throwTutorialPlayer.isLooping == true) && (tutorialCanvas.enabled == false));
        StartCoroutine(DelayClass.DelayCoroutin(20, () => controller.PlayVideoClip(provider.windTutorialPlayer)));

        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.FALLING);
        StartCoroutine(DelayClass.DelayCoroutin(30, () => controller.PlayVideoClip(provider.ukemiTutorialPlayer)));
    }

    ///シンプルモード用 長尾 01/07
    private IEnumerator SimpleObserveVideoFlagCroutine()
    {
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.FALLING);
        StartCoroutine(DelayClass.DelayCoroutin(30, () => controller.PlayVideoClip(provider.ukemiTutorialPlayer)));
    }
}
