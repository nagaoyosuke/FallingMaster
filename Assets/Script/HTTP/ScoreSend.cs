﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class ScoreSend : MonoBehaviour
{
    //接続するURL
    private string URL;

    private string SendJson;

    public UnityWebRequest request;
    public bool isComplating;

    void Start()
    {
        URL = Save.URL;
        request = new UnityWebRequest(URL, "POST");

        //Save.userName = "testunity";
        //Save.rank = Save.Rank.TENTH;
        //Save.stageState = Save.StageState.STAGE3;
        //Send();
    }

    public void Send()
    {
        JsonSet();
        StartCoroutine(OnSend());
    }

    //コルーチン
    IEnumerator OnSend()
    {
        isComplating = false;

        //POSTする情報
        var j = SendJson;

        request = new UnityWebRequest(URL, "POST");
        //jsonはstring型やからbytesに変換しないといけない　サーバで認識できないから
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(j);
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        //エラーが出ていないかチェック
        if (request.isNetworkError)
        {
            //通信失敗
            Debug.Log(request.error);
        }
        else
        {
            var data = request.downloadHandler.text;
            //通信成功
            Debug.Log(data);

            var s = JsonUtility.FromJson<JsonManager.Receive.ScoreJson>(data);
            if (s.mode == "endless")
                Save.EndlessMyRanking = s.ranking;
            else
                Save.DaniMyRanking = s.ranking;

            isComplating = true;
        }

    }

    void JsonSet()
    {
        int score = (int)Save.rank + 1;
        string name = Save.userName;
        //string mode = "dani";
        string mode = "endless";

        if (Save.stageState == Save.StageState.ENDLESS)
        {
            score = Save.UkemiScore;
            mode = "endless";
        }

        SendJson = new JsonManager.Send.ScoreJson(score, name, mode).ToJson();
        print(SendJson);
        print(score);

    }
}