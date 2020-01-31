using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class WorldRankingGet : MonoBehaviour
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
        request = new UnityWebRequest(URL, "POST");

        isComplating = false;
        //POSTする情報
        var j = SendJson;


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

            var s = JsonUtility.FromJson<JsonManager.Receive.ScoreRankingJson>(data);
            Array.Sort(s.endless,(a,b) => b.score - a.score);
            Array.Sort(s.dani, (a, b) => b.score - a.score);

            Save.CountryRanking5 = s;
            isComplating = true;

        }

    }

    void JsonSet()
    {
        SendJson = new JsonManager.Send.APIJson("GetScore").ToJson();

    }
}
