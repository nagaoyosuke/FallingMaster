using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;
/// <summary>
/// ゲームの進行を管理するクラス(03/23長尾)
/// </summary>
public class MainSceneManager : MonoBehaviour
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
    private Sequence Startsequence;

    void Awake() {
        Startsequence = DOTween.Sequence();
    }

    void Start(){
        StarttweenEvent.Invoke(Startsequence);
    }
}

[Serializable]
public class TweenEvent : UnityEvent<Sequence> { }