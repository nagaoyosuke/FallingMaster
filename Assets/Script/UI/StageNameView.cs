using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageNameView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private bool isView;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isView)
        {
            if (Save.maingameFlag == Save.MainGameFlag.STARTCAMERA)
            {
                isView = true;
                text.enabled = true;
                Effect();
            }
        }
    }

    void Effect()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                255f / 255f,
                2f
            )
        );

        sequence.Append(
            DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                0f / 255f,
                2f
            )
        ).AppendCallback(() =>
        {
            Save.maingameFlag = Save.MainGameFlag.STARTMOVE;
        });
    }


}
