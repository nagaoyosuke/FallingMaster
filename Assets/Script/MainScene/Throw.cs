﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    private bool isStart;

    // Update is called once per frame
    void Update(){
        if (!isStart && Save.maingameFlag == Save.MainGameFlag.THROW)
            StartCoroutine(ThrowAnime());
    }

    IEnumerator ThrowAnime(){
        //ここに投げるアニメーションができたら実装する

        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -200.0f));

        yield return null;
        Save.maingameFlag = Save.MainGameFlag.FALLING;
    }
}