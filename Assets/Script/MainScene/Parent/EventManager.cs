using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;

/// <summary>
/// シーンによって処理を変えたいから親クラスにした(06/10長尾)
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// ここにゲームで起こる自動イベントをdotween記述で順番にインスペクターから登録
    /// 入力待ちとかはできへんからムービーイベントやと思って
    /// CameraMove.csのStartMove参考(03/24長尾)
    /// </summary>
    public TweenEvent StarttweenEvent;

    /// <summary>
    /// ゲーム全体の流れをまとめるシーケンス
    /// </summary>
    [SerializeField]
    public Sequence Startsequence;
}


[Serializable]
public class TweenEvent : UnityEvent<Sequence> { }