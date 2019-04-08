using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 受け身関連の演出(04/04 長尾)
/// </summary>
public class UkemiEffecManager : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// 受け身入力が開始された時に表示されるテキスト
    /// </summary>
    [SerializeField]
    private Text UkemiStartText;

    private bool isEnd;

    void Start()
    {
        UkemiStartText.enabled = false;
    }

    void Update()
    {
        if (!isEnd)
        {
            if (Save.maingameFlag == Save.MainGameFlag.UKEMI)
            {
                Effect();
                isEnd = true;
            }
        }
    }

    /// <summary>
    /// ここに共通の受け身時の演出かく
    /// </summary>
    void Effect()
    {
        UkemiStartText.enabled = true;
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
