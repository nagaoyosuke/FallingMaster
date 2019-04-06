using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 受け身開始時の演出(04/04 長尾)
/// </summary>
public class UkemiStartEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text text;

    private bool isEne;

    void Start()
    {
        text.enabled = false;
    }

    void Update()
    {
        if (!isEne)
        {
            if (Save.maingameFlag == Save.MainGameFlag.UKEMI)
            {
                Effect();
                isEne = true;
            }
        }
    }

    /// <summary>
    /// ここに共通の受け身時の演出かく
    /// </summary>
    void Effect()
    {
        text.enabled = true;
    }

    /// <summary>
    /// 成功時の演出書く
    /// </summary>
    void SuccessEffect()
    {

    }

    /// <summary>
    /// 失敗時の演出書く
    /// </summary>
    void FailureEffect()
    {

    }

    /// <summary>
    /// 受け身入力せず着地した時の演出書く
    /// </summary>
    void FailureNoUkemiEffect()
    {

    }
}
