using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;

/// <summary>
/// ゲームの進行を管理するクラス(03/23長尾)
/// </summary>
public class FirstStageEventManager : EventManager
{
    private bool isStart;

    void OnEnable()
    {
        if(isStart)
            StarttweenEvent.Invoke(Startsequence);
        isStart = true;
    }

    void Awake() {
        Startsequence = DOTween.Sequence();
    }

    void Start(){
        StarttweenEvent.Invoke(Startsequence);
    }
}