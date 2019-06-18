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
                s = "合格";
                break;

            case Save.UkemiRank.NOUKEMI:
            case Save.UkemiRank.NONE:
            case Save.UkemiRank.BAD:
                s = "不合格";
                break;
        }

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

        yield return new WaitUntil(() => fader.isFadeOut == false);

        Save.ReSet();
        Sound.StopBgm();
        Sound.PlayBgm("Result1");

        MySceneManager.GoMenu();
    }
}
