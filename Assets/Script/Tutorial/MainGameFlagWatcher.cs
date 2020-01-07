using System.Collections;
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
    }

    private IEnumerator ObserveVideoFlagCroutine()
    {
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.STARTWAIT);
        StartCoroutine(DelayClass.DelayCoroutin(20, () => controller.PlayVideoClip(provider.throwTutorialPlayer)));

        yield return new WaitUntil(() => (provider.throwTutorialPlayer.isLooping == true) && (tutorialCanvas.enabled == false));
        StartCoroutine(DelayClass.DelayCoroutin(25, () => controller.PlayVideoClip(provider.windTutorialPlayer)));

        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.FALLING);
        StartCoroutine(DelayClass.DelayCoroutin(20, () => controller.PlayVideoClip(provider.ukemiTutorialPlayer)));
    }
}
