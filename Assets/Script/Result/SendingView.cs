using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendingView : MonoBehaviour
{
    [SerializeField]
    private GameObject inputView;
    [SerializeField]
    private GameObject rankingView;
    [SerializeField]
    private GameObject worldrankingView;
    [SerializeField]
    private Text rank;
    [SerializeField]
    private Text sendingText;
    [SerializeField]
    private ScoreSend scoreSend;

    public bool isSending;
    public bool isComplating;
    public bool isErr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendCheck()
    {
        if (isComplating)
            rankingView.SetActive(true);
        else
            inputView.SetActive(true);
    }

    public void CansellSend()
    {
        if (isErr || !isSending)
        {
            inputView.SetActive(false);
            if (isErr)
            {
                isErr = false;
                isSending = false;
            }
        }
    }

    public void Send()
    {
        if (isSending)
            return;

        isSending = true;
        StartCoroutine(StartSending());
        scoreSend.Send();
    }

    IEnumerator StartSending()
    {
        Coroutine sending = StartCoroutine(Sending());
        print(scoreSend.request);

        yield return new WaitUntil(() => scoreSend.request.isDone);
        EndSending(sending);


        if (scoreSend.request.isNetworkError)
        {
            Err();
            yield break;
        }

        yield return new WaitUntil(() => scoreSend.isComplating);

        rank.text = Save.EndlessMyRanking.ToString();
        isComplating = true;
        rankingView.SetActive(true);
    }

    void Err()
    {
        sendingText.text = "エラー";
        isErr = true;
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
