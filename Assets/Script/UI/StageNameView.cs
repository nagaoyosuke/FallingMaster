using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageNameView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private float time = 2.0f;

    [SerializeField]
    [TextArea]
    private string Stage1;

    [SerializeField]
    [TextArea]
    private string Stage2;

    [SerializeField]
    [TextArea]
    private string Stage3;

    [SerializeField]
    [TextArea]
    private string Endless;

    private bool isView;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;

        switch (Save.stageState)
        {
            case Save.StageState.STAGE1:
                text.text = Stage1;
                break;
            case Save.StageState.STAGE2:
                text.text = Stage2;
                break;
            case Save.StageState.STAGE3:
                text.text = Stage3;
                break;
            case Save.StageState.ENDLESS:
                text.text = Endless;
                break;
        }
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
                time
            )
        );

        sequence.Append(
            DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                0f / 255f,
                time
            )
        ).AppendCallback(() =>
        {
            Save.maingameFlag = Save.MainGameFlag.STARTMOVE;
        });
    }


}
