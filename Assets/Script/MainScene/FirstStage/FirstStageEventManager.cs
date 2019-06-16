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
    void Awake() {
        Startsequence = DOTween.Sequence();
    }

    void Start(){
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => Save.maingameFlag == Save.MainGameFlag.STARTMOVE);
        Startsequence = DOTween.Sequence();
        StarttweenEvent.Invoke(Startsequence);
    }
}