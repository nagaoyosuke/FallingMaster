using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldRankingView : MonoBehaviour
{
    [SerializeField]
    private GameObject RankingView;
    [SerializeField]
    private GameObject WolrdView;
    [SerializeField]
    private WorldRankingGet World;

    [SerializeField]
    private Text sendingText;

    [SerializeField]
    private Text[] names;
    [SerializeField]
    private Text[] points;

    [SerializeField]
    private bool isEndless;

    public bool isSending;
    public bool isComplating;
    public bool isErr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Send()
    {
        if (isSending)
            return;

        isSending = true;
        isComplating = false;
        StartCoroutine(StartSending());
        World.Send();
    }

    IEnumerator StartSending()
    {
        Coroutine sending = StartCoroutine(Sending());

        yield return new WaitUntil(() => World.request.isDone);
        EndSending(sending);


        if (World.request.isNetworkError)
        {
            Err();
            yield break;
        }

        yield return new WaitUntil(() => World.isComplating);

        SetRanking();
        isSending = false;

        isComplating = true;
        WolrdView.SetActive(true);
    }

    void Err()
    {
        sendingText.text = "エラー";
        isErr = true;
    }

    void SetRanking()
    {
        JsonManager.Receive.MemberJson[] d;
        if (isEndless)
            d = Save.CountryRanking5.endless;
        else
            d = Save.CountryRanking5.dani;

        print(names.Length);
        print(d.Length);

        for(int i = 0; i < names.Length; i++)
        {
            names[i].text = d[i].name;
            points[i].text = d[i].score.ToString();
        }
    }

    void EndSending(Coroutine sending)
    {
        if (sending == null)
            return;

        StopCoroutine(sending);
        sendingText.gameObject.SetActive(false);
    }

    IEnumerator Sending()
    {
        while (true)
        {
            sendingText.text = "送信中.";
            yield return new WaitForSeconds(0.5f);
            sendingText.text = "送信中..";
            yield return new WaitForSeconds(0.5f);
            sendingText.text = "送信中...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
