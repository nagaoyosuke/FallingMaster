using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
/// <summary>
/// ゲームの進行を管理するクラス(03/23)
/// </summary>
public class MainSceneManager : MonoBehaviour
{

    /// <summary>
    /// ここにゲームで起こるイベントを順番にインスペクターから登録
    /// </summary>
    public Func<Action<bool>,IEnumerator> Event;

    private bool isEnd;

    // Start is called before the first frame update
    IEnumerator Start(){
        Event = test;
        Event += test1;
        StartCoroutine(Event(EndCheck));
        StartCoroutine(Event(EndCheck));
        yield return null;
    }

    // Update is called once per frame
    void Update(){
        print(isEnd);
    }

    public void EndCheck(bool isEnd){
        this.isEnd = isEnd;
    }

    public void testa(){
        StartCoroutine(test(EndCheck));
    }

    public IEnumerator test(Action<bool> isEnd){
        yield return new WaitForSeconds(1.0f);
        print("sdfghj");
        isEnd(true);

    }

    public IEnumerator test1(Action<bool> isEnd){
        yield return new WaitForSeconds(1.0f);
        print("e");
        isEnd(true);

    }
}
