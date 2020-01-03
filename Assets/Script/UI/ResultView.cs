using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private ScreenFader fader;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Effect());
        
    }

    IEnumerator Effect()
    {
        string s = "";
        switch (Save.ukemiRank)
        {
            case Save.UkemiRank.PERFECT:
            case Save.UkemiRank.GOOD:
                s = "成功 次へ";
                break;

            case Save.UkemiRank.NOUKEMI:
            case Save.UkemiRank.NONE:
            case Save.UkemiRank.BAD:
                s = "失敗 次へ";
                break;
        }

        if (Save.stageState == Save.StageState.STAGE3)
            s = "結果発表へ";

        text.text = s;

        bool isEnd = false;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                255f / 255f,
                1.5f
            )
        ).AppendCallback(() => isEnd = true);

        yield return new WaitUntil(() => isEnd == true);

        fader.isFadeOut = true;
        Sound.PlaySe("sceneswitch02");
        yield return new WaitUntil(() => fader.isFadeOut == false);

        Save.FlagReSet();
        Sound.StopBgm();
        //Sound.PlayBgm("Result1");

        switch (Save.stageState)
        {
            case Save.StageState.STAGE1:
                Save.stageState = Save.StageState.STAGE2;
                break;
            case Save.StageState.STAGE2:
                Save.stageState = Save.StageState.STAGE3;
                break;
            case Save.StageState.STAGE3:
                MySceneManager.GoResult();
                yield break;
                break;
            case Save.StageState.SIMPLESTAGE1:
                Save.stageState = Save.StageState.SIMPLESTAGE2;
                break;
            case Save.StageState.SIMPLESTAGE2:
                Save.stageState = Save.StageState.SIMPLESTAGE3;
                break;
            case Save.StageState.SIMPLESTAGE3:
                MySceneManager.GoResult();
                yield break;
                break;
        }


        MySceneManager.GoMain();

        //MySceneManager.GoTitle();

    }
}
